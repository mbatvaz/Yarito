using Yarito.Domain.Core.Contracts._Common.Repository;

namespace Yarito.Infra.DataAccess.Storage
{
    public class FileRepository : IFileRepository
    {
        public async Task<string> UploadAsync(Stream file, string folder, string fileFormat, CancellationToken ct)
        {
            // حذف / از ابتدای folder برای جلوگیری از مشکل Path.Combine
            folder = folder.TrimStart('/', '\\');
            
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + fileFormat;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            if (file.CanSeek)
                file.Position = 0;

            try
            {
                await using (var stream = new FileStream(
                                 filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 81920,
                                 useAsync: true))
                {
                    await file.CopyToAsync(stream, 81920, ct);
                    await stream.FlushAsync(ct);
                }

                return $"/{folder}/{uniqueFileName}";
            }
            catch
            {
                await DeleteAsync($"/{folder}/{uniqueFileName}", ct);
                throw new Exception("ErrorWhenSavingImageOnDisk");
            }
        }

        public Task<bool> DeleteAsync(string filePath, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return Task.FromResult(false);

            var relativePath = filePath.Trim().Replace("\\", "/").TrimStart('/');

            var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var fullPath = Path.GetFullPath(Path.Combine(webRoot, relativePath));
            var fullWebRoot = Path.GetFullPath(webRoot);

            if (!fullPath.StartsWith(fullWebRoot, StringComparison.OrdinalIgnoreCase) || !File.Exists(fullPath))
                return Task.FromResult(false);

            File.Delete(fullPath);
            return Task.FromResult(true);
        }
    }
}
