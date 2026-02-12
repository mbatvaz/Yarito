using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Works.Services;

/// <summary>
/// اینترفیس سرویس برای مدیریت منطق کسب‌وکار مرتبط با خدمات.
/// </summary>
public interface IWorkServices
{
    #region Query Methods

    /// <summary>
    /// دریافت یک خدمت بر اساس شناسه.
    /// </summary>
    Task<Result<WorkDto>> GetByIdAsync(int workId, CancellationToken ct);

    /// <summary>
    /// گرفتن لیستی از شناسه کارها و برگرداندن لیستی از اطلاعات کامل آن‌ها.
    /// </summary>
    Task<Result<List<WorksFullDto>>> GetWorksByIDs(List<int> ids, CancellationToken ct);

    Task<IReadOnlyList<WorksFullDto>> GetExpertWorks(int expertId, CancellationToken ct);

    #endregion

    #region Command Methods

    /// <summary>
    /// افزودن یک خدمت جدید.
    /// </summary>
    Task<Result<WorkDto>> AddAsync(WorkDto newWork, CancellationToken ct);

    /// <summary>
    /// به‌روزرسانی یک خدمت موجود.
    /// </summary>
    Task<Result<WorkDto>> UpdateAsync(WorkDto work, CancellationToken ct);

    /// <summary>
    /// حذف نرم یک خدمت.
    /// </summary>
    Task<Result<bool>> DeleteAsync(int workId, CancellationToken ct);

    #endregion

    #region Validation Methods

    /// <summary>
    /// اعتبارسنجی ویژگی‌های خدمت.
    /// </summary>
    Result<WorkDto> IsPropertyValid(WorkDto dto);

    /// <summary>
    /// بررسی تکراری بودن عنوان خدمت.
    /// </summary>
    Task<Result<WorkDto>> IsTitleDuplicationAsync(string title, CancellationToken ct, int? workId = null);

    /// <summary>
    /// بررسی استفاده از دسته‌بندی در خدمات.
    /// </summary>
    Task<bool> IsCategoryInUseAsync(int categoryId, CancellationToken ct);

    #endregion
}
