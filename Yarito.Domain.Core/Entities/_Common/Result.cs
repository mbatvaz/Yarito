using System.Text.Json.Serialization;
using Yarito.Domain.Core.Enums._Common;

namespace Yarito.Domain.Core.Entities._Common;

/// <summary>
/// کلاس جنریک برای نتایج عملیات با پشتیبانی از موفقیت، هشدار و خطا.
/// </summary>
/// <typeparam name="T">نوع داده برگشتی</typeparam>
[method: JsonConstructor]
public class Result<T>(ResultStatusEnum status, string? message, T? data)
{
    public ResultStatusEnum Status { get; private set; } = status;
    public string? Message { get; private set; } = message;
    public T? Data { get; private set; } = data;

    public static Result<T> Success(string? message = null, T? data = default) =>
        new(ResultStatusEnum.Success, message, data);

    public static Result<T> Warning(string? message = null, T? data = default) =>
        new(ResultStatusEnum.Warning, message, data);

    public static Result<T> Failure(string? message = null, T? data = default) =>
        new(ResultStatusEnum.Failure, message, data);
}
