using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Framework;

namespace Yarito.Domain.Services.Requests
{
    public class RequestServices(
        IRequestRepo requestRepo) : IRequestServices
    {
        public async Task<int> GetCountAsync(CancellationToken ct)
            => await requestRepo.GetCountAsync(ct);

        public async Task<RequestFullDto?> GetRequestFullByIdAsync(int requestId, CancellationToken ct)
            => await requestRepo.GetRequestFullByIdAsync(requestId, ct);

        public async Task<RequestSuccessDto?> GetRequestSuccessInfoByIdAsync(int requestId, CancellationToken ct)
            => await requestRepo.GetRequestSuccessInfoByIdAsync(requestId, ct);

        public async Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q,
            CancellationToken ct)
            => await requestRepo.GetRequestsSummaryListAsync(q, ct);

        public async Task<PagedResult<RequestCardDto>> GetRequestsCardListAsync(RequestReqDto q, CancellationToken ct)
            => await requestRepo.GetRequestsCardListAsync(q, ct);

        public async Task<Result<bool>> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus,
            CancellationToken ct)
        {
            return await requestRepo.ChangeStatusAsync(requestId, newStatus, ct)
                ? Result<bool>.Success("وضعیت درخواست با موفقیت تغییر کرد.")
                : Result<bool>.Failure("خطا در تغییر وضعیت درخواست.");
        }

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
    }
}
