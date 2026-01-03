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
                return Result<string>.Failure("شماره موبایل وارد شده نامعتبر است");

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
                var userFullName = await appUserServices.GetNameByIdAsync(user.Id, ct);
                identity.AddClaim(new Claim("FullName", userFullName));
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
            if (!Validation.IsValidName(dto.FirstName))
                return Result<string>.Failure("نام وارد کرده معتبر نیست");
            if (!Validation.IsValidName(dto.LastName))
                return Result<string>.Failure("نام خانوادگی وارد شده معتبر نیست");
            if (!Validation.IsValidPhoneNumber(dto.PhoneNumber))
                return Result<string>.Failure("شماره موبایل وارد شده معتبر نیست");
            if(dto.Email is not null && !Validation.IsValidEmail(dto.Email))
                return Result<string>.Failure("ایمیل وارد شده معتبر نیست");
            if(!Validation.IsValidBaseWalletBalance(dto.BaseWalletBalance))
                return Result<string>.Failure("مقدار کیف پول پایه وارد شده معتبر نیست");
            if(dto.Address is not null && !Validation.IsValidAddress(dto.Address))
                return Result<string>.Failure("آدرس وارد شده معتبر نیست");
            if(dto.ProfileImage is not null && !Validation.IsValidImageUrlOrFileName(dto.ProfileImageUrl))
                return Result<string>.Failure("تصویر پروفایل وارد شده معتبر نیست");
            if(dto.CityId is not null && !await cityServices.IsExistAsync(dto.CityId.Value, ct))
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
                var registerUser = await appUserServices.AddAsync(dto, ct);
                return !registerUser 
                    ? throw new Exception("خطایی رخ داد لطفا دوباره امتحان کنید") 
                    : Result<string>.Success("حساب کاربری شما با موفقیت ساخته شده، اقدام به ورود به حساب کاربری خود کنید");
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(savedImageUrl))
                        await fileServices.DeleteImageOnDiskAsync(savedImageUrl, ct);

                await userManager.DeleteAsync(user);
                return Result<string>.Failure(ex.Message);
            }
        }
    }
}
