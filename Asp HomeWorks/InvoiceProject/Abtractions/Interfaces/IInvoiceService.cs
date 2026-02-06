using InvoiceProject.DTO.Invoice;
using InvoiceProject.Models;

namespace InvoiceProject.Abtractions.Interfaces;

public interface IInvoiceService
{
    Task<IEnumerable<InvoiceResponseDto>> GetAllAsync();
    Task<InvoiceResponseDto?> GetByIdAsync(int id);
    Task<InvoiceResponseDto> CreateAsync(CreateInvoiceRequestDto dto);
    Task<InvoiceResponseDto?> UpdateAsync(int id, CreateInvoiceRequestDto dto);
    Task<bool> UpdateStatusAsync(int id, InvoiceStatus newStatus);
    Task<bool> SoftDeleteAsync(int id);
    Task<bool> HardDeleteAsync(int id);
}
