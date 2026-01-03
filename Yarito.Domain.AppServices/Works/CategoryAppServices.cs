using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Framework;

namespace Yarito.Domain.AppServices.Works
{
    public class CategoryAppServices(
        ICategoryServices categoryServices) : ICategoryAppServices
    {
        public async Task<IReadOnlyList<CategoryStringDataDto>> GetAllCategoriesNamesAsync(CancellationToken ct)
            => await categoryServices.GetAllCategoriesNamesAsync(ct);

        public async Task<PagedResult<CategoryFullDto>> GetCategoriesListAsync(CategoryReqDto q, CancellationToken ct)
            => await categoryServices.GetCategoriesListAsync(q, ct);

        public async Task<Result<string>> AddAsync(CategoryDto newCategory, CancellationToken ct)
        {
            if (!Validation.IsValidText(newCategory.Title, 100))
                return Result<string>.Failure("طول عنوان بیشتر از 100 کاراکتر است");
            if (newCategory.Description is not null && !Validation.IsValidText(newCategory.Description, 500))
                return Result<string>.Failure("طول توضیحات بیشتر 500 کاراکتر است");

            return await categoryServices.AddAsync(newCategory, ct)
                ? Result<string>.Success("دسته بندی جدید با موفقیت ایجاد شد")
                : Result<string>.Failure("دسته بندی ایجاد نشد");
        }

        public async Task<Result<CategoryDto>> GetByIdAsync(int categoryId, CancellationToken ct)
        {
            var result = await categoryServices.GetByIdAsync(categoryId, ct);
            return result is not null
                ? Result<CategoryDto>.Success("دسته بندی یافت شد", result)
                : Result<CategoryDto>.Warning("دسته بندی یافت نشد");
        }

        public async Task<Result<string>> UpdateAsync(CategoryDto category, CancellationToken ct)
        {
            if (!Validation.IsValidText(category.Title, 100))
                return Result<string>.Failure("طول عنوان بیشتر از 100 کاراکتر است");
            if (category.Description is not null && !Validation.IsValidText(category.Description, 500))
                return Result<string>.Failure("طول توضیحات بیشتر 500 کاراکتر است");

            return await categoryServices.UpdateAsync(category, ct)
                ? Result<string>.Success("اطلاعات دسته بندی با موفقیت بروز شد")
                : Result<string>.Failure("اطلاعات دسته بندی بروز نشد");
        }

        public async Task<Result<string>> DeleteAsync(int categoryId, CancellationToken ct)
        {
            return await categoryServices.DeleteAsync(categoryId, ct)
                ? Result<string>.Success("دسته‌بندی با موفقیت حذف شد")
                : Result<string>.Failure("خطا در حذف دسته‌بندی");
        }

        public async Task<IReadOnlyList<CategoryDto>> GetJustCategoriesListAsync(CancellationToken ct)
            => await categoryServices.GetJustCategoriesListAsync(ct);
    }
}
