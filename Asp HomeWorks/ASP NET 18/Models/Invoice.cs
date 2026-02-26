namespace InvoiceProject.Models;

public class Invoice
{
    public int Id { get; set; }
    public int CustomerId { get; set; }

    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

    public decimal TotalSum { get; set; }

    public string? Comment { get; set; }
    public InvoiceStatus Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public Customer Customer { get; set; } = null!;
    public ICollection<InvoiceRow> Rows { get; set; } = new List<InvoiceRow>();





    public void RecalculateTotal()
    {
        TotalSum = Rows.Sum(r => r.Quantity * r.Rate);
    }



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