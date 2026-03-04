using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DataAccess;
using InvoiceProject.Models;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace InvoiceProject.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly InvoiceDbContext _context;

    public InvoiceRepository(InvoiceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Invoice>> GetAll(string userId)
    {
        return await _context.Invoices
            .Include(i => i.Rows)
            .Include(i => i.Customer)
            .Where(i => i.Customer.UserId == userId)
            .ToListAsync();
    }

    public async Task<Invoice?> GetById(int id, string userId)
    {
        return await _context.Invoices
            .Include(i => i.Rows)
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(i => i.Id == id && i.Customer.UserId == userId);
    }

    public async Task<bool> CustomerExists(int customerId, string userId)
    {
        return await _context.Customers
            .AnyAsync(c => c.Id == customerId && c.UserId == userId);
    }

    public async Task Create(Invoice invoice)
    {
  
        await _context.Invoices.AddAsync(invoice);
        await _context.SaveChangesAsync();
    }

    public async void Update(Invoice invoice)
    {

        _context.Invoices.Update(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Invoice invoice)
    {
        invoice.DeletedAt = DateTimeOffset.UtcNow;
      
        _context.Invoices.Remove(invoice);

        await _context.SaveChangesAsync();
        await Task.CompletedTask;
    }

    public async Task<(IEnumerable<Invoice> Items, int TotalCount)> GetPagedAsync(
        string userId,
        int? customerId,
        InvoiceStatus? status,
        decimal? minTotal,
        string? search,
        string? sort,
        string sortDirection,
        int page,
        int pageSize)
    {
        var query = _context.Invoices
            .Include(i => i.Customer)
            .Where(i => i.DeletedAt == null && i.Customer.UserId == userId)
            .AsQueryable();

      
        if (customerId.HasValue)
            query = query.Where(i => i.CustomerId == customerId.Value);

        if (status.HasValue)
            query = query.Where(i => i.Status == status.Value);

        if (minTotal.HasValue)
            query = query.Where(i => i.TotalSum >= minTotal.Value);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchTerm = search.ToLower();
            query = query.Where(i =>
                (i.Comment != null && i.Comment.ToLower().Contains(searchTerm)) ||
                i.Customer.Name.ToLower().Contains(searchTerm));
        }

      
        query = ApplySorting(query, sort, sortDirection);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(i => i.Rows)
            .ToListAsync();

        return (items, totalCount);
    }

    private IQueryable<Invoice> ApplySorting(IQueryable<Invoice> query, string? sort, string sortDirection)
    {
        var isDescending = sortDirection.ToLower() == "desc";

        return sort?.ToLower() switch
        {
            "totalsum" => isDescending ? query.OrderByDescending(i => i.TotalSum) : query.OrderBy(i => i.TotalSum),
            "status" => isDescending ? query.OrderByDescending(i => i.Status) : query.OrderBy(i => i.Status),
            "startdate" => isDescending ? query.OrderByDescending(i => i.StartDate) : query.OrderBy(i => i.StartDate),
            "createdat" => isDescending ? query.OrderByDescending(i => i.CreatedAt) : query.OrderBy(i => i.CreatedAt),
            _ => isDescending ? query.OrderByDescending(i => i.CreatedAt) : query.OrderBy(i => i.CreatedAt)
        };
    }

    public async Task UpdateStatus(int id, InvoiceStatus newStatus, string userId)
    {

        var entity = await GetById(id, userId);
        if (entity == null) throw new NullReferenceException();

        entity.Status = newStatus;
        await _context.SaveChangesAsync();
    
    }


}
