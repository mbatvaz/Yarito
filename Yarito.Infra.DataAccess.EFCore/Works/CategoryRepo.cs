using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.Entities.Works;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Works;
public class CategoryRepo(AppDbContext _db) : ICategoryRepo
{
    public async Task<bool> AddAsync(Category newCategory, CancellationToken ct)
    {
        _db.Categories.Add(newCategory);
        return await _db.SaveChangesAsync(ct) > 0;
    }
    public async Task<bool> DeleteAsync(int categoryId, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.Categories
            .Where(c => c.Id == categoryId && c.IsDeleted == false)
            .ExecuteUpdateAsync(c => c
                .SetProperty(category => category.IsDeleted, true), ct) > 0;
    }
}
