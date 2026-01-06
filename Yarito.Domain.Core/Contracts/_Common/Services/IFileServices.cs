namespace Yarito.Domain.Core.Contracts._Common.Services;

/// <summary>
/// اینترفیس سرویس برای مدیریت عملیات ذخیره و حذف فایل‌ها.
/// </summary>
public interface IFileServices
{
    /// <summary>
    /// ذخیره تصویر روی دیسک.
    /// </summary>
    /// <param name="imageStream">استریم تصویر</param>
    /// <param name="folder">پوشه مقصد</param>
    /// <param name="fileFormat">فرمت فایل (پسوند)</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>مسیر فایل ذخیره شده</returns>
    Task<string> SaveImageOnDiskAsync(Stream imageStream, string folder, string fileFormat, CancellationToken ct);

    /// <summary>
    /// حذف تصویر از دیسک.
    /// </summary>
    /// <param name="filePath">مسیر فایل</param>
    /// <param name="ct">توکن لغو عملیات</param>
    /// <returns>نتیجه موفقیت یا عدم موفقیت عملیات</returns>
    Task<bool> DeleteImageOnDiskAsync(string filePath, CancellationToken ct);
}
