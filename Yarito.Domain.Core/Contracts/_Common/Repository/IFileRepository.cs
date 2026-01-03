namespace Yarito.Domain.Core.Contracts._Common.Repository
{
    public interface IFileRepository
    {
        Task<string> UploadAsync(Stream file, string folder, string fileFormat, CancellationToken ct);
        Task<bool> DeleteAsync(string filePath, CancellationToken ct);
    }
}
