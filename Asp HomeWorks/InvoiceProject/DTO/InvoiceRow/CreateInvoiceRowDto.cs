namespace InvoiceProject.DTO.Invoice;

public class CreateInvoiceRowDto
{

    public string Service { get; set; } = null!;
  
    public decimal Quantity { get; set; }
   
    public decimal Rate { get; set; }
}
