using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Yarito.Domain.Core.Contracts._Common.Services;
using Yarito.Domain.Core.Contracts.Cities.Services;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.Contracts.Users.Services;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Users;
using Yarito.Framework;

namespace Yarito.Domain.AppServices.Users
{
    public class AuthenticationAppServices(
        IAppUserServices appUserServices,
        IHttpContextAccessor httpContextAccessor,
        IFileServices fileServices,
        ICityServices cityServices,
        SignInManager<IdentityUser<int>> signInManager,
        UserManager<IdentityUser<int>> userManager) : IAuthenticationAppServices
    {
        public async Task<Result<string>> LoginAsync(LoginDto dto, CancellationToken ct)
        {

            if (!Validation.IsValidPhoneNumber(dto.PhoneNumber))
                return Result<string>.Warning("شماره موبایل معتبر نیست");

            await signInManager.SignOutAsync();

            var user = await userManager.FindByNameAsync(dto.PhoneNumber);
            if (user is null)
                return Result<string>.Failure("شماره موبایل یا رمز عبور وارد شده اشتباه است");

            var check = await signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);
            if (!check.Succeeded)
                return Result<string>.Failure("شماره موبایل یا رمز عبور وارد شده اشتباه است");

            var role = (await userManager.GetRolesAsync(user)).SingleOrDefault();

            if (role is null)
            {
                await signInManager.SignOutAsync();
                return Result<string>.Failure("مشکلی برای حساب کاربری شما رخ داده است. با پشتیبانی تماس بگیرید.");
            }
            
            var principal = await signInManager.CreateUserPrincipalAsync(user);
            var identity = (ClaimsIdentity)principal.Identity!;

            if (role != "Admin")
            {
                var userInfoResult = await appUserServices.GetAppUserSummaryByIdAsync(user.Id, ct);
                if (userInfoResult is { Status: ResultStatusEnum.Success, Data: not null })
                {
                    identity.AddClaim(new Claim("FullName", $"{userInfoResult.Data.FirstName} {userInfoResult.Data.LastName}"));
                    identity.AddClaim(new Claim("ProfileImage", userInfoResult.Data.ProfileImgPath));
                }
                else
                {
                    await signInManager.SignOutAsync();
                    return Result<string>.Failure("امکان ورود به حساب کاربری وجود ندارد");
                }
            }

            await httpContextAccessor.HttpContext!.SignInAsync(
                IdentityConstants.ApplicationScheme,
                principal, new AuthenticationProperties
                {
                    IsPersistent = dto.RememberMe
                });

            return Result<string>.Success("با موفقیت وارد شدید", role);
        }

        public async Task<Result<string>> LogoutAsync()
        {
            await signInManager.SignOutAsync();
            return Result<string>.Success("با موفقیت از حساب کاربری خود خارج شدید");
        }

        public async Task<Result<string>> RegisterAsync(RegisterDto dto, CancellationToken ct)
        {
            var validationResult = appUserServices.IsPropertyValid(dto);
            if (validationResult.Status != ResultStatusEnum.Success || validationResult.Data is null)
                return Result<string>.Failure(validationResult.Message);

            dto = validationResult.Data;

            if (dto.Email is not null)
            {
                var emailResult = await appUserServices.IsEmailDuplicationAsync(dto.Email, ct);
                if (emailResult.Status != ResultStatusEnum.Success)
                    return Result<string>.Failure(emailResult.Message);
            }

            if (dto.CityId is not null && !await cityServices.IsExistAsync(dto.CityId.Value, ct))
                return Result<string>.Failure("شهر وارد شده معتبر نیست");

            var user = new IdentityUser<int>
            {
                UserName = dto.PhoneNumber,
                PhoneNumber = dto.PhoneNumber
            };

            var identityResult = await userManager.CreateAsync(user, dto.Password);
            if (!identityResult.Succeeded)
                return Result<string>.Failure(identityResult.Errors.First().Description);

            var roleResult = await userManager.AddToRoleAsync(user, dto.UserType == UserTypeEnum.Customer ? "Customer" : "Expert");
            if (!roleResult.Succeeded)
            {
                await userManager.DeleteAsync(user);
                return Result<string>.Failure(roleResult.Errors.First().Description);
            }

            dto.Id = user.Id;
            string? savedImageUrl = null;

            try
            {
                if (dto.ProfileImage is not null && dto.ProfileImageUrl is not null)
                {
                    savedImageUrl = await fileServices.SaveImageOnDiskAsync(dto.ProfileImage, "/Images/Profile", dto.ProfileImageUrl, ct);
                    dto.ProfileImageUrl = savedImageUrl;
                }

                var registerResult = await appUserServices.AddAsync(dto, ct);
                if (registerResult.Status != ResultStatusEnum.Success)
                    throw new Exception(registerResult.Message ?? "خطایی رخ داد لطفا دوباره امتحان کنید");

                return Result<string>.Success("حساب کاربری شما با موفقیت ساخته شده، اقدام به ورود به حساب کاربری خود کنید");
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(savedImageUrl))
                    await fileServices.DeleteImageOnDiskAsync(savedImageUrl, ct);

                await userManager.DeleteAsync(user);
                return Result<string>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> ChangePasswordAsync(ChangePasswordDto dto, CancellationToken ct)
        {
            var user = await userManager.FindByIdAsync(dto.Id.ToString());
            if (user is null)
            {
                await signInManager.SignOutAsync();
                return Result<bool>.Failure("مشخصات کاربر پیدا نشد");
            }

            var result = await userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);

            if (!result.Succeeded)
                return Result<bool>.Failure(result.Errors.First().Description);

            await signInManager.SignOutAsync();
            return Result<bool>.Success("رمز عبور شما با موفقیت تغییر کرد، لطفا دوباره وارد سیستم شوید");
        }
    }
}
