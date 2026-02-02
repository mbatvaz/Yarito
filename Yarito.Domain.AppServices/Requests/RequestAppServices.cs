using Yarito.Domain.Core.Contracts._Common.Services;
using Yarito.Domain.Core.Contracts.Images.Services;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.Contracts.Users.Services;
using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.AppServices.Requests
{
    public class RequestAppServices(
        IRequestServices requestServices,
        IAppUserServices appUserServices,
        IFileServices fileServices,
        IImageServices imageServices,
        IBidServices bidServices,
        IWorkServices workServices) : IRequestAppServices
    {
        #region Query Methods

        public async Task<Result<RequestFullDto>> GetRequestFullByIdAsync(int requestId, CancellationToken ct)
            => await requestServices.GetRequestFullByIdAsync(requestId, ct);

        public async Task<Result<RequestSuccessDto>> GetRequestSuccessInfoByIdAsync(int requestId, CancellationToken ct)
            => await requestServices.GetRequestSuccessInfoByIdAsync(requestId, ct);

        public async Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q, CancellationToken ct)
            => await requestServices.GetRequestsSummaryListAsync(q, ct);

        public async Task<PagedResult<RequestCardDto>> GetRequestsCardListAsync(RequestReqDto q, CancellationToken ct)
            => await requestServices.GetRequestsCardListAsync(q, ct);

        #endregion

        #region Command Methods

        public async Task<Result<bool>> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus, CancellationToken ct)
            => await requestServices.ChangeStatusAsync(requestId, newStatus, ct);

        #endregion

        public async Task<Result<int>> AddNewRequest(RequestNewDto dto, CancellationToken ct)
        {
            var validationResult = requestServices.IsPropertyValid(dto);
            if (validationResult.Status != ResultStatusEnum.Success)
                return Result<int>.Warning(validationResult.Message);

            var requestCountResult = await requestServices.CountOfOpenRequestForCustomerIdAsync(dto.UserId, ct);
            if(requestCountResult.Status != ResultStatusEnum.Success)
                return Result<int>.Warning(requestCountResult.Message);

            var workValidationResult = await workServices.GetByIdAsync(dto.WorkId, ct);
            if (workValidationResult.Status != ResultStatusEnum.Success || workValidationResult.Data is null)
                return Result<int>.Warning(workValidationResult.Message);

            var work = workValidationResult.Data;
            var finalProposedPrice = dto.ProposedPrice ?? work.BasePrice;
            if (dto.ProposedPrice.HasValue)
            {
                var priceCheck = requestServices.IsProposedPriceAllow(finalProposedPrice, work);
                if (priceCheck.Status != ResultStatusEnum.Success)
                    return Result<int>.Warning(priceCheck.Message);
            }
            dto.ProposedPrice = finalProposedPrice;

            if (dto.UseProfileAddress)
            {
                var addressResult = await appUserServices.GetCustomerAddressAsync(dto.UserId, ct);
                if(addressResult.Status != ResultStatusEnum.Success || addressResult.Data is null)
                    return Result<int>.Warning(addressResult.Message);
                dto.Address = addressResult.Data;
            }
            
            var addNewRequestResult = await requestServices.Add(dto, ct);
            var failureMessage = addNewRequestResult.Message;
            try
            {
                if (addNewRequestResult.Status != ResultStatusEnum.Success)
                    return Result<int>.Failure(addNewRequestResult.Message);

                if (dto.Images.Count == 0)
                    return Result<int>.Success(addNewRequestResult.Message, addNewRequestResult.Data);

                foreach (var i in dto.Images)
                {
                    i.FileName = await fileServices.SaveImageOnDiskAsync(i.ImageStream, "Images/Request", i.FileFormat, ct);
                }

                var addRequestImageResult = await imageServices.AddRangeAsync(dto.Images, addNewRequestResult.Data, ct);
                if (addRequestImageResult.Status != ResultStatusEnum.Success)
                {
                    failureMessage = addRequestImageResult.Message;
                    throw new Exception();
                }

                return Result<int>.Success(addNewRequestResult.Message, addNewRequestResult.Data);
            }
            catch (Exception ex)
            {
                await requestServices.ChangeStatusAsync(addNewRequestResult.Data, RequestStatusEnum.Cancelled, ct);
                foreach (var i in dto.Images)
                {
                    await fileServices.DeleteImageOnDiskAsync(i.FileName, ct);
                }

                await imageServices.RemoveRequestImagesAsync(addNewRequestResult.Data, ct);
                return Result<int>.Failure(failureMessage);
            }
        }

        public async Task<Result<bool>> CompletionAsync(int requestId, int customerId, CancellationToken ct)
        {
            var acceptedBid = await requestServices.CompletionValidationAsync(requestId, customerId, ct);
            if (acceptedBid.Status != ResultStatusEnum.Success || acceptedBid.Data is null)
                return Result<bool>.Failure(acceptedBid.Message);

            try
            {
                var changStatusResult = await requestServices.ChangeStatusAsync(requestId, RequestStatusEnum.Completed, ct, false); 

                if (changStatusResult.Status != ResultStatusEnum.Success)
                    return Result<bool>.Failure(changStatusResult.Message); 

                var balance = acceptedBid.Data.ProposedPrice * (decimal)0.9;

                var feeResult = await appUserServices.DecreaseWalletBalanceAsync(11, balance, ct, false);
                if (feeResult.Status != ResultStatusEnum.Success)
                    throw new Exception();

                var result = await appUserServices.IncreaseWalletBalanceAsync(acceptedBid.Data.ExpertId, balance, ct, true);

                return result.Status == ResultStatusEnum.Success
                    ? Result<bool>.Success("وضعیت درخواست شما به اتمام تغییر یافت")
                    : throw new Exception();
            }
            catch (Exception ex)
            {
                requestServices.ClearChangeTracker(); 
                return Result<bool>.Failure("در هنگام تغییر وضعیت خطایی رخ داد، لطفا بعدا تلاش کنید");
            }
        }
    }
}
