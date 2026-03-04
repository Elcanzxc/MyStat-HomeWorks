using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DataAccess;
using InvoiceProject.Models;

namespace InvoiceProject.Repositories;

public class UserRepository : IUserRepository
{
    private readonly InvoiceDbContext _context;

    public UserRepository(InvoiceDbContext context)
    {
        _context = context;
    }

}
