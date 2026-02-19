using InvoiceProject.Common;
using InvoiceProject.DTO.Customer;

namespace InvoiceProject.Abtractions.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerResponseDto>> GetAll();
    Task<IEnumerable<CustomerDetailsResponseDto>> GetAllDetailed();
    Task<CustomerDetailsResponseDto?> GetDetailedById(int id);
    Task<CustomerResponseDto?> GetById(int id);

    Task<CustomerResponseDto> Create(CustomerRequestDto dto);
    Task<CustomerResponseDto?> Update(int id, CustomerUpdateDto dto);

    Task<bool> SoftDeleteAsync(int id);

    Task<bool> HardDeleteAsync(int id);

    /// <summary>
    /// Retrieves a paginated list of customers.
    /// </summary>
    Task<PagedResult<CustomerResponseDto>> GetPagedAsync(CustomerQueryParams queryParams);
}
