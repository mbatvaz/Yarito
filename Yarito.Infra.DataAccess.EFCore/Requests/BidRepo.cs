using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Requests;
public class BidRepo(AppDbContext _db) : IBidRepo
{
    private IQueryable<Bid> ApplyFilters(BidReqDto q)
    {
        var query = _db.Bids.AsNoTracking().AsQueryable();

        // Status filter
        if (q.Status is not null)
            query = query.Where(b => b.Status == q.Status);

        // Extra filters
        if (q.ExpertId is not null)
            query = query.Where(b => b.ExpertId == q.ExpertId);

        if (q.RequestId is not null)
            query = query.Where(b => b.RequestId == q.RequestId.Value);

        // Date filters
        if (q.From is not null)
            query = query.Where(b => b.CreatedAt >= q.From.Value);

        if (q.To is not null)
            query = query.Where(b => b.CreatedAt <= q.To.Value);

        // Text search
        if (!string.IsNullOrWhiteSpace(q.TextSearch))
        {
            var ts = q.TextSearch.Trim();

            query = query.Where(b =>
                (b.Description != null && b.Description.Contains(ts)) 
                || b.Request.Title.Contains(ts)
                || b.Request.Work.Title.Contains(ts)
            );
        }

        // Sorting
        var sortBy = q.Sort?.SortBy ?? BidSortableEnum.CreatedAt;
        var dir = q.Sort?.Direction ?? SortDirectionEnum.Descending;

        query = (sortBy, dir) switch
        {
            (BidSortableEnum.ProposedPrice, SortDirectionEnum.Ascending)
                => query.OrderBy(b => b.ProposedPrice),

            (BidSortableEnum.ProposedPrice, SortDirectionEnum.Descending)
                => query.OrderByDescending(b => b.ProposedPrice),

            (BidSortableEnum.ProposedVisitDateTime, SortDirectionEnum.Ascending)
                => query.OrderBy(b => b.ProposedVisitDateTime),

            (BidSortableEnum.ProposedVisitDateTime, SortDirectionEnum.Descending)
                => query.OrderByDescending(b => b.ProposedVisitDateTime),

            (BidSortableEnum.CreatedAt, SortDirectionEnum.Ascending)
                => query.OrderBy(b => b.CreatedAt),

            _ => query.OrderByDescending(b => b.CreatedAt),
        };

        return query;
    }

    public async Task<int> GetCountAsync(CancellationToken ct)
    {
        return await _db.Bids.AsNoTracking()
            .CountAsync(ct);
    }

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

    public async Task<PagedResult<BidSummaryDto>> GetBidsSummaryListAsync(BidReqDto q, CancellationToken ct)
    {
        var query = ApplyFilters(q);
        var total = await query.CountAsync(ct);
        var skip = (q.Page - 1) * q.PageSize;
        var items = await query
            .Skip(skip)
            .Take(q.PageSize)
            .Select(b => new BidSummaryDto()
            {
                Id = b.Id,
                ServiceTitle = b.Request.Work.Title,
                ProposedPrice = b.ProposedPrice,
                ExpertPhoneNumber = b.Expert.PhoneNumber,
                ExpertFirstName = b.Expert.FirstName,
                ExpertLastName = b.Expert.LastName,
                ProposedVisitDate = b.ProposedVisitDateTime,
                Status = b.Status
            }).ToListAsync(ct);

        return new PagedResult<BidSummaryDto>
        {
            Items = items,
            Page = q.Page,
            PageSize = q.PageSize,
            TotalCount = total
        };
    }
}
