using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Requests.Services;

namespace Yarito.Domain.Services.Requests
{
    public class BidServices(
        IBidRepo bidRepo) : IBidServices
    {
        public async Task<int> GetCountAsync(CancellationToken ct) 
            => await bidRepo.GetCountAsync(ct);
    }
}
