using System.ComponentModel.DataAnnotations;

namespace Yarito.Domain.Core.Enums.Transactions;

/// <summary>
/// انواع مختلف تراکنش‌های مالی را در سیستم مشخص می‌کند.
/// </summary>
public enum TransactionTypeEnum
{
    /// <summary>
    /// افزایش موجودی کیف پول کاربر (مانند شارژ حساب).
    /// </summary>
    [Display(Name = "واریز")]
    Deposit = 0,

    /// <summary>
    /// کاهش موجودی کیف پول کاربر (مانند برداشت وجه).
    /// </summary>
    [Display(Name = "برداشت")]
    Withdrawal = 1,

    /// <summary>
    /// پرداخت هزینه یک درخواست خدماتی توسط مشتری.
    /// </summary>
    [Display(Name = "پرداخت هزینه درخواست")]
    PaymentForRequest = 2,

    /// <summary>
    /// دریافت درآمد حاصل از انجام یک کار توسط متخصص.
    /// </summary>
    [Display(Name = "دریافت هزینه کار")]
    IncomeFromBid = 3
}
