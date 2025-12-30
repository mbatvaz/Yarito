using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.AppServices.Requests
{
    public class ReviewsAppServices(
        IReviewsServices reviewsServices) : IReviewsAppServices
    {
        public async Task<IReadOnlyList<HomeViewReviewDto>>
            GetReviewsForHomePageAsync(ReviewReqDto request, CancellationToken ct)
            => await reviewsServices.GetReviewsForHomePageAsync(request, ct);

        public async Task<PagedResult<ReviewFullDto>>
            GetReviewsListAsync(ReviewReqDto request, CancellationToken ct)
            => await reviewsServices.GetReviewsListAsync(request, ct);
    }
}
