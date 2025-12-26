using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Domain.Core.Entities.Users;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Users;
public class ExpertRepo(AppDbContext _db) : IExpertRepo
{
    public async Task<bool> AddAsync(Expert newExpert, CancellationToken ct)
    {
        _db.AppUsers.Add(newExpert);
        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> UpdateAsync(Expert newExpert, CancellationToken ct)
    {
        var dbExpert = await _db.AppUsers.OfType<Expert>()
            .FirstOrDefaultAsync(c => c.Id == newExpert.Id, ct);
        if (dbExpert is null) return false;

        dbExpert.CityId = newExpert.CityId;
        dbExpert.Email = newExpert.Email;
        dbExpert.FirstName = newExpert.FirstName;
        dbExpert.LastName = newExpert.LastName;
        dbExpert.ProfileImgPath = newExpert.ProfileImgPath;

        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> DeleteAsync(int expertId, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.AppUsers
            .Where(au => au.Id == expertId && au.IsDeleted == false)
            .ExecuteUpdateAsync(au => au
                .SetProperty(appUser => appUser.IsDeleted, true), ct) > 0;
    }
}
