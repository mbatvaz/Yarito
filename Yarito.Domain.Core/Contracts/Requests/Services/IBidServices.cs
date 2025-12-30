namespace Yarito.Domain.Core.Contracts.Requests.Services
{
    public interface IBidServices
    {
        Task<int> GetCountAsync(CancellationToken ct);
    }
}
