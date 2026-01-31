using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Domain.Core.Contracts.Users.Services;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Users;
using Yarito.Framework;

namespace Yarito.Domain.Services.Users;

public class AppUserServices(IAppUserRepo appUserRepo) : IAppUserServices
{
    #region Query Methods

    public async Task<AppUserStaticsDto> GetUserCountAsync(CancellationToken ct)
        => await appUserRepo.GetUserCountAsync(ct)
        ?? new AppUserStaticsDto { NumberOfCustomers = 0, NumberOfExperts = 0 };

    public async Task<Result<AppUserSummaryDto>> GetAppUserSummaryByIdAsync(int userId, CancellationToken ct)
    {
        var result = await appUserRepo.GetAppUserSummaryByIdAsync(userId, ct);
        return result is not null
            ? Result<AppUserSummaryDto>.Success("کاربر یافت شد", result)
            : Result<AppUserSummaryDto>.Warning("کاربر پیدا نشد");
    }

    public async Task<PagedResult<AppUserSummaryDto>> GetAppUserSummaryListAsync(AppUserReqDto q, CancellationToken ct)
        => await appUserRepo.GetAppUserSummaryListAsync(q, ct);

    public async Task<Result<AppUserFullDto>> GetAppUserFullByIdAsync(int userId, CancellationToken ct)
    {
        var user = await appUserRepo.GetAppUserFullByIdAsync(userId, ct);
        return user is null
            ? Result<AppUserFullDto>.Warning("کاربر یافت نشد")
            : Result<AppUserFullDto>.Success("کاربر مورد نظر یافت شد", user);
    }

    public async Task<IReadOnlyList<CategoryFullDto>> GetExpertCategoryWorksListDto(int expertId, CancellationToken ct)
        => await appUserRepo.GetExpertCategoryWorksListDto(expertId, ct);

    #endregion

    #region Command Methods

    public async Task<Result<RegisterDto>> AddAsync(RegisterDto dto, CancellationToken ct)
    {
        return await appUserRepo.AddAsync(dto, ct)
            ? Result<RegisterDto>.Success("کاربر جدید با موفقیت ایجاد شد")
            : Result<RegisterDto>.Failure("کاربر ایجاد نشد");
    }

    public async Task<Result<bool>> UpdateAsync(int userId, AppUserUpdateDto dto, CancellationToken ct)
    {
        return await appUserRepo.UpdateAsync(userId, dto, ct)
            ? Result<bool>.Success("اطلاعات کاربر با موفقیت بروزرسانی شد")
            : Result<bool>.Failure("خطا در بروزرسانی اطلاعات کاربر");
    }

    public async Task<Result<bool>> SoftDeleteAsync(int userId, CancellationToken ct)
    {
        return await appUserRepo.SoftDeleteAsync(userId, ct)
            ? Result<bool>.Success("کاربر با موفقیت حذف شد")
            : Result<bool>.Failure("خطا در حذف کاربر");
    }

    #endregion

    #region Validation Methods

    public Result<RegisterDto> IsPropertyValid(RegisterDto dto)
    {
        dto.FirstName = Validation.NormalizeText(dto.FirstName);
        dto.LastName = Validation.NormalizeText(dto.LastName);
        dto.Address = dto.Address is not null ? Validation.NormalizeText(dto.Address) : null;

        if (!Validation.IsValidName(dto.FirstName))
            return Result<RegisterDto>.Warning("نام وارد شده معتبر نیست");

        if (!Validation.IsValidName(dto.LastName))
            return Result<RegisterDto>.Warning("نام خانوادگی وارد شده معتبر نیست");

        if (!Validation.IsValidPhoneNumber(dto.PhoneNumber))
            return Result<RegisterDto>.Warning("شماره موبایل وارد شده معتبر نیست");

        if (dto.Email is not null && !Validation.IsValidEmail(dto.Email))
            return Result<RegisterDto>.Warning("ایمیل وارد شده معتبر نیست");

        if (!Validation.IsValidBaseWalletBalance(dto.BaseWalletBalance))
            return Result<RegisterDto>.Warning("مقدار کیف پول پایه وارد شده معتبر نیست");

        if (dto.Address is not null && !Validation.IsValidAddress(dto.Address))
            return Result<RegisterDto>.Warning("آدرس وارد شده معتبر نیست");

        if (dto.ProfileImage is not null && !Validation.IsValidImageUrlOrFileName(dto.ProfileImageUrl))
            return Result<RegisterDto>.Warning("تصویر پروفایل وارد شده معتبر نیست");

        return Result<RegisterDto>.Success("همه ویژگی‌ها معتبر هستند", dto);
    }

    public Result<AppUserUpdateDto> IsPropertyValid(AppUserUpdateDto dto)
    {
        dto.FirstName = dto.FirstName is not null ? Validation.NormalizeText(dto.FirstName) : null;
        dto.LastName = dto.LastName is not null ? Validation.NormalizeText(dto.LastName) : null;
        dto.Address = dto.Address is not null ? Validation.NormalizeText(dto.Address) : null;

        if (dto.FirstName is not null && !Validation.IsValidName(dto.FirstName))
            return Result<AppUserUpdateDto>.Warning("نام وارد شده معتبر نیست");

        if (dto.LastName is not null && !Validation.IsValidName(dto.LastName))
            return Result<AppUserUpdateDto>.Warning("نام خانوادگی وارد شده معتبر نیست");

        if (dto.Email is not null && !Validation.IsValidEmail(dto.Email))
            return Result<AppUserUpdateDto>.Warning("ایمیل وارد شده معتبر نیست");

        if (dto.Address is not null && !Validation.IsValidAddress(dto.Address))
            return Result<AppUserUpdateDto>.Warning("آدرس وارد شده معتبر نیست");

        if (dto.ProfileImage is not null && !Validation.IsValidImageUrlOrFileName(dto.ProfileImageExtension))
            return Result<AppUserUpdateDto>.Warning("تصویر پروفایل وارد شده معتبر نیست");

        return Result<AppUserUpdateDto>.Success("همه ویژگی‌ها معتبر هستند", dto);
    }

    public async Task<Result<RegisterDto>> IsEmailDuplicationAsync(string email, CancellationToken ct, int? userId = null)
    {
        if (userId == null)
            return await appUserRepo.IsEmailExistsAsync(email, ct)
                ? Result<RegisterDto>.Warning("کاربری با این ایمیل وجود دارد")
                : Result<RegisterDto>.Success("ایمیل تکراری نیست");

        return await appUserRepo.IsEmailExistsAsync(email, userId.Value, ct)
            ? Result<RegisterDto>.Warning("کاربری با این ایمیل وجود دارد")
            : Result<RegisterDto>.Success("ایمیل تکراری نیست");
    }

    public async Task<bool> IsExistsAsync(int userId, CancellationToken ct)
        => await appUserRepo.IsExistsAsync(userId, ct);

    public async Task<bool> IsCitySetAsync(int userId, CancellationToken ct)
        => await appUserRepo.IsCitySetAsync(userId, ct);

    public async Task<Result<string>> GetCustomerAddressAsync(int userId, CancellationToken ct)
    {
        var result = await appUserRepo.GetCustomerAddressAsync(userId, ct);
        return string.IsNullOrWhiteSpace(result) 
            ? Result<string>.Warning("آدرسی برای کاربر ذخیره نشده است") 
            : Result<string>.Success("آدرس کاربر یافت شد", result);
    }

    #endregion
}
