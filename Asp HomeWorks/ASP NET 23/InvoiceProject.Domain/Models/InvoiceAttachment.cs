namespace InvoiceProject.Models;

public class InvoiceAttachment
{
    public int Id { get; set; } 

    public int InvoiceId { get; set; }

    public Invoice Invoice { get; set; } = null!;
    public string OriginalFileName { get; set; } = string.Empty;

    public string StoredFileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long Size { get; set; }

    public int UploadedByCustomerId { get; set; }

    public Customer UploadedByCustomer { get; set; } = null!;

    public DateTimeOffset UploadedAt { get; set; }

}
