using InvoiceProject.Models;

namespace InvoiceProject.Abtractions.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetOrderedByEmailExceptIdsAsync(IEnumerable<string> excludeIds);
}
