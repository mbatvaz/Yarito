namespace Yarito.Domain.Core.Enums.Requests;

/// <summary>
/// فیلدهای قابل مرتب‌سازی برای درخواست‌ها را مشخص می‌کند.
/// </summary>
public enum RequestSortableEnum
{
    /// <summary>
    /// مرتب‌سازی بر اساس تاریخ ایجاد.
    /// </summary>
    CreatedAt,

    /// <summary>
    /// مرتب‌سازی بر اساس تاریخ ترجیحی بازدید.
    /// </summary>
    PreferredVisitDateTime,

    /// <summary>
    /// مرتب‌سازی بر اساس قیمت پیشنهادی.
    /// </summary>
    ProposedPrice
}
