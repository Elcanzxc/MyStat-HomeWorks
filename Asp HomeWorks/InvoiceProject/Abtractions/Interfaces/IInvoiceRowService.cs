using InvoiceProject.DTO.Invoice;

namespace InvoiceProject.Abtractions.Interfaces;

public interface IInvoiceRowService
{
    Task<InvoiceRowDto> AddRowAsync(int invoiceId, CreateInvoiceRowDto dto);
    Task<InvoiceRowDto?> UpdateRowAsync(int rowId, CreateInvoiceRowDto dto);
    Task<bool> DeleteRowAsync(int rowId);
}