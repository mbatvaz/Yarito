using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Works;

namespace Yarito.Domain.Services.Works
{
    public class CategoryServices(
        ICategoryRepo categoryRepo) : ICategoryServices
    {
        public async Task<IReadOnlyList<CategoryStringDataDto>> GetAllCategoriesNamesAsync(CancellationToken ct)
            => await categoryRepo.GetAllCategoriesNamesAsync(ct);

        public async Task<PagedResult<CategoryFullDto>> GetCategoriesListAsync(CategoryReqDto q, CancellationToken ct)
            => await categoryRepo.GetCategoriesListAsync(q, ct);

        public async Task<bool> AddAsync(CategoryDto newCategory, CancellationToken ct) 
            => await categoryRepo.AddAsync(newCategory, ct);

        public async Task<CategoryDto?> GetByIdAsync(int categoryId, CancellationToken ct)
            => await categoryRepo.GetByIdAsync(categoryId, ct);

        public async Task<bool> UpdateAsync(CategoryDto category, CancellationToken ct)
            => await categoryRepo.UpdateAsync(category, ct);

        public async Task<bool> DeleteAsync(int categoryId, CancellationToken ct)
            => await categoryRepo.DeleteAsync(categoryId, ct);

        public async Task<IReadOnlyList<CategoryDto>> GetJustCategoriesListAsync(CancellationToken ct)
            => await categoryRepo.GetJustCategoriesListAsync(ct);
    }
}
