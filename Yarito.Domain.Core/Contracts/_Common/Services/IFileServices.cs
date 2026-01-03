namespace Yarito.Domain.Core.Contracts._Common.Services
{
    public interface IFileServices
    {
        Task<string> SaveImageOnDiskAsync(Stream imageStream, string folder, string fileFormat, CancellationToken ct);
        Task<bool> DeleteImageOnDiskAsync(string filePath, CancellationToken ct);
    }
}
