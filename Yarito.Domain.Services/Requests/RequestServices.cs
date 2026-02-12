using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.Contracts.Users.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Framework;

namespace Yarito.Domain.Services.Requests
{
    public class RequestServices(
        IRequestRepo requestRepo,
        IBidServices bidServices,
        IAppUserServices appUserServices) : IRequestServices
    {
        public async Task<int> GetCountAsync(CancellationToken ct)
            => await requestRepo.GetCountAsync(ct);

        public void ClearChangeTracker() => requestRepo.ClearChangeTracker();

        public async Task<Result<RequestFullDto>> GetRequestFullByIdAsync(int requestId, CancellationToken ct)
        {
            var result = await requestRepo.GetRequestFullByIdAsync(requestId, ct);
            return result is not null
                ? Result<RequestFullDto>.Success("درخواست با موفقیت پیدا شد", result)
                : Result<RequestFullDto>.Failure("درخواستی پیدا نشد");
        }

        public async Task<Result<RequestSuccessDto>> GetRequestSuccessInfoByIdAsync(int requestId, CancellationToken ct)
        {
            var result = await requestRepo.GetRequestSuccessInfoByIdAsync(requestId, ct);
            return result is not null
                ? Result<RequestSuccessDto>.Success("درخواست با موفقیت ثبت شده است", result)
                : Result<RequestSuccessDto>.Failure("درخواست با موفقیت ثبت نشده");
        }

        public async Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q,
            CancellationToken ct)
            => await requestRepo.GetRequestsSummaryListAsync(q, ct);

        public async Task<PagedResult<RequestCardDto>> GetRequestsCardListAsync(RequestReqDto q, CancellationToken ct)
            => await requestRepo.GetRequestsCardListAsync(q, ct);

        public async Task<Result<bool>> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus,
            CancellationToken ct, bool save = true)
        {
            return await requestRepo.ChangeStatusAsync(requestId, newStatus, ct, save)
                ? Result<bool>.Success("وضعیت درخواست با موفقیت تغییر کرد.")
                : Result<bool>.Failure("خطا در تغییر وضعیت درخواست.");
        }

        public async Task<bool> SaveChangesAsync(CancellationToken ct) 
        => await requestRepo.SaveChangesAsync(ct);

        public Result<RequestNewDto> IsPropertyValid(RequestNewDto dto)
        {
            dto.Title = Validation.NormalizeText(dto.Title);
            dto.Description = dto.Description is not null ? Validation.NormalizeText(dto.Description) : null;
            dto.Address = dto.Address is not null ? Validation.NormalizeText(dto.Address) : null;

            if (!Validation.IsValidText(dto.Title, 100))
                return Result<RequestNewDto>.Warning("طول عنوان بیشتر از 100 کاراکتر است");
            if (dto.Description is not null && !Validation.IsValidText(dto.Description, 1000))
                return Result<RequestNewDto>.Warning("طول توضیحات نباید بیشتر از 1000 کاراکتر باشد");
            if (!dto.UseProfileAddress && (string.IsNullOrWhiteSpace(dto.Address) || !Validation.IsValidAddress(dto.Address)))
                return Result<RequestNewDto>.Warning("مقدار آدرس وارد شده معتبر نمی باشد");
            if (dto.Images.Count > 4)
                return Result<RequestNewDto>.Warning("فقط میتوانید 4 تصویر اپلود کنید");
            foreach (var i in dto.Images.Where(i => !Validation.IsValidImageUrlOrFileName(i.FileName)))
            {
                return Result<RequestNewDto>.Warning($"فرمت تصویر {i.FileName} معتبر نمی باشد");
            }

            if (dto.PreferredVisitDateTime is not null && dto.PreferredVisitDateTime <= DateTime.Today)
                return Result<RequestNewDto>.Warning("تاریخ انتخاب شده شما معتبر نمی باشد");

            return Result<RequestNewDto>.Success("تمام مقادیر وارد شده معتبر می باشد");
        }

        public async Task<Result<bool>> CountOfOpenRequestForCustomerIdAsync(int customerId, CancellationToken ct)
        {
            return await requestRepo.CountOfOpenRequestForCustomerIdAsync(customerId, ct) > 5 
                ? Result<bool>.Warning("شما بیش از 5 درخواست فعال دارید") 
                : Result<bool>.Success("تعداد درخواست شمت کمتر از حد نصاب است");
        }

        public Result<bool> IsProposedPriceAllow(decimal proposedPrice, WorkDto work)
        {
            var min = work.BasePrice * 0.5m;
            var max = work.BasePrice * 1.8m;

            if (proposedPrice < min || proposedPrice > max)
                return Result<bool>.Warning(
                    $"برای {work.Title} فقط می‌توانید بین {min:N0} و {max:N0} قیمت پیشنهاد دهید.".ToPersianNum());

            return Result<bool>.Success("قیمت پیشنهادی صحیح است");
        }

        public async Task<Result<int>> Add(RequestNewDto dto, CancellationToken ct)
        {
            var result = await requestRepo.AddAsync(dto, ct);
            return result == 0
                ? Result<int>.Failure("در ثبت درخواست خطایی رخ داد")
                : Result<int>.Success("درخواست با موفقیت ثبت شد", result);
        }

        public async Task<Result<BidFullDto>> CompletionValidationAsync(int requestId, int customerId, CancellationToken ct)
        {
            var requestResult = await GetRequestFullByIdAsync(requestId, ct);
            if (requestResult.Status != ResultStatusEnum.Success || requestResult.Data is null)
                return Result<BidFullDto>.Failure(requestResult.Message);

            var request = requestResult.Data;

            if (request.CustomerId != customerId)
                return Result<BidFullDto>.Failure("دسترسی غیرمجاز.");

            if (request.Status == RequestStatusEnum.Completed)
                return Result<BidFullDto>.Success("این درخواست قبلاً اتمام شده است.");

            if (request.Status != RequestStatusEnum.InProgress)
                return Result<BidFullDto>.Failure("فقط درخواست‌هایی که در وضعیت انجام هستند قابلیت اتمام دارند.");

            if (request.AcceptedBidId is null)
                return Result<BidFullDto>.Failure("برای این درخواست پیشنهاد پذیرفته‌شده‌ای وجود ندارد.");


            var bidResult = await bidServices.GetBidFullByIdAsync(request.AcceptedBidId.Value, ct);
            if (bidResult.Status != ResultStatusEnum.Success || bidResult.Data is null)
                return Result<BidFullDto>.Failure(bidResult.Message);

            var bid = bidResult.Data;

            if (bid.RequestId != requestId)
                return Result<BidFullDto>.Failure("پیشنهاد پذیرفته‌شده معتبر نیست.");

            return Result<BidFullDto>.Success("تغییر وضعیت به تکمیل ممکن است", bid);
        }

 


        public async Task<Result<bool>> CancelValidationAsync(int requestId, int customerId, CancellationToken ct)
        {
            var requestResult = await GetRequestFullByIdAsync(requestId, ct);
            if (requestResult.Status != ResultStatusEnum.Success || requestResult.Data is null)
                return Result<bool>.Failure(requestResult.Message);

            var request = requestResult.Data;

            if (request.CustomerId != customerId)
                return Result<bool>.Failure("دسترسی غیرمجاز.");

            if (request.Status == RequestStatusEnum.Cancelled)
                return Result<bool>.Success("این درخواست قبلاً لغو شده است.");

            if (request.Status != RequestStatusEnum.Pending)
                return Result<bool>.Failure("فقط درخواست‌هایی که در وضعیت انتظار هستند قابلیت لغو دارند.");

            if (request.AcceptedBidId is not null)
                return Result<bool>.Failure("این درخواست دارای پیشنهاد پذیرفته‌شده است و نمی‌تواند لغو شود.");

            return Result<bool>.Success("امکان لغو درخواست وجود دارد.");
        }

        public async Task<Result<BidFullDto>> AcceptBidValidationAsync(int requestId, int bidId, int customerId, CancellationToken ct)
        {
            var requestResult = await GetRequestFullByIdAsync(requestId, ct);
            if (requestResult.Status != ResultStatusEnum.Success || requestResult.Data is null)
                return Result<BidFullDto>.Failure(requestResult.Message);

            var request = requestResult.Data;

            if (request.CustomerId != customerId)
                return Result<BidFullDto>.Failure("دسترسی غیرمجاز.");

            if (request.Status != RequestStatusEnum.Pending)
                return Result<BidFullDto>.Failure("فقط درخواست‌هایی که در وضعیت انتظار هستند قابلیت پذیرش پیشنهاد دارند.");

            if (request.AcceptedBidId.HasValue)
                return Result<BidFullDto>.Failure("این درخواست در حال حاضر دارای یک پیشنهاد پذیرفته شده است.");

            var bidResult = await bidServices.GetBidFullByIdAsync(bidId, ct);
            if (bidResult.Status != ResultStatusEnum.Success || bidResult.Data is null)
                return Result<BidFullDto>.Failure("پیشنهاد مورد نظر یافت نشد.");

            var bid = bidResult.Data;
            if (bid.RequestId != requestId)
                return Result<BidFullDto>.Failure("این پیشنهاد متعلق به این درخواست نمی‌باشد.");

            if (bid.ProposedVisitDateTime.Date <= DateTime.Today)
                return Result<BidFullDto>.Failure("تاریخ مراجعه پیشنهادی باید حداقل برای روز بعد از امروز باشد.");

            var userResult = await appUserServices.GetAppUserSummaryByIdAsync(customerId, ct);
            if (userResult.Status != ResultStatusEnum.Success || userResult.Data is null)
                return Result<BidFullDto>.Failure("اطلاعات کاربر یافت نشد.");

            if (userResult.Data.WalletBalance < bid.ProposedPrice)
                return Result<BidFullDto>.Failure("موجودی حساب شما برای پذیرش این پیشنهاد کافی نیست.");

            return Result<BidFullDto>.Success("اعتبارسنجی با موفقیت انجام شد.", bid);
        }

        public async Task<Result<bool>> SetAcceptedBidIdAsync(int requestId, int bidId, CancellationToken ct, bool save = true)
        {
            return await requestRepo.AcceptBidAsync(requestId, bidId, ct, save)
                ? Result<bool>.Success("پیشنهاد با موفقیت به عنوان پیشنهاد پذیرفته شده ثبت شد.")
                : Result<bool>.Failure("خطا در ثبت پیشنهاد پذیرفته شده.");
        }

        public async Task<PagedResult<OpenRequestDto>> GetFineOpenRequestAsync(RequestReqDto q, CancellationToken ct) 
            => await requestRepo.GetFineOpenRequestAsync(q, ct);
    }
}
