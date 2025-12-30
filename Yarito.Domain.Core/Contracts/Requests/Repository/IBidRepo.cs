using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.Contracts.Requests.Repository
{
    public interface IBidRepo
    {
        Task<int> GetCountAsync(CancellationToken ct);
        Task<bool> AddAsync(Bid newBid, CancellationToken ct);
        Task<bool> UpdateAsync(Bid newBid, CancellationToken ct);
        Task<bool> ChangeSingleStatusAsync(int bidId, BidStatusEnum newStatus, CancellationToken ct);
        Task<bool> ChangeMultipleStatusesAsync(List<int> bidIds, BidStatusEnum newStatus, CancellationToken ct);
    }
}
