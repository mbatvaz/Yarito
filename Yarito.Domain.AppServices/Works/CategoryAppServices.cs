using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;

namespace Yarito.Domain.AppServices.Works
{
    public class CategoryAppServices(
        ICategoryServices categoryServices,
        IWorkServices workServices) : ICategoryAppServices
    {
        public async Task<IReadOnlyList<CategoryStringDataDto>> GetAllCategoriesNamesAsync(CancellationToken ct)
            => await categoryServices.GetAllCategoriesNamesAsync(ct);

        public async Task<PagedResult<CategoryFullDto>> GetCategoriesListAsync(CategoryReqDto q, CancellationToken ct)
            => await categoryServices.GetCategoriesListAsync(q, ct);

        public async Task<IReadOnlyList<CategoryDto>> GetJustCategoriesListAsync(CancellationToken ct)
            => await categoryServices.GetJustCategoriesListAsync(ct);

        public async Task<Result<CategoryDto>> GetByIdAsync(int categoryId, CancellationToken ct)
        {
            var result = await categoryServices.GetByIdAsync(categoryId, ct);
            return result is not null
                ? Result<CategoryDto>.Success("دسته بندی یافت شد", result)
                : Result<CategoryDto>.Warning("دسته بندی یافت نشد");
        }

        public async Task<Result<CategoryDto>> AddAsync(CategoryDto newCategory, CancellationToken ct)
        {
            var validationResults = categoryServices.IsPropertyValid(newCategory);
            if (validationResults.Status != ResultStatusEnum.Success || validationResults.Data is null)
                return validationResults;

            var duplicationResult = await categoryServices.IsTitleDuplicationAsync(newCategory.Title, ct);
            if (duplicationResult.Status != ResultStatusEnum.Success)
                return duplicationResult;

            return await categoryServices.AddAsync(validationResults.Data, ct);
        }
        
        public async Task<Result<CategoryDto>> UpdateAsync(CategoryDto category, CancellationToken ct)
        {
            var validationResults = categoryServices.IsPropertyValid(category);
            if (validationResults.Status != ResultStatusEnum.Success || validationResults.Data is null)
                return validationResults;

            var duplicationResult = await categoryServices.IsTitleDuplicationAsync(category.Title, ct, category.Id);
            if (duplicationResult.Status != ResultStatusEnum.Success)
                return duplicationResult;

            return await categoryServices.UpdateAsync(validationResults.Data, ct);
        }

        public async Task<Result<bool>> DeleteAsync(int categoryId, CancellationToken ct)
        {
            if (await workServices.IsCategoryInUseAsync(categoryId, ct))
                return Result<bool>.Warning("برای حذف دسته بندی ابتدا کار های آن را حذف کنید");

            return await categoryServices.DeleteAsync(categoryId, ct);
        }
    }
}
