using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Domain.Core.Contracts.Users.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Framework;

namespace Yarito.Domain.Services.Requests
{
    public class BidServices(
        IBidRepo bidRepo,
        IRequestRepo requestRepo,
        IAppUserRepo appUserRepo) : IBidServices
    {
        public async Task<int> GetCountAsync(CancellationToken ct) 
            => await bidRepo.GetCountAsync(ct);

        public async Task<PagedResult<BidSummaryDto>> GetBidsSummaryListAsync(BidReqDto q, CancellationToken ct)
            => await bidRepo.GetBidsSummaryListAsync(q, ct);

        public async Task<PagedResult<BidFullDto>> GetBidsFullListAsync(BidReqDto q, CancellationToken ct)
            => await bidRepo.GetBidsFullListAsync(q, ct);

        public async Task<Result<BidFullDto>> GetBidFullByIdAsync(int bidId, CancellationToken ct)
        {
            var result = await bidRepo.GetBidFullByIdAsync(bidId, ct);
            return result is not null
                ? Result<BidFullDto>.Success("پیشنهاد مورد نظر یافت شد", result)
                : Result<BidFullDto>.Failure("پیشنهاد مورد نظر یافت نشد");
        }

        public async Task<Result<bool>> ChangeSingleStatusAsync(int bidId, BidStatusEnum newStatus, CancellationToken ct, bool save = true)
        {
            return await bidRepo.ChangeSingleStatusAsync(bidId, newStatus, ct, save)
                ? Result<bool>.Success("وضعیت پیشنهاد با موفقیت تغییر کرد")
                : Result<bool>.Failure("خطایی در هنگام تغییر وضعیت پیشنهاد رخ داد");
        }

        public async Task<Result<BidDetailsDto>> GetBidDetailsAsync(int bidId, CancellationToken ct)
        {
            var result = await bidRepo.GetBidDetailsAsync(bidId, ct);
            return result is not null
                ? Result<BidDetailsDto>.Success("پیشنهاد یافت شد", result)
                : Result<BidDetailsDto>.Failure("پیشنهادی مورد نظر یافت نشد");
        }

        public async Task<Result<bool>> RejectAllBidsByRequestIdAsync(int requestId, CancellationToken ct, bool save = false)
        {
            var result = await bidRepo.RejectAllBidsByRequestIdAsync(requestId, ct, save);
            return result
                ? Result<bool>.Success("تمامی پیشنهادهای مربوط به این درخواست لغو شدند.")
                : Result<bool>.Failure("خطا در لغو پیشنهادهای مربوط به درخواست.");
        }

        public async Task<Result<bool>> RejectBidAsync(int bidId, CancellationToken ct, bool save = true)
        {
            var updateResult = await bidRepo.ChangeSingleStatusAsync(bidId, BidStatusEnum.Rejected, ct, save);
            return updateResult
                ? Result<bool>.Success("پیشنهاد با موفقیت رد شد.")
                : Result<bool>.Failure("خطا در رد پیشنهاد.");
        }

        public async Task<Result<bool>> AcceptBidAsync(int bidId, int requestId, CancellationToken ct, bool save = false)
        {
            return await bidRepo.AcceptBidAndRejectOthersAsync(bidId, requestId, ct, save)
                ? Result<bool>.Success("پیشنهاد با موفقیت پذیرفته شد.")
                : Result<bool>.Failure("خطا در پذیرش پیشنهاد.");
        }

        public async Task<PagedResult<BidForRequestDto>> GetExpertBids(BidReqDto q, CancellationToken ct)
            => await bidRepo.GetExpertBids(q, ct);

        public async Task<Result<BidFullDto>> GetExpertBidForRequestAsync(int requestId, int expertId, CancellationToken ct)
        {
            var result = await bidRepo.GetExpertBidForRequestAsync(requestId, expertId, ct);
            return result is not null
                ? Result<BidFullDto>.Success("پیشنهاد شما برای این درخواست یافت شد", result)
                : Result<BidFullDto>.Failure("پیشنهادی برای این درخواست ثبت نشده است");
        }

        public async Task<Result<AddNewBidDto>> AddValidateAsync(AddNewBidDto dto, CancellationToken ct)
        {
            dto.Description = dto.Description is not null ? Validation.NormalizeText(dto.Description) : null;

            var existingBid = await GetExpertBidForRequestAsync(dto.RequestId, dto.ExpertId, ct);
            if (existingBid.Status == ResultStatusEnum.Success || existingBid.Data is not null)
                return Result<AddNewBidDto>.Warning("شما قبلاً پیشنهادی برای این درخواست ثبت کرده‌اید.");

            if (dto.ProposedVisitDateTime.Date <= DateTime.Today)
                return Result<AddNewBidDto>.Warning("تاریخ پیشنهادی باید حداقل از فردا باشد.");

            var requestResult = await requestRepo.GetRequestFullByIdAsync(dto.RequestId, ct);
            if (requestResult is null)
                return Result<AddNewBidDto>.Failure("درخواست مورد نظر یافت نشد.");

            if (requestResult.Status != RequestStatusEnum.Pending)
                return Result<AddNewBidDto>.Warning("این درخواست دیگر در وضعیت دریافت پیشنهاد نمی‌باشد.");

            var expertCityId = await appUserRepo.GetAppUserCityIdAsync(dto.ExpertId, ct);
            var customerCityId = await appUserRepo.GetAppUserCityIdAsync(requestResult.CustomerId, ct);

            if (expertCityId == null || customerCityId == null)
                return Result<AddNewBidDto>.Failure("خطا در دریافت اطلاعات مکان کاربر.");

            if (expertCityId != customerCityId)
                return Result<AddNewBidDto>.Warning("شهر شما با شهر مشتری یکسان نیست.");

            var expertInfo = await appUserRepo.GetAppUserFindRequestInfoByIdAsync(dto.ExpertId, ct);
            if (expertInfo is null)
                return Result<AddNewBidDto>.Failure("خطا در دریافت تخصص‌های کاربر.");

            if (!expertInfo.WorkId.Contains(requestResult.WorkId))
                return Result<AddNewBidDto>.Warning("این کار در لیست تخصص‌های شما قرار ندارد.");

            return Result<AddNewBidDto>.Success("اعتبارسنجی با موفقیت انجام شد.", dto);
        }

        public async Task<Result<bool>> AddAsync(AddNewBidDto dto, CancellationToken ct)
        {
            return await bidRepo.AddAsync(dto, ct)
                ? Result<bool>.Success("پیشنهاد شما با موفقیت ثبت شد")
                : Result<bool>.Failure("خطا در ثبت پیشنهاد");
        }

        public async Task<Result<bool>> DeleteValidateAsync(int requestId, int bidId, int expertId, CancellationToken ct)
        {
            var bid = await bidRepo.GetExpertBidForRequestAsync(requestId, expertId, ct);
            if(bid is null)
                return Result<bool>.Failure("شما پیشنهادی با این شناسه ندارید");
            if (bid.Status != BidStatusEnum.Pending)
                return Result<bool>.Warning("فقط در وضعیت در انتظار امکان حذف درخواست وجود دارد");
            if(bid.RequestId != requestId || bid.Id != bidId)
                return Result<bool>.Failure("این پیشنهاد متعلق به این درخواست نمی باشد");
            return Result<bool>.Success("تمام شرایط حذف درخواست امکان پذیر است");
        }

        public async Task<Result<bool>> DeleteAsync(int bidId, CancellationToken ct)
        {
            var result = await bidRepo.SoftDelete(bidId, ct);
            return result
                ? Result<bool>.Success("پیشنهاد با موفقیت حذف شد")
                : Result<bool>.Failure("حذف پیشنهاد موفقیت آمیز نبوده");
        }
    }
}
