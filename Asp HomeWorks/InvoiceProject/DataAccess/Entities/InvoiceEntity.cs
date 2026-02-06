using InvoiceProject.Models;

namespace InvoiceProject.DataAccess.Entities;

public class InvoiceEntity
{
    public int Id { get; set; }

 
    public int CustomerId { get; set; }
    public virtual CustomerEntity Customer { get; set; } = null!;

    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

    public string? Comment { get; set; }
    public InvoiceStatus Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }


    public virtual ICollection<InvoiceRowEntity> Rows { get; set; } = new List<InvoiceRowEntity>();


    public decimal TotalSum { get; set; }
}

public enum InvoiceStatus
{
    Created,
    Sent,
    Received,
    Paid,
    Cancelled,
    Rejected
}