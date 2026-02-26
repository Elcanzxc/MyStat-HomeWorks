namespace InvoiceProject.DTO.Invoice;

public class InvoiceRequestDto
{

    public int CustomerId { get; set; }
  
    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }
   
    public string? Comment { get; set; }
    
    public List<InvoiceRowRequestDto> RowsCreate { get; set; } = [];
}
