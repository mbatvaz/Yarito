using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Requests;
public class BidRepo(AppDbContext _db) : IBidRepo
{
    public async Task<bool> AddAsync(Bid newBid, CancellationToken ct)
    {
        _db.Bids.Add(newBid);
        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> UpdateAsync(Bid newBid, CancellationToken ct)
    {
        var dbBid = await _db.Bids.FindAsync([newBid.Id], ct);
        if (dbBid is null) return false;

        dbBid.ProposedPrice = newBid.ProposedPrice;
        dbBid.Description = newBid.Description;
        dbBid.ProposedVisitDateTime = newBid.ProposedVisitDateTime;

        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> ChangeSingleStatusAsync(int bidId, BidStatusEnum newStatus, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.Bids
            .Where(b => b.Id == bidId && b.Status != newStatus)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Status, newStatus),
                ct) > 0;
    }

    public async Task<bool> ChangeMultipleStatusesAsync(List<int> bidIds, BidStatusEnum newStatus, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.Bids
            .Where(b => bidIds.Contains(b.Id) && b.Status != newStatus)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Status, newStatus),
                ct) > 0;
    }
}
