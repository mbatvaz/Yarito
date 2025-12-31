using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Users;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Users;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Users
{
    public class AppUserRepo(AppDbContext _db) : IAppUserRepo
    {
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

        public async Task<string?> GetFullNameByIdAsync(int userId, CancellationToken ct)
        {
            return await _db.AppUsers.Where(c => c.Id == userId)
                .Select(c => $"{c.FirstName} {c.LastName}")
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
                    LastName = dto.LastName
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
            user.ProfileImgPath = "/Images/Profile/default.png";

            _db.AppUsers.Add(user);
            return await _db.SaveChangesAsync(ct) > 0;
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
            var rows = await _db.Set<Expert>()
                .AsNoTracking()
                .Where(e => e.Id == expertId)
                .SelectMany(e => 
                    e.Works.Select(w => 
                        new
                        {
                            CategoryId = w.CategoryId,
                            CategoryTitle = w.Category.Title,
                            CategoryDescription = w.Category.Description,

                            WorkId = w.Id,
                            WorkTitle = w.Title,
                            WorkBasePrice = w.BasePrice
                        }
                    ))
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

        public async Task<bool> SoftDeleteAsync(int userId, CancellationToken ct)
        {
            return await _db.AppUsers
                .Where(u => u.Id == userId)
                .ExecuteUpdateAsync(s
                    => s.SetProperty(u => u.IsDeleted, true), ct) > 0;
        }
    }
}
