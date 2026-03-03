using InvoiceProject.Models;

namespace InvoiceProject.Abtractions.Interfaces;

public interface IInvoiceAttachmentRepository
{
    Task<InvoiceAttachment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<InvoiceAttachment?> GetByIdWithInvoiceAsync(int id, CancellationToken ct = default);
    Task AddAsync(InvoiceAttachment attachment, CancellationToken ct = default);
    Task Delete(InvoiceAttachment attachment);
    Task<bool> InvoiceExistsAsync(int invoiceId, CancellationToken ct = default);
}
