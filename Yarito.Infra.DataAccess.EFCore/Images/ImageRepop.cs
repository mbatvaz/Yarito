using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Images.Repository;
using Yarito.Domain.Core.DTOs.Images;
using Yarito.Domain.Core.Entities.Images;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Images
{
    public class ImageRepop(AppDbContext _db) : IImageRepop
    {
        public async Task<bool> AddRangeAsync(List<ImageStreamDto> lDto, int requestId, CancellationToken ct)
        {
            var images = lDto
                .Where(p => true)
                .Select(p => p.FileName)
                .Distinct()
                .Select(p => new RequestImage
                {
                    RequestId = requestId,
                    ImgPath = p
                })
                .ToList();

            _db.RequestImages.AddRange(images);
            return await _db.SaveChangesAsync(ct) > 0;
        }

        public async Task<bool> RemoveRequestImagesAsync(int requestId, CancellationToken ct)
        {
            _db.ChangeTracker.Clear();
            return await _db.RequestImages
                .Where(ri => ri.RequestId == requestId && !ri.IsDeleted)
                .ExecuteUpdateAsync(setters => setters
                        .SetProperty(x => x.IsDeleted, true), ct) > 0;
        }
    }
}
