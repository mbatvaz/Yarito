using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Services.Requests
{
    public class ReviewsServices(
        IReviewRepo reviewRepo) : IReviewsServices
    {
        public async Task<bool> ChangeStatusAsync(int reviewId, ReviewStatusEnum newStatus, CancellationToken ct)
            => await reviewRepo.ChangeStatusAsync(reviewId, newStatus, ct);

        public async Task<IReadOnlyList<HomeViewReviewDto>> GetReviewsForHomePageAsync(ReviewReqDto request, CancellationToken ct)
            => await reviewRepo.GetReviewsForHomePageAsync(request, ct);

        public async Task<PagedResult<ReviewFullDto>> GetReviewsListAsync(ReviewReqDto request, CancellationToken ct)
            => await reviewRepo.GetReviewsListAsync(request, ct);
    }
}
