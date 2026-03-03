using InvoiceProject.Common;
using InvoiceProject.DTO;
using InvoiceProject.Models;

namespace InvoiceProject.Abtractions.Interfaces;

public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetAll(string userId);
    Task<Invoice?> GetById(int id, string userId);
    Task<bool> CustomerExists(int customerId, string userId);
    Task Create(Invoice invoice);
    void Update(Invoice invoice);
    Task Delete(Invoice invoice);
    Task UpdateStatus(int id, InvoiceStatus newStatus, string userId);
    Task<(IEnumerable<Invoice> Items, int TotalCount)> GetPagedAsync(
        string userId,
        int? customerId,
        InvoiceStatus? status,
        decimal? minTotal,
        string? search,
        string? sort,
        string sortDirection,
        int page,
        int pageSize);
}


