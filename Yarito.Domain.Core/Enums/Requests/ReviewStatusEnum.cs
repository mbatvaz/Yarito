using System.ComponentModel.DataAnnotations;

namespace Yarito.Domain.Core.Enums.Requests
{
    /// <summary>
    /// وضعیت‌های مختلف یک نظر را مشخص می‌کند.
    /// </summary>
    public enum ReviewStatusEnum
    {
        /// <summary>
        /// نظر ثبت شده و در انتظار بررسی یا پذیرش توسط ادمین است。
        /// </summary>
        [Display(Name = "در انتظار")]
        Pending = 0,

        /// <summary>
        /// نظر ثبت شده توسط ادمین تایید شده است。
        /// </summary>
        [Display(Name = "تایید")]
        Approved = 1,

        /// <summary>
        /// نظر ثبت شده توسط ادمین رد شده است。
        /// </summary>
        [Display(Name = "رد")]
        Rejected = 2,

        /// <summary>
        /// همه نظرات。
        /// </summary>
        [Display(Name = "همه")]
        All = 3,
    }
}
