using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Domain.Core.Contracts.Requests.AppServices
{
    public interface IReviewsAppServices
    {
        Task<IReadOnlyList<HomeViewReviewDto>> GetReviewsForHomePageAsync(ReviewReqDto request, CancellationToken ct);
        Task<PagedResult<ReviewFullDto>> GetReviewsListAsync(ReviewReqDto request, CancellationToken ct);
    }
}
