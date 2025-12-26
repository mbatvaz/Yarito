using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Domain.Core.Entities.Users;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Users;

public class CustomerRepo(AppDbContext _db) : ICustomerRepo
{
    public async Task<bool> AddAsync(Customer newCustomer, CancellationToken ct)
    {
        _db.AppUsers.Add(newCustomer);
        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> UpdateAsync(Customer newCustomer, CancellationToken ct)
    {
        var dbCustomer = await _db.AppUsers.OfType<Customer>()
            .FirstOrDefaultAsync(c => c.Id == newCustomer.Id, ct);
        if (dbCustomer is null) return false;

        dbCustomer.CityId = newCustomer.CityId;
        dbCustomer.Email = newCustomer.Email;
        dbCustomer.FirstName = newCustomer.FirstName;
        dbCustomer.LastName = newCustomer.LastName;
        dbCustomer.ProfileImgPath = newCustomer.ProfileImgPath;
        dbCustomer.Address = newCustomer.Address;

        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> DeleteAsync(int customerId, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.AppUsers
            .Where(au => au.Id == customerId && au.IsDeleted == false)
            .ExecuteUpdateAsync(au => au
                .SetProperty(appUser => appUser.IsDeleted, true), ct) > 0;
    }
}
