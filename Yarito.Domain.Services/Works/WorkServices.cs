using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Framework;


namespace Yarito.Domain.Services.Works;

public class WorkServices(
    IWorkRepo workRepo,
    IWorkQueryRepo workQueryRepo,
    IExpertWorkRepo expertWorkRepo) : IWorkServices
{
    #region Query Methods

    public async Task<Result<WorkDto>> GetByIdAsync(int workId, CancellationToken ct)
    {
        var result = await workRepo.GetByIdAsync(workId, ct);
        return result is not null
            ? Result<WorkDto>.Success("خدمت با موفقیت دریافت شد", result)
            : Result<WorkDto>.Failure("خدمت یافت نشد");
    }

    public async Task<Result<List<WorksFullDto>>> GetWorksByIDs(List<int> ids, CancellationToken ct)
    {
        var result = await workQueryRepo.GetWorksByIDs(ids, ct);
        return result.Count > 0
            ? Result<List<WorksFullDto>>.Success("لیست خدمات با موفقیت دریافت شد", result)
            : Result<List<WorksFullDto>>.Failure("هیچ خدمتی یافت نشد");
    }

    public async Task<IReadOnlyList<WorksFullDto>> GetExpertWorks(int expertId, CancellationToken ct) 
        => await expertWorkRepo.GetExpertWorks(expertId, ct);

    #endregion

    #region Command Methods

    public async Task<Result<WorkDto>> AddAsync(WorkDto newWork, CancellationToken ct)
    {
        return await workRepo.AddAsync(newWork, ct)
            ? Result<WorkDto>.Success("خدمت جدید با موفقیت ایجاد شد")
            : Result<WorkDto>.Failure("خدمت ایجاد نشد");
    }

    public async Task<Result<WorkDto>> UpdateAsync(WorkDto work, CancellationToken ct)
    {
        return await workRepo.UpdateAsync(work, ct)
            ? Result<WorkDto>.Success("اطلاعات خدمت با موفقیت بروز شد")
            : Result<WorkDto>.Failure("اطلاعات خدمت بروز نشد");
    }

    public async Task<Result<bool>> DeleteAsync(int workId, CancellationToken ct)
    {
        return await workRepo.DeleteAsync(workId, ct)
            ? Result<bool>.Success("خدمت با موفقیت حذف شد")
            : Result<bool>.Failure("خطا در حذف خدمت");
    }

    #endregion

    #region Validation Methods

    public Result<WorkDto> IsPropertyValid(WorkDto dto)
    {
        dto.Title = Validation.NormalizeText(dto.Title);

        if (!Validation.IsValidText(dto.Title, 100))
            return Result<WorkDto>.Warning("طول عنوان بیشتر از 100 کاراکتر است");

        if (dto.BasePrice < 0)
            return Result<WorkDto>.Warning("قیمت پایه نمی‌تواند منفی باشد");

        if (dto.CategoryId <= 0)
            return Result<WorkDto>.Warning("دسته‌بندی نامعتبر است");

        return Result<WorkDto>.Success("همه ویژگی ها معتبر هستند", dto);
    }

    public async Task<Result<WorkDto>> IsTitleDuplicationAsync(string title, CancellationToken ct, int? workId = null)
    {
        if (workId == null)
            return await workRepo.IsTitleExistsAsync(title, ct)
                ? Result<WorkDto>.Warning("خدمت با این عنوان وجود دارد")
                : Result<WorkDto>.Success("خدمت با این عنوان وجود ندارد");

        return await workRepo.IsTitleExistsAsync(title, workId.Value, ct)
            ? Result<WorkDto>.Warning("خدمت با این عنوان وجود دارد")
            : Result<WorkDto>.Success("خدمت با این عنوان وجود ندارد");
    }

    public async Task<bool> IsCategoryInUseAsync(int categoryId, CancellationToken ct)
        => await workRepo.IsCategoryInUseAsync(categoryId, ct);

    #endregion
}
