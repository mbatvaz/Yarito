using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Domain.Core.DTOs._Common;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Entities.Users;
using Yarito.Domain.Core.Enums.Users;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Users
{
    public class AppUserRepo(AppDbContext _db) : IAppUserRepo
    {
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
            user.ProfileImgPath = @"\Images\Profile\default.png";

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
    }
}
