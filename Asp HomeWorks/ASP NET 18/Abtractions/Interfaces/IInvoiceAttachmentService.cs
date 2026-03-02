using InvoiceProject.DTO;

namespace InvoiceProject.Abtractions.Interfaces;

    public interface IInvoiceAttachmentService
    {
    Task<AttachmentResponseDto?> UploadAsync(
    int taskId,
    Stream stream,
    string originalFileName,
    string contentType,
    long length,
    int userId,
    CancellationToken cancellationToken = default
    );

    Task<(Stream stream, string fileName, string contentType)?> GetDownloadAsync(
       int attachmentId,
       CancellationToken cancellationToken = default
       );

    Task<bool> DeleteAsync(
       int attachmentId,
       CancellationToken cancellationToken = default
       );

    Task<InvoiceAttachmentInfo?> GetAttachmentInfoAsync(
       int attachmentId,
       CancellationToken cancellationToken = default
       );
    }

public class InvoiceAttachmentInfo
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }

    public string StoredFileName { get; set; } = string.Empty;
    public string StorageKey { get; set; } = string.Empty;
    public int UploadedByCustomerId { get; set; }
}