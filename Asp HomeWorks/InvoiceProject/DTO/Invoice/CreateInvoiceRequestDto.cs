namespace InvoiceProject.DTO.Invoice;

public class CreateInvoiceRequestDto
{

    public int CustomerId { get; set; }
  
    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }
   
    public string? Comment { get; set; }
    
    public List<CreateInvoiceRowDto> Rows { get; set; } = [];
}
