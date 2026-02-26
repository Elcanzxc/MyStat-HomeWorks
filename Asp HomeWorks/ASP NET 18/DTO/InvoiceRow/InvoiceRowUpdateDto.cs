namespace InvoiceProject.DTO.InvoiceRow;

public class InvoiceRowUpdateDto
{
    public string Service { get; set; } = null!;

    public decimal Quantity { get; set; }

    public decimal Rate { get; set; }
}
