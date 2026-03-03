using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DataAccess;
using InvoiceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceProject.Repositories;

public class InvoiceAttachmentRepository : IInvoiceAttachmentRepository
{
    private readonly InvoiceDbContext _context;

    public InvoiceAttachmentRepository(InvoiceDbContext context) => _context = context;

    public async Task<InvoiceAttachment?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Attachments.FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task<InvoiceAttachment?> GetByIdWithInvoiceAsync(int id, CancellationToken ct = default)
    {
        return await _context.Attachments
            .Include(a => a.Invoice)
            .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task AddAsync(InvoiceAttachment attachment, CancellationToken ct = default)
    {

        await _context.Attachments.AddAsync(attachment, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task Delete(InvoiceAttachment attachment)
    {
        _context.Attachments.Remove(attachment);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> InvoiceExistsAsync(int invoiceId, CancellationToken ct = default)
    {
        return await _context.Invoices.AnyAsync(i => i.Id == invoiceId, ct);
    }
}
