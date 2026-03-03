using InvoiceProject.Common;
using InvoiceProject.DTO;
using InvoiceProject.Models;

namespace InvoiceProject.Abtractions.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAll(string userId);
    Task<IEnumerable<Customer>> GetAllDetailed(string userId);
    Task<Customer?> GetById(int id, string userId);
    Task<Customer?> GetDetailedById(int id, string userId);
    Task<Customer> Create(Customer customer);
    Task Update(Customer customer);
    Task Delete(Customer customer);
    Task<(IEnumerable<Customer> Items, int TotalCount)> GetPagedAsync(
        string userId,
        string? search,
        string? sort,
        string sortDirection,
        int page,
        int pageSize);
}
