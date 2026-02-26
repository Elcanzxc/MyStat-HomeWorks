using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DataAccess;
using InvoiceProject.DTO.Invoice;
using InvoiceProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace InvoiceProject.Services;

public class InvoiceService : IInvoiceService
{
    private readonly InvoiceDbContext _context;
    private readonly IMapper _mapper;
    private readonly string _currentUserId;

    public InvoiceService(InvoiceDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;

        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        _currentUserId = userId ?? throw new UnauthorizedAccessException("User not authenticated");
    }

    public async Task<IEnumerable<InvoiceResponseDto>> GetAll()
    {
        var invoices = await _context.Invoices
            .Include(i => i.Rows)
            .Include(i => i.Customer)
            .Where(i => i.Customer.UserId == _currentUserId) // Фильтр владельца
            .ToListAsync();

        return _mapper.Map<IEnumerable<InvoiceResponseDto>>(invoices);
    }

    public async Task<InvoiceResponseDto?> GetById(int id)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Rows)
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(i => i.Id == id && i.Customer.UserId == _currentUserId);

        return invoice == null ? null : _mapper.Map<InvoiceResponseDto>(invoice);
    }

    public async Task<InvoiceResponseDto> Create(InvoiceRequestDto dto)
    {
        // ВАЖНО: Проверяем, что CustomerId принадлежит текущему юзеру
        var customerExists = await _context.Customers
            .AnyAsync(c => c.Id == dto.CustomerId && c.UserId == _currentUserId);

        if (!customerExists)
        {
            throw new UnauthorizedAccessException("You cannot create an invoice for this customer.");
        }

        var invoice = _mapper.Map<Invoice>(dto);

        // Маппер заполнит Rows, но нам нужно посчитать TotalSum
        invoice.RecalculateTotal();

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        return _mapper.Map<InvoiceResponseDto>(invoice);
    }

    public async Task<InvoiceResponseDto?> Update(int id, InvoiceUpdateDto dto)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Rows)
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(i => i.Id == id && i.Customer.UserId == _currentUserId);

        if (invoice == null) return null;

        if (invoice.Status != InvoiceStatus.Created)
        {
            throw new InvalidOperationException("Only invoices with 'Created' status can be edited.");
        }

        _mapper.Map(dto, invoice);
        invoice.RecalculateTotal();

        await _context.SaveChangesAsync();
        return _mapper.Map<InvoiceResponseDto>(invoice);
    }

    public async Task<bool> UpdateStatus(int id, InvoiceStatus newStatus)
    {
        var entity = await _context.Invoices
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(i => i.Id == id && i.Customer.UserId == _currentUserId);

        if (entity == null) return false;

        entity.Status = newStatus;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SoftDelete(int id)
    {
        var entity = await _context.Invoices
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(i => i.Id == id && i.Customer.UserId == _currentUserId);

        if (entity == null) return false;

        entity.DeletedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> HardDelete(int id)
    {
        var entity = await _context.Invoices
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(i => i.Id == id && i.Customer.UserId == _currentUserId);

        if (entity == null) return false;

        if (entity.Status != InvoiceStatus.Created)
        {
            throw new InvalidOperationException("Only invoices with 'Created' status can be hard deleted.");
        }

        _context.Invoices.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<PagedResult<InvoiceResponseDto>> GetPagedAsync(InvoiceQueryParams queryParams)
    {
        queryParams.Validate();

        var query = _context.Invoices
            .Include(i => i.Customer)
            .Where(i => i.DeletedAt == null && i.Customer.UserId == _currentUserId) // Фильтр безопасности
            .AsQueryable();

        if (queryParams.CustomerId.HasValue)
            query = query.Where(i => i.CustomerId == queryParams.CustomerId.Value);

        if (queryParams.Status.HasValue)
            query = query.Where(i => i.Status == queryParams.Status.Value);

        if (queryParams.MinTotal.HasValue)
            query = query.Where(i => i.TotalSum >= queryParams.MinTotal.Value);

        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            var searchTerm = queryParams.Search.ToLower();
            query = query.Where(i =>
                (i.Comment != null && i.Comment.ToLower().Contains(searchTerm)) ||
                i.Customer.Name.ToLower().Contains(searchTerm));
        }

        query = ApplySorting(query, queryParams.Sort, queryParams.SortDirection!);

        var totalCount = await query.CountAsync();
        var skip = (queryParams.Page - 1) * queryParams.PageSize;

        var invoices = await query
            .Skip(skip)
            .Take(queryParams.PageSize)
            .Include(i => i.Rows) // Не забываем подгрузить строки для результата
            .ToListAsync();

        var dtos = _mapper.Map<IEnumerable<InvoiceResponseDto>>(invoices);

        return PagedResult<InvoiceResponseDto>.Create(dtos, queryParams.Page, queryParams.PageSize, totalCount);
    }

    private IQueryable<Invoice> ApplySorting(IQueryable<Invoice> query, string? sort, string sortDirection)
    {
        var isDescending = sortDirection == "desc";

        return sort switch
        {
            "totalsum" => isDescending ? query.OrderByDescending(i => i.TotalSum) : query.OrderBy(i => i.TotalSum),
            "status" => isDescending ? query.OrderByDescending(i => i.Status) : query.OrderBy(i => i.Status),
            "startdate" => isDescending ? query.OrderByDescending(i => i.StartDate) : query.OrderBy(i => i.StartDate),
            "createdat" => isDescending ? query.OrderByDescending(i => i.CreatedAt) : query.OrderBy(i => i.CreatedAt),
            _ => query.OrderByDescending(i => i.CreatedAt)
        };
    }
}