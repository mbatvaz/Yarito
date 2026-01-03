using Yarito.Domain.Core.Contracts._Common.Repository;
using Yarito.Domain.Core.Contracts._Common.Services;

namespace Yarito.Domain.Services._Common
{
    public class FileServices(
        IFileRepository fileRepository) : IFileServices
    {
        public async Task<string> SaveImageOnDiskAsync(Stream imageStream, string folder, string fileFormat,CancellationToken ct)
            => await fileRepository.UploadAsync(imageStream, folder, fileFormat, ct);

        public Task<bool> DeleteImageOnDiskAsync(string filePath, CancellationToken ct)
            => fileRepository.DeleteAsync(filePath, ct);
    }
}
