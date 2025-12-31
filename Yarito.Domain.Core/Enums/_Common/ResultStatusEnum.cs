namespace Yarito.Domain.Core.Enums._Common;

/// <summary>
/// وضعیت‌های نتیجه یک عملیات را مشخص می‌کند.
/// </summary>
public enum ResultStatusEnum
{
    /// <summary>
    /// عملیات با موفقیت انجام شد.
    /// </summary>
    Success,

    /// <summary>
    /// عملیات با هشدار انجام شد.
    /// </summary>
    Warning,

    /// <summary>
    /// عملیات با خطا مواجه شد.
    /// </summary>
    Failure
}
