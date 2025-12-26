using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Domain.Core.Contracts.Requests.Repository
{
    public interface IRequestRepo
    {
        Task<bool> AddAsync(Request newRequest, CancellationToken ct);
        Task<bool> UpdateAsync(Request newRequest, CancellationToken ct);
        Task<bool> AcceptBidAsync(int requestId, int bidId, CancellationToken ct);
        Task<bool> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus, CancellationToken ct);
    }
}
