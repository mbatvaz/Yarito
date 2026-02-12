using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Users;
using Yarito.Domain.Core.Entities.Works;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Users;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Users;

public class AppUserRepo(AppDbContext _db) : IAppUserRepo
{
    #region Private Methods

    private IQueryable<AppUser> ApplyingFiltersToQueries(AppUserReqDto q)
    {
        var query = _db.AppUsers.AsNoTracking().AsQueryable();

        //UserType filters
        if (q.UserType is not null)
        {
            query = q.UserType.Value switch
            {
                UserTypeEnum.Customer => query.Where(u => u is Customer),
                UserTypeEnum.Expert => query.Where(u => u is Expert),
                _ => query
            };
        }

        // City filter
        if (q.CityId is not null)
            query = query.Where(u => u.CityId == q.CityId.Value);

        // Text search
        if (!string.IsNullOrWhiteSpace(q.TextSearch))
        {
            var ts = q.TextSearch.Trim();

            query = query.Where(u =>
                u.FirstName.Contains(ts) ||
                u.LastName.Contains(ts) ||
                u.PhoneNumber.Contains(ts) ||
                (u.Email != null && u.Email.Contains(ts)) ||
                (u.City != null && u.City.Name.Contains(ts))
            );
        }

        // Sorting
        var sortBy = q.Sort?.SortBy ?? AppUserSortableEnum.Id;
        var dir = q.Sort?.Direction ?? SortDirectionEnum.Descending;

        query = (sortBy, dir) switch
        {
            (AppUserSortableEnum.CreatedAt, SortDirectionEnum.Ascending)
                => query.OrderBy(r => r.CreatedAt),

            (AppUserSortableEnum.CreatedAt, SortDirectionEnum.Descending)
                => query.OrderByDescending(r => r.CreatedAt),

            (AppUserSortableEnum.Id, SortDirectionEnum.Ascending)
                => query.OrderBy(r => r.Id),

            _ => query.OrderByDescending(r => r.Id),
        };

        return query;
    }

    #endregion

    #region Query Methods

    public async Task<AppUserSummaryDto?> GetAppUserSummaryByIdAsync(int userId, CancellationToken ct)
    {
        return await _db.AppUsers.Where(c => c.Id == userId)
            .Select(au => new AppUserSummaryDto()
            {
                Id = au.Id,
                FirstName = au.FirstName,
                LastName = au.LastName,
                ProfileImgPath = au.ProfileImgPath,
                PhoneNumber = au.PhoneNumber,
                WalletBalance = au.WalletBalance,
                IsInfoComplete = au.CityId.HasValue,
                UserType = au is Customer ? UserTypeEnum.Customer : UserTypeEnum.Expert
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<bool> AddAsync(RegisterDto dto, CancellationToken ct)
    {
        AppUser user = dto.UserType switch
        {
            UserTypeEnum.Customer => new Customer
            {
                PhoneNumber = dto.PhoneNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address
            },
            UserTypeEnum.Expert => new Expert
            {
                PhoneNumber = dto.PhoneNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            },
            _ => throw new Exception()
        };

        user.Id = dto.Id;
        user.ProfileImgPath = dto.ProfileImageUrl ?? "/Images/Profile/default.png";
        user.CityId = dto.CityId;
        user.Email = dto.Email;
        user.WalletBalance = dto.BaseWalletBalance;

        _db.AppUsers.Add(user);
        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> UpdateAsync(AppUserUpdateDto dto, CancellationToken ct)
    {
        var user = await _db.AppUsers.FirstOrDefaultAsync(u => u.Id == dto.UserId, ct);
        if (user == null) return false;

        if (!string.IsNullOrEmpty(dto.FirstName)) user.FirstName = dto.FirstName;
        if (!string.IsNullOrEmpty(dto.LastName)) user.LastName = dto.LastName;
        if (dto.Email != null) user.Email = dto.Email;
        if (dto.CityId.HasValue) user.CityId = dto.CityId.Value;
        if (!string.IsNullOrEmpty(dto.ProfileImgPath)) user.ProfileImgPath = dto.ProfileImgPath;

        if (user is Customer customer && !string.IsNullOrEmpty(dto.Address))
        {
            customer.Address = dto.Address;
        }

        if (user is Expert && dto.WorkIds is not null)
        {
            var existingWorks = _db.ExpertWorks.Where(ew => ew.ExpertId == dto.UserId);
            _db.ExpertWorks.RemoveRange(existingWorks);

            foreach (var workId in dto.WorkIds)
            {
                _db.ExpertWorks.Add(new ExpertWork { ExpertId = dto.UserId, WorkId = workId });
            }
        }

        await _db.SaveChangesAsync(ct);
        return true;
    }


    public async Task<AppUserStaticsDto?> GetUserCountAsync(CancellationToken ct)
    {
        return await _db.AppUsers.AsNoTracking()
            .GroupBy(ap => 1)
            .Select(us => new AppUserStaticsDto
            {
                NumberOfCustomers = us.Count(c => c is Customer),
                NumberOfExperts = us.Count(e => e is Expert)
            }).FirstOrDefaultAsync(ct);
    }

    public async Task<PagedResult<AppUserSummaryDto>> GetAppUserSummaryListAsync(AppUserReqDto q, CancellationToken ct)
    {
        var query = ApplyingFiltersToQueries(q);
        var total = await query.CountAsync(ct);
        var skip = (q.Page - 1) * q.PageSize;
        var items = await query
            .Skip(skip)
            .Take(q.PageSize)
            .Select(au => new AppUserSummaryDto()
            {
                Id = au.Id,
                FirstName = au.FirstName,
                LastName = au.LastName,
                ProfileImgPath = au.ProfileImgPath,
                PhoneNumber = au.PhoneNumber,
                WalletBalance = au.WalletBalance,
                IsInfoComplete = au.CityId.HasValue,
                UserType = au is Customer ? UserTypeEnum.Customer : UserTypeEnum.Expert
            }).ToListAsync(ct);

        return new PagedResult<AppUserSummaryDto>
        {
            Items = items,
            Page = q.Page,
            PageSize = q.PageSize,
            TotalCount = total
        };
    }

    public async Task<AppUserFullDto?> GetAppUserFullByIdAsync(int userId, CancellationToken ct)
    {
        return await _db.AppUsers.AsNoTracking()
            .Where(au => au.Id == userId)
            .Select(au => new AppUserFullDto
            {
                Id = au.Id,
                FirstName = au.FirstName,
                LastName = au.LastName,
                PhoneNumber = au.PhoneNumber,
                Email = au.Email,
                CityName = au.City != null ? au.City.Name : null,
                UserType = au is Customer ? UserTypeEnum.Customer : UserTypeEnum.Expert,
                CreatedAt = au.CreatedAt,
                ProfileImgPath = au.ProfileImgPath ?? "/Images/Profile/default.PNG",
                WalletBalance = au.WalletBalance,
                Address = au is Customer ? ((Customer)au).Address : null
            }).FirstOrDefaultAsync(ct);
    }

    public async Task<IReadOnlyList<CategoryFullDto>> GetExpertCategoryWorksListDto(int expertId, CancellationToken ct)
    {
        var rows = await _db.ExpertWorks
            .AsNoTracking()
            .Where(ew => ew.ExpertId == expertId)
            .Select(ew => new
            {
                CategoryId = ew.Work.CategoryId,
                CategoryTitle = ew.Work.Category.Title,
                CategoryDescription = ew.Work.Category.Description,

                WorkId = ew.WorkId,
                WorkTitle = ew.Work.Title,
                WorkBasePrice = ew.Work.BasePrice
            })
            .ToListAsync(ct);

        var result = rows
            .GroupBy(x => new { x.CategoryId, x.CategoryTitle, x.CategoryDescription })
            .Select(g => new CategoryFullDto
            {
                Id = g.Key.CategoryId,
                CategoryTitle = g.Key.CategoryTitle,
                Description = g.Key.CategoryDescription,
                Works = g
                    .GroupBy(w => w.WorkId)
                    .Select(wg => new WorksFullDto
                    {
                        Id = wg.Key,
                        Title = wg.First().WorkTitle,
                        BasePrice = wg.First().WorkBasePrice
                    })
                    .ToList()
            })
            .ToList();

        return result;
    }

    #endregion

    #region Command Methods

    public async Task<bool> SoftDeleteAsync(int userId, CancellationToken ct, bool save)
    {
        var user = await _db.AppUsers.FindAsync([userId], ct);
        if (user is null || user.IsDeleted) return false;

        user.IsDeleted = true;

        if (save)
            return await _db.SaveChangesAsync(ct) > 0;

        return true;
    }

    #endregion

    #region Validation Methods

    public async Task<bool> IsEmailExistsAsync(string email, CancellationToken ct)
    {
        return await _db.AppUsers.AnyAsync(u => u.Email == email, ct);
    }

    public async Task<bool> IsEmailExistsAsync(string email, int excludeId, CancellationToken ct)
    {
        return await _db.AppUsers.AnyAsync(u => u.Id != excludeId && u.Email == email, ct);
    }

    public async Task<bool> IsExistsAsync(int userId, CancellationToken ct)
    {
        return await _db.AppUsers.AnyAsync(u => u.Id == userId, ct);
    }

    public async Task<bool> IsCitySetAsync(int userId, CancellationToken ct)
    {
        return await _db.AppUsers.AnyAsync(u => u.Id == userId && u.City != null, ct);
    }

    public async Task<int?> GetAppUserCityIdAsync(int userId, CancellationToken ct)
    {
        return await _db.AppUsers
            .Where(u => u.Id == userId)
            .Select(u => u.CityId)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<string?> GetCustomerAddressAsync(int userId, CancellationToken ct)
    {
        return await _db.AppUsers
            .OfType<Customer>()
            .Where(u => u.Id == userId)
            .Select(u => u.Address)
            .FirstOrDefaultAsync(ct);
    }
    #endregion

    public async Task<bool> IncreaseWalletBalanceAsync(int userId, decimal amount, CancellationToken ct, bool save)
    {
        var user = await _db.AppUsers.FirstOrDefaultAsync(u => u.Id == userId, ct);
        if (user is null) return false;

        user.WalletBalance += amount;

        if (save)
            return await _db.SaveChangesAsync(ct) > 0;

        return true;
    }

    public async Task<bool> DecreaseWalletBalanceAsync(int userId, decimal amount, CancellationToken ct, bool save)
    { 
        var user = await _db.AppUsers.FirstOrDefaultAsync(u => u.Id == userId, ct);
        if (user is null) return false;
        if (user.WalletBalance < amount) return false;

        user.WalletBalance -= amount;

        if (save)
            return await _db.SaveChangesAsync(ct) > 0;

        return true;
    }

    public async Task<UserDashboardDto?> GetAppUserDashboardByIdAsync(int userId, CancellationToken ct)
    {
        return await _db.AppUsers.AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => new UserDashboardDto
            {
                WalletBalance = u.WalletBalance,
                IsInfoComplete = u.CityId.HasValue
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<ExpertFindRequestInfoDto?> GetAppUserFindRequestInfoByIdAsync(int appUserId, CancellationToken ct)
    {
        return await _db.AppUsers
            .OfType<Expert>()
            .AsNoTracking()
            .Where(au => au.Id == appUserId)
            .Select(au => new ExpertFindRequestInfoDto()
            {
                CityId = au.CityId,
                WorkId = au.ExpertWorks.Select(w => w.WorkId).ToList()
            }).FirstOrDefaultAsync(ct);
    }
}
