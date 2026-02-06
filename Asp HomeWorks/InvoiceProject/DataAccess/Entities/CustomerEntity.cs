namespace InvoiceProject.DataAccess.Entities;

public class CustomerEntity
{

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }


    public virtual ICollection<InvoiceEntity> Invoices { get; set; } = new List<InvoiceEntity>();
}
