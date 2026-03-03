using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DataAccess;
using InvoiceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceProject.Repositories;

public class InvoiceRowRepository : IInvoiceRowRepository
{
    private readonly InvoiceDbContext _context;

    public InvoiceRowRepository(InvoiceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InvoiceRow>> GetAll(string userId)
    {
        return await _context.InvoiceRows
            .Include(r => r.Invoice)
                .ThenInclude(i => i.Customer)
            .Where(r => r.Invoice.Customer.UserId == userId)
            .ToListAsync();
    }

    public async Task<InvoiceRow?> GetByIdWithInvoice(int rowId, string userId)
    {

        return await _context.InvoiceRows
            .Include(r => r.Invoice)
                .ThenInclude(i => i.Customer)
            .Include(r => r.Invoice)
                .ThenInclude(i => i.Rows)
            .FirstOrDefaultAsync(r => r.Id == rowId && r.Invoice.Customer.UserId == userId);
    }

    public async Task AddRow(InvoiceRow row)
    {
        await _context.SaveChangesAsync();
        await _context.InvoiceRows.AddAsync(row);
    }

    public async Task Delete(InvoiceRow row)
    {
        _context.InvoiceRows.Remove(row);
        await _context.SaveChangesAsync();
        await Task.CompletedTask;
    }

    public async Task UpdateRow()
    {
        await _context.SaveChangesAsync();
        await Task.CompletedTask;
    }

}
