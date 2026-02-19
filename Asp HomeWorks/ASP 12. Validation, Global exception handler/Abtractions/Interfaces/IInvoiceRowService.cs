using InvoiceProject.DTO.Invoice;
using InvoiceProject.DTO.InvoiceRow;

namespace InvoiceProject.Abtractions.Interfaces;

public interface IInvoiceRowService
{
    Task<IEnumerable<InvoiceRowResponseDto>> GetAll();
    Task<InvoiceRowResponseDto> AddRow(int invoiceId, InvoiceRowRequestDto dto);
    Task<InvoiceRowResponseDto?> UpdateRow(int rowId, InvoiceRowUpdateDto dto);
    Task<bool> DeleteRow(int rowId);
}