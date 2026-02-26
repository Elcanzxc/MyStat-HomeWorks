using Microsoft.AspNetCore.Identity;

namespace InvoiceProject.Models;

public class User: IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }

    public ICollection<Customer> Customers { get; set; } = new List<Customer>();

}
