using InvoiceProject.Storage;

namespace InvoiceProject.Abtractions.Interfaces;

public interface IFileStorage
{
    Task<StoredFileInfo> UploadAsync(
        Stream stream,
        string originalFileName,
        string contentType,
        string folderKey,
        CancellationToken cancellationToken = default
        );

    Task<Stream> OpenReadAsync(
        string storageKey,
        CancellationToken cancellation = default);

    Task DeleteAsync(
        string storageKey,
        CancellationToken cancellation = default
        );
}
