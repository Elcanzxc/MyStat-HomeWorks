namespace InvoiceProject.DTO.Invoice;

public class InvoiceRowRequestDto
{

    public string Service { get; set; } = null!;
  
    public decimal Quantity { get; set; }
   
    public decimal Rate { get; set; }
}
