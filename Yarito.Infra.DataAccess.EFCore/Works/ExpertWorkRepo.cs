using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Works
{
    public class ExpertWorkRepo(AppDbContext _db) : IExpertWorkRepo
    {
        public async Task<IReadOnlyList<WorksFullDto>> GetExpertWorks(int expertId, CancellationToken ct)
        {
            return await _db.ExpertWorks
                .AsNoTracking()
                .Where(ew => ew.ExpertId == expertId)
                .Select(w => new WorksFullDto()
                {
                    Id = w.WorkId,
                    Title = w.Work.Title,
                    BasePrice = w.Work.BasePrice
                }).ToListAsync(ct);
        }

        public async Task<bool> HasExpertWork(int expertId, int workId, CancellationToken ct)
            => await _db.ExpertWorks.AnyAsync(ew => ew.ExpertId == expertId && ew.WorkId == workId, ct);
    }
}