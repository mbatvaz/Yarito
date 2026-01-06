using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Framework;

namespace Yarito.Domain.Services.Works;

public class CategoryServices(ICategoryRepo categoryRepo) : ICategoryServices
{
    #region Query Methods

    public async Task<CategoryDto?> GetByIdAsync(int categoryId, CancellationToken ct)
        => await categoryRepo.GetByIdAsync(categoryId, ct);

    public async Task<IReadOnlyList<CategoryStringDataDto>> GetAllCategoriesNamesAsync(CancellationToken ct)
        => await categoryRepo.GetAllCategoriesNamesAsync(ct);

    public async Task<IReadOnlyList<CategoryDto>> GetJustCategoriesListAsync(CancellationToken ct)
        => await categoryRepo.GetJustCategoriesListAsync(ct);

    public async Task<PagedResult<CategoryFullDto>> GetCategoriesListAsync(CategoryReqDto q, CancellationToken ct)
        => await categoryRepo.GetCategoriesListAsync(q, ct);

    #endregion

    #region Command Methods

    public async Task<Result<CategoryDto>> AddAsync(CategoryDto newCategory, CancellationToken ct)
    {
        return await categoryRepo.AddAsync(newCategory, ct)
            ? Result<CategoryDto>.Success("دسته بندی جدید با موفقیت ایجاد شد")
            : Result<CategoryDto>.Failure("دسته بندی ایجاد نشد");
    }

    public async Task<Result<CategoryDto>> UpdateAsync(CategoryDto category, CancellationToken ct)
    {
        return await categoryRepo.UpdateAsync(category, ct)
            ? Result<CategoryDto>.Success("اطلاعات دسته بندی با موفقیت بروز شد")
            : Result<CategoryDto>.Failure("اطلاعات دسته بندی بروز نشد");
    }

    public async Task<Result<bool>> DeleteAsync(int categoryId, CancellationToken ct)
    {
        return await categoryRepo.DeleteAsync(categoryId, ct)
            ? Result<bool>.Success("دسته‌بندی با موفقیت حذف شد")
            : Result<bool>.Failure("خطا در حذف دسته‌بندی");
    }

    #endregion

    #region Validation Methods

    public Result<CategoryDto> IsPropertyValid(CategoryDto dto)
    {
        dto.Title = Validation.NormalizeText(dto.Title);
        dto.Description = Validation.NormalizeText(dto.Description);

        if (!Validation.IsValidText(dto.Title, 100))
            return Result<CategoryDto>.Warning("طول عنوان بیشتر از 100 کاراکتر است");

        if (dto.Description is not null && !Validation.IsValidText(dto.Description, 500))
            return Result<CategoryDto>.Warning("طول توضیحات بیشتر از 500 کاراکتر است");

        return Result<CategoryDto>.Success("همه ویژگی ها معتبر هستند", dto);
    }

    public async Task<Result<CategoryDto>> IsTitleDuplicationAsync(string title, CancellationToken ct, int? categoryId = null)
    {
        if (categoryId == null)
            return await categoryRepo.IsTitleExistsAsync(title, ct)
                ? Result<CategoryDto>.Warning("دسته بندی با این عنوان وجود دارد")
                : Result<CategoryDto>.Success("دسته بندی با این عنوان وجود ندارد");

        return await categoryRepo.IsTitleExistsAsync(title, categoryId.Value, ct)
            ? Result<CategoryDto>.Warning("دسته بندی با این عنوان وجود دارد")
            : Result<CategoryDto>.Success("دسته بندی با این عنوان وجود ندارد");
    }

    #endregion
}