namespace Yarito.Domain.Core.Contracts._Common.Repository;

/// <summary>
/// اینترفیس ریپازیتوری برای مدیریت عملیات آپلود و حذف فایل‌ها.
/// </summary>
public interface IFileRepository
{
    /// <summary>
    /// آپلود فایل به مسیر مشخص شده.
    /// </summary>
    /// <param name="file">استریم فایل</param>
    /// <param name="folder">پوشه مقصد</param>
    /// <param name="fileFormat">فرمت فایل (پسوند)</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>مسیر فایل آپلود شده</returns>
    Task<string> UploadAsync(Stream file, string folder, string fileFormat, CancellationToken ct);

    /// <summary>
    /// حذف فایل از مسیر مشخص شده.
    /// </summary>
    /// <param name="filePath">مسیر فایل</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> DeleteAsync(string filePath, CancellationToken ct);
}
