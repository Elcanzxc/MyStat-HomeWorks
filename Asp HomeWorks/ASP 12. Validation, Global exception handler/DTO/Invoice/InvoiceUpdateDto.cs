namespace InvoiceProject.DTO.Invoice;

public class InvoiceUpdateDto
{
    public int CustomerId { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }

    public string? Comment { get; set; }

}
