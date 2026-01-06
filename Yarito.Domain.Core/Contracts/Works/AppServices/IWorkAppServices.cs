using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Works.AppServices;

/// <summary>
/// اینترفیس اپ‌سرویس برای مدیریت عملیات مرتبط با خدمات در لایه نمایش.
/// </summary>
public interface IWorkAppServices
{
    /// <summary>
    /// افزودن یک خدمت جدید.
    /// </summary>
    /// <param name="newWork">اطلاعات خدمت جدید</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<Result<WorkDto>> AddAsync(WorkDto newWork, CancellationToken ct);

    /// <summary>
    /// دریافت یک خدمت بر اساس شناسه.
    /// </summary>
    /// <param name="workId">شناسه خدمت</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>اطلاعات خدمت یا null در صورت عدم وجود</returns>
    Task<Result<WorkDto>> GetByIdAsync(int workId, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی یک خدمت موجود.
    /// </summary>
    /// <param name="work">اطلاعات خدمت برای به‌روزرسانی</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<Result<WorkDto>> UpdateAsync(WorkDto work, CancellationToken ct);

    /// <summary>
    /// حذف نرم یک خدمت.
    /// </summary>
    /// <param name="workId">شناسه خدمت</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<Result<bool>> DeleteAsync(int workId, CancellationToken ct);
}
