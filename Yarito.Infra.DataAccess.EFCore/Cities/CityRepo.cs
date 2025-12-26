using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Cities.Repository;
using Yarito.Domain.Core.Entities.Cities;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Cities;

public class CityRepo(AppDbContext _db) : ICityRepo
{
    public async Task<bool> AddAsync(City newCity, CancellationToken ct)
    {
        _db.Cities.Add(newCity);
        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> UpdateAsync(City newCity, CancellationToken ct)
    {
        var dbCity = await _db.Cities.FindAsync([newCity.Id], ct);
        if (dbCity is null) return false;
        
        dbCity.Name = newCity.Name;
        dbCity.ParentId = newCity.ParentId;

        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> DeleteAsync(int cityId, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.Cities
            .Where(c => c.Id == cityId && c.IsDeleted == false)
            .ExecuteUpdateAsync(c => c
                .SetProperty(city => city.IsDeleted, true), ct) > 0;
    }
}
