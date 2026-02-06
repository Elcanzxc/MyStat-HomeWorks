namespace InvoiceProject.DataAccess.Entities;

public class InvoiceRowEntity
{
    public int Id { get; set; }


    public int InvoiceId { get; set; }
    public virtual InvoiceEntity Invoice { get; set; } = null!;

    public string Service { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }


    public decimal Sum { get; set; }
}
