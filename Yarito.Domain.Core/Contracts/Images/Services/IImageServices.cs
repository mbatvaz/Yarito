using Yarito.Domain.Core.DTOs.Images;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Images.Services
{
    public interface IImageServices
    {
        Task<Result<bool>> AddRangeAsync(List<ImageStreamDto> lDto, int requestId, CancellationToken ct);
        Task<Result<bool>> RemoveRequestImagesAsync(int requestId, CancellationToken ct);
    }
}
