namespace InvoiceProject.Abtractions.Interfaces;

public interface IInvoiceExportService
{
    Task<byte[]> GeneratePdfAsync(int invoiceId, string userId);
}