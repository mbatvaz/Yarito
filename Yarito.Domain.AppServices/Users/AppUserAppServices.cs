using Yarito.Domain.Core.Contracts._Common.Services;
using Yarito.Domain.Core.Contracts.Cities.Services;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.Contracts.Users.Services;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Core.DTOs._Common;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Domain.AppServices.Users
{
    public class AppUserAppServices(
        IAppUserServices appUserServices,
        IRequestServices requestServices,
        IBidServices bidServices,
        ICityServices cityServices,
        IWorkServices workServices,
        IFileServices fileServices) : IAppUserAppServices
    {
        private const string DefaultProfileImage = "/Images/Profile/default.png";

        public async Task<AppStatisticsDto> GetStatisticsAsync(CancellationToken ct)
        {
            var userStatistics = await appUserServices.GetUserCountAsync(ct);
            return new AppStatisticsDto
            {
                NumberOfCustomers = userStatistics.NumberOfCustomers,
                NumberOfExperts = userStatistics.NumberOfExperts,
                NumberOfRequests = await requestServices.GetCountAsync(ct),
                NumberOfBid = await bidServices.GetCountAsync(ct),
            };
        }

        public async Task<PagedResult<AppUserSummaryDto>> GetAppUserSummaryListAsync(AppUserReqDto q, CancellationToken ct)
            => await appUserServices.GetAppUserSummaryListAsync(q, ct);

        public async Task<Result<AppUserSummaryDto>> GetAppUserSummaryByIdAsync(int userid, CancellationToken ct)
            => await appUserServices.GetAppUserSummaryByIdAsync(userid, ct);

        public async Task<Result<AppUserFullDto>> GetAppUserFullByIdAsync(int userId, CancellationToken ct)
            => await appUserServices.GetAppUserFullByIdAsync(userId, ct);

        public async Task<IReadOnlyList<CategoryFullDto>> GetExpertCategoryWorksListDto(int expertId, CancellationToken ct)
            => await appUserServices.GetExpertCategoryWorksListDto(expertId, ct);

        public async Task<Result<bool>> UpdateAsync(AppUserUpdateDto dto, CancellationToken ct)
        {
            var validationResult = appUserServices.IsPropertyValid(dto);
            if (validationResult.Status != ResultStatusEnum.Success || validationResult.Data is null)
                return Result<bool>.Failure(validationResult.Message);

            dto = validationResult.Data;

            if (dto.Email is not null)
            {
                var emailResult = await appUserServices.IsEmailDuplicationAsync(dto.Email, ct, dto.UserId);
                if (emailResult.Status != ResultStatusEnum.Success)
                    return Result<bool>.Failure(emailResult.Message);
            }

            if (dto.CityId is not null && !await cityServices.IsExistAsync(dto.CityId.Value, ct))
                return Result<bool>.Failure("شهر وارد شده معتبر نیست");

            if (dto.WorkIds is not null && dto.WorkIds.Count > 0)
            {
                var workFindResult = await workServices.GetWorksByIDs(dto.WorkIds, ct);

                if (workFindResult.Status != ResultStatusEnum.Success)
                    return Result<bool>.Failure("خدمات انتخاب شده یافت نشدند");
            }

            string? savedImageUrl = null;

            try
            {
                if (dto.DeleteProfileImage)
                {
                    dto.ProfileImgPath = DefaultProfileImage;
                }
                else if (dto.ProfileImage is not null && dto.ProfileImageFormat is not null)
                {
                    savedImageUrl = await fileServices.SaveImageOnDiskAsync(
                        dto.ProfileImage, 
                        "/Images/Profile", 
                        dto.ProfileImageFormat, 
                        ct);
                    dto.ProfileImgPath = savedImageUrl;
                }

                var updateResult = await appUserServices.UpdateAsync(dto, ct);
                if (updateResult.Status != ResultStatusEnum.Success)
                    throw new Exception(updateResult.Message ?? "خطا در بروزرسانی اطلاعات");

                if ((savedImageUrl is not null || dto.DeleteProfileImage) &&
                    dto.CurrentProfileImage is not null &&
                    dto.CurrentProfileImage.EndsWith("default.png", StringComparison.OrdinalIgnoreCase))
                {
                    await fileServices.DeleteImageOnDiskAsync(dto.CurrentProfileImage, ct);
                }

                return Result<bool>.Success("اطلاعات با موفقیت بروزرسانی شد");
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(savedImageUrl))
                    await fileServices.DeleteImageOnDiskAsync(savedImageUrl, ct);

                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> SoftDeleteAsync(int userId, CancellationToken ct)
            => await appUserServices.SoftDeleteAsync(userId, ct);

        public async Task<bool> IsCitySetAsync(int userId, CancellationToken ct) 
            => await appUserServices.IsCitySetAsync(userId, ct);

        public async Task<Result<UserDashboardDto>> GetAppUserDashboardByIdAsync(int userId, CancellationToken ct)
            => await appUserServices.GetAppUserDashboardByIdAsync(userId, ct);
    }
}
