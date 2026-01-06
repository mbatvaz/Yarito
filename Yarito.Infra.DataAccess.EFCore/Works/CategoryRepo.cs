using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Works;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Works;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Works;

public class CategoryRepo(AppDbContext _db) : ICategoryRepo
{
    #region Private Methods

    private IQueryable<Category> ApplyFilters(CategoryReqDto q)
    {
        var query = _db.Categories.AsNoTracking().AsQueryable();

        // Text search
        if (!string.IsNullOrWhiteSpace(q.TextSearch))
        {
            var ts = q.TextSearch.Trim();
            query = query.Where(c =>
                c.Title.Contains(ts)
                || (c.Description != null && c.Description.Contains(ts))
                || (c.Works.Any(w => w.Title.Contains(ts)))
            );
        }

        // Date filters
        if (q.From is not null)
            query = query.Where(c => c.CreatedAt >= q.From.Value);

        if (q.To is not null)
            query = query.Where(c => c.CreatedAt <= q.To.Value);

        // Sorting
        var sortBy = q.Sort?.SortBy ?? CategorySortableEnum.CreatedAt;
        var dir = q.Sort?.Direction ?? SortDirectionEnum.Descending;

        query = (sortBy, dir) switch
        {
            (CategorySortableEnum.Title, SortDirectionEnum.Ascending) => query.OrderBy(c => c.Title),
            (CategorySortableEnum.Title, SortDirectionEnum.Descending) => query.OrderByDescending(c => c.Title),
            (CategorySortableEnum.CreatedAt, SortDirectionEnum.Ascending) => query.OrderBy(c => c.CreatedAt),
            _ => query.OrderByDescending(c => c.CreatedAt),
        };

        return query;
    }

    #endregion

    #region Query Methods

    public async Task<CategoryDto?> GetByIdAsync(int categoryId, CancellationToken ct)
    {
        return await _db.Categories
            .Where(c => c.Id == categoryId)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IReadOnlyList<CategoryStringDataDto>> GetAllCategoriesNamesAsync(CancellationToken ct)
    {
        return await _db.Categories
            .Select(c => new CategoryStringDataDto
            {
                Title = c.Title,
                Description = c.Description,
                WorksTitle = c.Works.Select(w => w.Title).ToArray()
            })
            .ToArrayAsync(ct);
    }

    public async Task<IReadOnlyList<CategoryDto>> GetJustCategoriesListAsync(CancellationToken ct)
    {
        return await _db.Categories
            .AsNoTracking()
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Title = c.Title
            })
            .ToListAsync(ct);
    }

    public async Task<PagedResult<CategoryFullDto>> GetCategoriesListAsync(CategoryReqDto q, CancellationToken ct)
    {
        var query = ApplyFilters(q);
        var total = await query.CountAsync(ct);
        var skip = (q.Page - 1) * q.PageSize;

        var items = await query
            .Skip(skip)
            .Take(q.PageSize)
            .Select(c => new CategoryFullDto
            {
                Id = c.Id,
                CategoryTitle = c.Title,
                Description = c.Description,
                Works = c.Works.Select(w => new WorksFullDto
                {
                    Id = w.Id,
                    Title = w.Title,
                    BasePrice = w.BasePrice
                }).ToList()
            })
            .ToListAsync(ct);

        return new PagedResult<CategoryFullDto>
        {
            Items = items,
            Page = q.Page,
            PageSize = q.PageSize,
            TotalCount = total
        };
    }

    #endregion

    #region Command Methods

    public async Task<bool> AddAsync(CategoryDto newCategory, CancellationToken ct)
    {
        _db.Categories.Add(new Category
        {
            Title = newCategory.Title,
            Description = newCategory.Description
        });
        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> UpdateAsync(CategoryDto category, CancellationToken ct)
    {
        var dbCategory = await _db.Categories.FindAsync([category.Id], ct);
        if (dbCategory is null) return false;

        dbCategory.Title = category.Title;
        dbCategory.Description = category.Description;

        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> DeleteAsync(int categoryId, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.Categories
            .Where(c => c.Id == categoryId && c.IsDeleted == false)
            .ExecuteUpdateAsync(c => c.SetProperty(category => category.IsDeleted, true), ct) > 0;
    }

    #endregion

    #region Validation Methods

    public async Task<bool> IsTitleExistsAsync(string title, CancellationToken ct)
    {
        return await _db.Categories.AnyAsync(c => c.Title == title, ct);
    }

    public async Task<bool> IsTitleExistsAsync(string title, int excludeId, CancellationToken ct)
    {
        return await _db.Categories.AnyAsync(c => c.Id != excludeId && c.Title == title, ct);
    }

    #endregion
}
