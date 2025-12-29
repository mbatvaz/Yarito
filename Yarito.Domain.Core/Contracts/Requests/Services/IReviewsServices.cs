using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.DTOs.Requests;

namespace Yarito.Domain.Core.Contracts.Requests.Services
{
    public interface IReviewsServices
    {
        Task<IReadOnlyList<HomePageReviewDto>>
            GetReviewsForHomePageAsync(ReviewReqDto request, CancellationToken ct);
    }
}