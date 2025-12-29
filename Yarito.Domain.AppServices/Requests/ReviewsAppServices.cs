using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;

namespace Yarito.Domain.AppServices.Requests
{
    public class ReviewsAppServices(
        IReviewsServices reviewsServices) : IReviewsAppServices
    {
        public async Task<IReadOnlyList<HomePageReviewDto>>
            GetReviewsForHomePageAsync(ReviewReqDto request, CancellationToken ct)
            => await reviewsServices.GetReviewsForHomePageAsync(request, ct);
    }
}
