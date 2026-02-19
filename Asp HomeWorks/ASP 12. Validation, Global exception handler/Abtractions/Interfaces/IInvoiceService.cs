using InvoiceProject.Common;
using InvoiceProject.DTO.Invoice;
using InvoiceProject.Models;

namespace InvoiceProject.Abtractions.Interfaces;

public interface IInvoiceService
{
    Task<IEnumerable<InvoiceResponseDto>> GetAll();
    Task<InvoiceResponseDto?> GetById(int id);

    Task<InvoiceResponseDto> Create(InvoiceRequestDto dto);

    Task<InvoiceResponseDto?> Update(int id, InvoiceUpdateDto dto);

    Task<bool> UpdateStatus(int id, InvoiceStatus newStatus);

    Task<bool> SoftDelete(int id);
    Task<bool> HardDelete(int id);

    /// <summary>
    /// Retrieves a paginated list of invoices with advanced filtering.
    /// </summary>
    Task<PagedResult<InvoiceResponseDto>> GetPagedAsync(InvoiceQueryParams queryParams);
}
