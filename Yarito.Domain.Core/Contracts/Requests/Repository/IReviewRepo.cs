using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.Contracts.Requests.Repository
{
    public interface IReviewRepo
    {
        Task<bool> AddAsync(Review newReview, CancellationToken ct);
        Task<bool> UpdateAsync(Review newReview, CancellationToken ct);
        Task<bool> DeleteAsync(int reviewId, CancellationToken ct);
        Task<bool> ChangeStatusAsync(int reviewId, ReviewStatusEnum newStatus, CancellationToken ct);
        Task<IReadOnlyList<HomePageReviewDto>> GetReviewsForHomePageAsync(ReviewReqDto q, CancellationToken ct);

    }
}
