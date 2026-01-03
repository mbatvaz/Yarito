using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Framework;

namespace Yarito.Domain.AppServices.Works;

public class WorkAppServices(IWorkServices workServices) : IWorkAppServices
{
    public async Task<Result<string>> AddAsync(WorkDto newWork, CancellationToken ct)
    {
        if (!Validation.IsValidText(newWork.Title, 100))
            return Result<string>.Failure("طول عنوان نباید بیشتر از 100 کاراکتر باشد");

        if (newWork.BasePrice < 0)
            return Result<string>.Failure("قیمت پایه نمی‌تواند منفی باشد");

        if (newWork.CategoryId <= 0)
            return Result<string>.Failure("دسته‌بندی نامعتبر است");

        return await workServices.AddAsync(newWork, ct)
            ? Result<string>.Success("سرویس جدید با موفقیت اضافه شد")
            : Result<string>.Failure("اضافه کردن سرویس ناموفق بود");
    }

    public async Task<Result<WorkDto>> GetByIdAsync(int workId, CancellationToken ct)
    {
        var result = await workServices.GetByIdAsync(workId, ct);
        return result is not null
            ? Result<WorkDto>.Success("سرویس با موفقیت یافت شد", result)
            : Result<WorkDto>.Warning("سرویس یافت نشد");
    }

    public async Task<Result<string>> UpdateAsync(WorkDto work, CancellationToken ct)
    {
        if (!Validation.IsValidText(work.Title, 100))
            return Result<string>.Failure("طول عنوان نباید بیشتر از 100 کاراکتر باشد");

        if (work.BasePrice < 0)
            return Result<string>.Failure("قیمت پایه نمی‌تواند منفی باشد");

        if (work.CategoryId <= 0)
            return Result<string>.Failure("دسته‌بندی نامعتبر است");

        return await workServices.UpdateAsync(work, ct)
            ? Result<string>.Success("سرویس با موفقیت ویرایش شد")
            : Result<string>.Failure("ویرایش سرویس ناموفق بود");
    }

    public async Task<Result<string>> DeleteAsync(int workId, CancellationToken ct)
    {
        return await workServices.DeleteAsync(workId, ct)
            ? Result<string>.Success("سرویس با موفقیت حذف شد")
            : Result<string>.Failure("حذف سرویس ناموفق بود");
    }
}
