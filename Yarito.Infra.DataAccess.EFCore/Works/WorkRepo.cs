using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities.Works;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Works;

public class WorkRepo(AppDbContext _db) : IWorkRepo
{
    #region Query Methods

    public async Task<WorkDto?> GetByIdAsync(int workId, CancellationToken ct)
    {
        return await _db.Works
            .Where(w => w.Id == workId)
            .Select(w => new WorkDto
            {
                Id = w.Id,
                Title = w.Title,
                BasePrice = w.BasePrice,
                CategoryId = w.CategoryId
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<List<WorksFullDto>> GetWorksByIDs(List<int> ids, CancellationToken ct)
    {
        return await _db.Works
            .Where(w => ids.Contains(w.Id))
            .Select(w => new WorksFullDto
            {
                Id = w.Id,
                Title = w.Title,
                BasePrice = w.BasePrice
            })
            .ToListAsync(ct);
    }

    #endregion

    #region Command Methods

    public async Task<bool> AddAsync(WorkDto newWork, CancellationToken ct)
    {
        _db.Works.Add(new Work
        {
            Title = newWork.Title,
            BasePrice = newWork.BasePrice,
            CategoryId = newWork.CategoryId
        });
        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> UpdateAsync(WorkDto work, CancellationToken ct)
    {
        var dbWork = await _db.Works.FindAsync([work.Id], ct);
        if (dbWork is null) return false;

        dbWork.Title = work.Title;
        dbWork.BasePrice = work.BasePrice;
        dbWork.CategoryId = work.CategoryId;

        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> DeleteAsync(int workId, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.Works
            .Where(w => w.Id == workId && w.IsDeleted == false)
            .ExecuteUpdateAsync(w => w.SetProperty(work => work.IsDeleted, true), ct) > 0;
    }

    #endregion

    #region Validation Methods

    public async Task<bool> IsTitleExistsAsync(string title, CancellationToken ct)
    {
        return await _db.Works.AnyAsync(w => w.Title == title, ct);
    }

    public async Task<bool> IsTitleExistsAsync(string title, int excludeId, CancellationToken ct)
    {
        return await _db.Works.AnyAsync(w => w.Id != excludeId && w.Title == title, ct);
    }

    public async Task<bool> IsCategoryInUseAsync(int categoryId, CancellationToken ct)
    {
        return await _db.Works.AnyAsync(w => w.CategoryId == categoryId, ct);
    }

    #endregion
}
