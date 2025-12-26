using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.Entities.Works;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Works;
public class WorkRepo(AppDbContext _db) : IWorkRepo
{
    public async Task<bool> AddAsync(Work newWork, CancellationToken ct)
    { 
        _db.Works.Add(newWork);
        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> UpdateAsync(Work newWork, CancellationToken ct)
    {
        var dbWork = await _db.Works.FindAsync([newWork.Id], ct);
        if (dbWork is null) return false;

        dbWork.BasePrice = newWork.BasePrice;
        dbWork.Title = newWork.Title;

        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> DeleteAsync(int workId, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.Works
            .Where(w => w.Id == workId && w.IsDeleted == false)
            .ExecuteUpdateAsync(w => w
                .SetProperty(work => work.IsDeleted, true), ct) > 0;
    }
}
