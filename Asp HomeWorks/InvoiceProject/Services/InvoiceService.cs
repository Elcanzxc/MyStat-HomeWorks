using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DataAccess;
using InvoiceProject.DataAccess.Entities;
using InvoiceProject.DTO.Invoice;
using Microsoft.EntityFrameworkCore;

namespace InvoiceProject.Services;

public class InvoiceService : IInvoiceService
{
    private readonly InvoiceDbContext _context;
    private readonly IMapper _mapper;

    public InvoiceService(InvoiceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InvoiceResponseDto>> GetAllAsync()
    {
        var invoices = await _context.Invoices
            .Include(i => i.Rows)
            .ToListAsync();
        return _mapper.Map<IEnumerable<InvoiceResponseDto>>(invoices);
    }

    public async Task<InvoiceResponseDto?> GetByIdAsync(int id)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == id);

        return invoice == null ? null : _mapper.Map<InvoiceResponseDto>(invoice);
    }

    public async Task<InvoiceResponseDto> CreateAsync(CreateInvoiceRequestDto dto)
    {
        var entity = _mapper.Map<InvoiceEntity>(dto);

       
        entity.TotalSum = entity.Rows.Sum(r => r.Sum);

        _context.Invoices.Add(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<InvoiceResponseDto>(entity);
    }

    public async Task<InvoiceResponseDto?> UpdateAsync(int id, CreateInvoiceRequestDto dto)
    {
        var entity = await _context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (entity == null) return null;

      
        if (entity.Status != InvoiceStatus.Created)
        {
            throw new InvalidOperationException("Only invoices with 'Created' status can be edited.");
        }

     
        _mapper.Map(dto, entity);

      
        entity.TotalSum = entity.Rows.Sum(r => r.Sum);

        await _context.SaveChangesAsync();
        return _mapper.Map<InvoiceResponseDto>(entity);
    }

    public async Task<bool> UpdateStatusAsync(int id, Models.InvoiceStatus newStatus)
    {
        var entity = await _context.Invoices.FindAsync(id);
        if (entity == null) return false;

        entity.Status = (InvoiceStatus)newStatus;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var entity = await _context.Invoices.FindAsync(id);
        if (entity == null) return false;

        entity.DeletedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> HardDeleteAsync(int id)
    {
        var entity = await _context.Invoices.FindAsync(id);
        if (entity == null) return false;

       
        if (entity.Status != InvoiceStatus.Created)
        {
            throw new InvalidOperationException("Only invoices with 'Created' status can be hard deleted.");
        }

        _context.Invoices.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

}