namespace InvoiceProject.DTO.Invoice;

public class InvoiceRowResponseDto
{
    
    public string Service { get; set; } = null!;
  
    public decimal Quantity { get; set; }
   
    public decimal Rate { get; set; }

    public decimal Sum { get; set; }

}
