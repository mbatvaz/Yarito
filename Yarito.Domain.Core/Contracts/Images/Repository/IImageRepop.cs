using Yarito.Domain.Core.DTOs.Images;

namespace Yarito.Domain.Core.Contracts.Images.Repository
{
    public interface IImageRepop
    {
        Task<bool> AddRangeAsync(List<ImageStreamDto> lDto, int requestId, CancellationToken ct);
        Task<bool> RemoveRequestImagesAsync(int requestId, CancellationToken ct);
    }
}
