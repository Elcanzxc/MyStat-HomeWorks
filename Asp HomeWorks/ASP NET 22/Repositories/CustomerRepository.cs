using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DataAccess;
using InvoiceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceProject.Repositories;

public class CustomerRepository : ICustomerRepository
{

    private readonly InvoiceDbContext _context;

    public CustomerRepository(InvoiceDbContext context) => _context = context;


    public async Task<Customer> Create(Customer customer)
    {
        customer.CreatedAt = DateTimeOffset.UtcNow;

        await _context.Customers.AddAsync(customer);

        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task Delete(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Customer>> GetAll(string userId) =>
        await _context.Customers.Where(c => c.UserId == userId).ToListAsync();

    public async Task<IEnumerable<Customer>> GetAllDetailed(string userId) =>
        await _context.Customers
            .Include(c => c.Invoices).ThenInclude(i => i.Rows)
            .Where(c => c.UserId == userId)
            .AsNoTracking().ToListAsync();

    public async Task<Customer?> GetById(int id, string userId) =>
        await _context.Customers.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

    public async Task<Customer?> GetDetailedById(int id, string userId) =>
        await _context.Customers
            .Include(c => c.Invoices).ThenInclude(i => i.Rows)
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

    public async Task<(IEnumerable<Customer> Items, int TotalCount)> GetPagedAsync(
            string userId, string? search, string? sort, string sortDirection, int page, int pageSize)
    {
        var query = _context.Customers
            .Where(c => c.DeletedAt == null && c.UserId == userId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.ToLower();
            query = query.Where(c => c.Name.ToLower().Contains(term) || c.Email.ToLower().Contains(term));
        }

        query = ApplySorting(query, sort, sortDirection);
        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return (items, total);
    }

    private IQueryable<Customer> ApplySorting(IQueryable<Customer> query, string? sort, string direction)
    {
        bool desc = direction == "desc";
        return sort?.ToLower() switch
        {
            "name" => desc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
            "email" => desc ? query.OrderByDescending(x => x.Email) : query.OrderBy(x => x.Email),
            _ => query.OrderBy(x => x.CreatedAt)
        };
    }
    public async Task Update(Customer customer)
    {
        customer.UpdatedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();
        await Task.CompletedTask;
      
    }
 }
