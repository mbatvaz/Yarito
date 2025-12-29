using Yarito.Domain.Core.DTOs.Requests;

namespace Yarito.Domain.Core.Contracts.Requests.AppServices
{
    public interface IReviewsAppServices
    {
        Task<IReadOnlyList<HomePageReviewDto>>
            GetReviewsForHomePageAsync(ReviewReqDto request, CancellationToken ct);
    }
}
