using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Users;
using Yarito.Domain.Core.Enums.Transactions;

namespace Yarito.Domain.Core.Entities.Transactions;

/// <summary>
/// نماینده یک تراکنش مالی در سیستم است که توسط یک کاربر انجام می‌شود.
/// </summary>
/// <remarks>
/// <para>این کلاس اطلاعات مربوط به تراکنش‌های مالی کاربران را ذخیره می‌کند.</para>
/// <para>پراپرتی‌ها:</para>
/// <list type="bullet">
/// <item><description><b>Amount:</b> مبلغ تراکنش</description></item>
/// <item><description><b>TransactionType:</b> نوع تراکنش (مانند واریز، برداشت)</description></item>
/// <item><description><b>Description:</b> توضیحات اختیاری برای تراکنش</description></item>
/// <item><description><b>RelatedEntityId:</b> شناسه موجودیت مرتبط با این تراکنش (مانند شناسه درخواست)</description></item>
/// </list>
/// <para>کلیدهای خارجی و ناوبری:</para>
/// <list type="bullet">
/// <item><description><b>UserId:</b> شناسه کاربری که تراکنش را انجام داده است</description></item>
/// <item><description><b>User:</b> ارجاع به موجودیت کاربر</description></item>
/// </list>
/// </remarks>
public class Transaction : BaseEntity
{
    // Properties
    public decimal Amount { get; set; }
    public TransactionTypeEnum TransactionType { get; set; }
    public string? Description { get; set; }
    public Guid? RelatedEntityId { get; set; }

    // Foreign Keys
    public Guid UserId { get; set; }

    // Navigation Properties
    public AppUser User { get; set; } = null!;
}
