using Yarito.Domain.Core.Contracts.Images.Repository;
using Yarito.Domain.Core.Contracts.Images.Services;
using Yarito.Domain.Core.DTOs.Images;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Services.Images
{
    public class ImageServices(
        IImageRepop imageRepop) : IImageServices
    {
        public async Task<Result<bool>> AddRangeAsync(List<ImageStreamDto> lDto, int requestId, CancellationToken ct)
        {
            var result = await imageRepop.AddRangeAsync(lDto, requestId, ct);
            return result
                ? Result<bool>.Success("تصاویر با موفقیت در دیتابیس ذخیره شدند")
                : Result<bool>.Failure("در هنگام ذخیره تصاویر مشکلی رخ داد");
        }

        public async Task<Result<bool>> RemoveRequestImagesAsync(int requestId, CancellationToken ct)
        {
            var result = await imageRepop.RemoveRequestImagesAsync(requestId, ct);
            return result
                ? Result<bool>.Success("تصاویر با موفقیت از دیتابیس حذف شدند")
                : Result<bool>.Failure("در هنگام حذف تصاویر مشکلی رخ داد");
        }
    }
}
