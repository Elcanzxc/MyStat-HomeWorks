using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO;
using InvoiceProject.Models;

namespace InvoiceProject.Services;

public class InvoiceAttachmentService : IInvoiceAttachmentService
{
    public const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5MB
    public static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf", ".txt", ".zip" };
    public static readonly string[] AllowedContentTypes = {
        "image/jpeg", "image/png", "application/pdf", "text/plain", "application/zip", "application/x-zip-compressed"
    };

    private readonly IInvoiceAttachmentRepository _repository;
    private readonly IFileStorage _storage;

    public InvoiceAttachmentService(
        IInvoiceAttachmentRepository repository,
        IFileStorage storage)
    {
        _repository = repository;
        _storage = storage;
    }

    public async Task<AttachmentResponseDto?> UploadAsync(int invoiceId, Stream stream, string originalFileName, string contentType, long length, int userId, CancellationToken ct = default)
    {
      
        ValidateFile(length, originalFileName, contentType);

        if (!await _repository.InvoiceExistsAsync(invoiceId, ct))
            throw new ArgumentException($"Invoice with ID {invoiceId} not found.");

      
        var folderKey = $"invoice/{invoiceId}";
        var info = await _storage.UploadAsync(stream, originalFileName, contentType, folderKey, ct);

     
        var attachment = new InvoiceAttachment
        {
            InvoiceId = invoiceId,
            OriginalFileName = originalFileName,
            StoredFileName = info.StoredFileName,
            ContentType = contentType,
            Size = info.Size,
            UploadedByCustomerId = userId,
            UploadedAt = DateTimeOffset.UtcNow
        };

        await _repository.AddAsync(attachment, ct);
 

        return MapToDto(attachment);
    }

    public async Task<(Stream stream, string fileName, string contentType)?> GetDownloadAsync(int attachmentId, CancellationToken ct = default)
    {
        var att = await _repository.GetByIdAsync(attachmentId, ct);
        if (att is null) return null;

        var key = $"invoice/{att.InvoiceId}/{att.StoredFileName}";
        var stream = await _storage.OpenReadAsync(key, ct);

        return (stream, att.OriginalFileName, att.ContentType);
    }

    public async Task<InvoiceAttachmentInfo?> GetAttachmentInfoAsync(int attachmentId, CancellationToken ct = default)
    {
        var att = await _repository.GetByIdWithInvoiceAsync(attachmentId, ct);
        if (att is null) return null;

        return new InvoiceAttachmentInfo
        {
            Id = att.Id,
            InvoiceId = att.InvoiceId,
            StoredFileName = att.StoredFileName,
            StorageKey = $"invoice/{att.InvoiceId}/{att.StoredFileName}",
            UploadedByCustomerId = att.UploadedByCustomerId,
        };
    }

    public async Task<bool> DeleteAsync(int attachmentId, CancellationToken ct = default)
    {
        var att = await _repository.GetByIdAsync(attachmentId, ct);
        if (att is null) return false;

        var key = $"invoice/{att.InvoiceId}/{att.StoredFileName}";

        await _repository.Delete(att);
    

    
        await _storage.DeleteAsync(key, ct);
        return true;
    }

  
    private void ValidateFile(long length, string fileName, string contentType)
    {
        if (length > MaxFileSizeBytes)
            throw new ArgumentException($"File size must not exceed {MaxFileSizeBytes / (1024 * 1024)} MB");

        var ext = Path.GetExtension(fileName)?.ToLowerInvariant();
        if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
            throw new ArgumentException($"Allowed extensions: {string.Join(", ", AllowedExtensions)}");

        if (!AllowedContentTypes.Contains(contentType, StringComparer.OrdinalIgnoreCase))
            throw new ArgumentException($"Allowed content type: {string.Join(", ", AllowedContentTypes)}");
    }

    private AttachmentResponseDto MapToDto(InvoiceAttachment att) => new()
    {
        Id = att.Id,
        InvoiceId = att.InvoiceId,
        OriginalFileName = att.OriginalFileName,
        ContentType = att.ContentType,
        Size = att.Size,
        UploadedByCustomerId = att.UploadedByCustomerId,
        UploadedAt = att.UploadedAt
    };
}