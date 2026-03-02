using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DataAccess;
using InvoiceProject.DTO;
using InvoiceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceProject.Services;

public class InvoiceAttachmentService : IInvoiceAttachmentService
{
    public const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5MB
    public static readonly string[] AllowedExtensions = {
        ".jpg", ".jpeg", ".png", ".pdf", ".txt", ".zip",
    };

    public static readonly string[] AllowedContentTypes = {
        "image/jpeg", "image/png", "application/pdf", "text/plain",
        "application/zip", "application/x-zip-compressed"
    };

    private readonly InvoiceDbContext _context;
    //private readonly ITaskItemRepository _taskItemRepository;
    //private readonly ITaskAttachmentRepository _taskAttachmentRepository;
    private readonly IFileStorage _storage;

    public InvoiceAttachmentService(InvoiceDbContext context, IFileStorage storage)
    {
        _context = context;
        _storage = storage;
    }

    //public TaskAttachmentService(
    //    ITaskItemRepository taskItemRepository,
    //    ITaskAttachmentRepository taskAttachmentRepository,
    //    IFileStorage storage)
    //{
    //    _taskItemRepository = taskItemRepository;
    //    _taskAttachmentRepository = taskAttachmentRepository;
    //    _storage = storage;
    //}

    public async Task<AttachmentResponseDto?> UploadAsync(int invoiceId, Stream stream, string originalFileName, string contentType, long length, int userId, CancellationToken cancellationToken = default)
    {
        if (length > MaxFileSizeBytes)
            throw new ArgumentException($"File size must not exceed {MaxFileSizeBytes / (1024 * 1024)} MB");

        var ext = Path.GetExtension(originalFileName)?.ToLowerInvariant();
        if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
            throw new ArgumentException($"Allowed extensions: {string.Join(", ", AllowedExtensions)}");

        if (!AllowedContentTypes.Contains(contentType, StringComparer.OrdinalIgnoreCase))
            throw new ArgumentException($"Allowed content type: {string.Join(", ", AllowedContentTypes)}");

        var invoice = await _context.Invoices.FindAsync([invoiceId], cancellationToken);
        if (invoice is null)
            throw new ArgumentException($"Invoice with ID {invoiceId} not found.");

        var folderKey = $"invoice/{invoiceId}";
        var info = await _storage.UploadAsync(stream, originalFileName, contentType, folderKey, cancellationToken);

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

        await _context.Attachments.AddAsync(attachment);

        //
        await _context.SaveChangesAsync(cancellationToken);

        return new AttachmentResponseDto
        {
            Id = attachment.Id,
            InvoiceId = attachment.InvoiceId,
            OriginalFileName = attachment.OriginalFileName,
            ContentType = attachment.ContentType,
            Size = attachment.Size,
            UploadedByCustomerId = attachment.UploadedByCustomerId,
            UploadedAt = attachment.UploadedAt
        };
    }

    public async Task<(Stream stream, string fileName, string contentType)?> GetDownloadAsync(int attachmentId, CancellationToken cancellationToken = default)
    {


        var att = await _context.Attachments.FirstOrDefaultAsync(a=> a.Id == attachmentId);
        if (att is null) return null;

        var key = $"invoice/{att.InvoiceId}/{att.StoredFileName}";
        var stream = await _storage.OpenReadAsync(key, cancellationToken);
        return (stream, att.OriginalFileName, att.ContentType);
    }

    public async Task<InvoiceAttachmentInfo?> GetAttachmentInfoAsync(int attachmentId, CancellationToken cancellationToken = default)
    {
        var att = await _context.Attachments
            .Include(a => a.Invoice)
            .FirstOrDefaultAsync(a => a.Id == attachmentId);

        if (att is null) return null;

        return new InvoiceAttachmentInfo
        {
            Id = att.Id,
            InvoiceId = att.InvoiceId,
            StoredFileName = att.StoredFileName,
            StorageKey = $"tasks/{att.InvoiceId}/{att.StoredFileName}",
            UploadedByCustomerId = att.UploadedByCustomerId,
        };
    }

    public async Task<bool> DeleteAsync(int attachmentId, CancellationToken cancellationToken = default)
    {
        var att = await _context.Attachments.FirstOrDefaultAsync(a => a.Id == attachmentId);
        if (att is null) return false;

        var key = $"invoice/{att.InvoiceId}/{att.StoredFileName}";
     
        _context.Attachments.Remove(att);
        await _context.SaveChangesAsync();

        await _storage.DeleteAsync(key, cancellationToken);
        return true;
    }


}
