using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Domain.Core.Contracts.Users.Repository
{
    public interface ICustomerRepo
    {
        Task<bool> AddAsync(Customer newCustomer, CancellationToken ct);
        Task<bool> UpdateAsync(Customer newCustomer, CancellationToken ct);
        Task<bool> DeleteAsync(int customerId, CancellationToken ct);
    }
}
