using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DataAccess;
using InvoiceProject.DTO.Invoice;
using InvoiceProject.DTO.InvoiceRow;
using InvoiceProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InvoiceProject.Services;

public class InvoiceRowService : IInvoiceRowService
{
    private readonly InvoiceDbContext _context;
    private readonly IMapper _mapper;
    private readonly string _currentUserId;

    public InvoiceRowService(InvoiceDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;

        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        _currentUserId = userId ?? throw new UnauthorizedAccessException("User not authenticated");
    }

    public async Task<IEnumerable<InvoiceRowResponseDto>> GetAll()
    {
        // Достаем строки только тех инвойсов, чей клиент принадлежит текущему юзеру
        var invoicesRows = await _context.InvoiceRows
            .Include(r => r.Invoice)
                .ThenInclude(i => i.Customer)
            .Where(r => r.Invoice.Customer.UserId == _currentUserId) // ПРОВЕРКА ПО ЦЕПОЧКЕ
            .ToListAsync();

        return _mapper.Map<IEnumerable<InvoiceRowResponseDto>>(invoicesRows);
    }

    public async Task<InvoiceRowResponseDto> AddRow(int invoiceId, InvoiceRowRequestDto dto)
    {
        // Проверяем инвойс и его владельца (через Customer)
        var invoice = await _context.Invoices
            .Include(i => i.Rows)
            .Include(i => i.Customer) // Обязательно подгружаем клиента для проверки UserId
            .FirstOrDefaultAsync(i => i.Id == invoiceId && i.Customer.UserId == _currentUserId);

        if (invoice == null) throw new KeyNotFoundException("Invoice not found or access denied.");

        if (invoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Cannot add rows to an invoice that is not in 'Created' status.");

        var rowEntity = _mapper.Map<InvoiceRow>(dto);
        rowEntity.InvoiceId = invoiceId;

        _context.InvoiceRows.Add(rowEntity);

        // Используем твой метод модели для пересчета
        invoice.RecalculateTotal();

        await _context.SaveChangesAsync();
        return _mapper.Map<InvoiceRowResponseDto>(rowEntity);
    }

    public async Task<InvoiceRowResponseDto?> UpdateRow(int rowId, InvoiceRowUpdateDto dto)
    {
        // Проверяем строку через всю цепочку до UserId
        var row = await _context.InvoiceRows
            .Include(r => r.Invoice)
                .ThenInclude(i => i.Customer)
            .Include(r => r.Invoice)
                .ThenInclude(i => i.Rows)
            .FirstOrDefaultAsync(r => r.Id == rowId && r.Invoice.Customer.UserId == _currentUserId);

        if (row == null) return null;

        if (row.Invoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Cannot update rows of an invoice that is not in 'Created' status.");

        _mapper.Map(dto, row);
        row.Invoice.RecalculateTotal();

        await _context.SaveChangesAsync();
        return _mapper.Map<InvoiceRowResponseDto>(row);
    }

    public async Task<bool> DeleteRow(int rowId)
    {
        var row = await _context.InvoiceRows
            .Include(r => r.Invoice)
                .ThenInclude(i => i.Customer)
            .Include(r => r.Invoice)
                .ThenInclude(i => i.Rows)
            .FirstOrDefaultAsync(r => r.Id == rowId && r.Invoice.Customer.UserId == _currentUserId);

        if (row == null) return false;

        if (row.Invoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Cannot delete rows from an invoice that is not in 'Created' status.");

        _context.InvoiceRows.Remove(row);

        await _context.SaveChangesAsync();

        // После сохранения пересчитываем общую сумму инвойса
        row.Invoice.RecalculateTotal();
        await _context.SaveChangesAsync();

        return true;
    }
}