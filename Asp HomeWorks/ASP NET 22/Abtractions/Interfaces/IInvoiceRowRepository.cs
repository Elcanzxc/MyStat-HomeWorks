using InvoiceProject.DTO;
using InvoiceProject.Models;

namespace InvoiceProject.Abtractions.Interfaces;

public interface IInvoiceRowRepository
{
    Task UpdateRow();
    Task<IEnumerable<InvoiceRow>> GetAll(string userId);
    Task<InvoiceRow?> GetByIdWithInvoice(int rowId, string userId);
    Task AddRow(InvoiceRow row);
    Task Delete(InvoiceRow row);
}
