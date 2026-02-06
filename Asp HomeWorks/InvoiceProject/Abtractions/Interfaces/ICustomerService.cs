using InvoiceProject.DTO.Customer;

namespace InvoiceProject.Abtractions.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerResponseDto>> GetAllAsync();
    Task<CustomerResponseDto?> GetByIdAsync(int id);
    Task<CustomerResponseDto> CreateAsync(CustomerRequestDto dto);
    Task<CustomerResponseDto?> UpdateAsync(int id, CustomerRequestDto dto);
    Task<bool> SoftDeleteAsync(int id);
    Task<bool> HardDeleteAsync(int id);
}
