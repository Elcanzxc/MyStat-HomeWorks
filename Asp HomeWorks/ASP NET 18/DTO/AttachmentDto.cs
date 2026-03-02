namespace InvoiceProject.DTO;

public class AttachmentResponseDto
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long Size { get; set; }
    public int UploadedByCustomerId { get; set; }
    public DateTimeOffset UploadedAt { get; set; }
}