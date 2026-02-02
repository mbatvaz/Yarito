using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Domain.Core.Enums.Users;
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
            query = query.Where(b => b.RequestId == q.RequestId);

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
                ExpertId = b.ExpertId,
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

    public async Task<PagedResult<BidFullDto>> GetBidsFullListAsync(BidReqDto q, CancellationToken ct)
    {
        var query = ApplyFilters(q);
        var total = await query.CountAsync(ct);
        var skip = (q.Page - 1) * q.PageSize;
        var items = await query
            .Skip(skip)
            .Take(q.PageSize)
            .Select(b => new BidFullDto()
            {
                Id = b.Id,
                ProposedPrice = b.ProposedPrice,
                ExpertId = b.ExpertId,
                Status = b.Status,
                Description = b.Description,
                CreatedAt = b.ProposedVisitDateTime,
                ProposedVisitDateTime = b.ProposedVisitDateTime,
                RequestId = b.RequestId

            }).ToListAsync(ct);

        return new PagedResult<BidFullDto>
        {
            Items = items,
            Page = q.Page,
            PageSize = q.PageSize,
            TotalCount = total
        };
    }

    public async Task<BidFullDto?> GetBidFullByIdAsync(int bidId, CancellationToken ct)
    {
        return await _db.Bids.AsNoTracking()
            .Where(b => b.Id == bidId)
            .Select(b => new BidFullDto()
            {
                Id = b.Id,
                ProposedVisitDateTime = b.ProposedVisitDateTime,
                CreatedAt = b.CreatedAt,
                ProposedPrice = b.ProposedPrice,
                Description = b.Description,
                Status = b.Status,
                ExpertId = b.ExpertId,
                RequestId = b.RequestId
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<BidDetailsDto?> GetBidDetailsAsync(int bidId, CancellationToken ct)
    {
        return await _db.Bids.AsNoTracking()
            .Where(b => b.Id == bidId)
            .Select(b => new BidDetailsDto()
            {
                Bid = new BidFullDto()
                {
                    Id = b.Id,
                    ProposedVisitDateTime = b.ProposedVisitDateTime,
                    CreatedAt = b.CreatedAt,
                    ProposedPrice = b.ProposedPrice,
                    Description = b.Description,
                    Status = b.Status,
                    ExpertId = b.ExpertId,
                    RequestId = b.RequestId
                },
                Expert = new AppUserFullDto()
                {
                    Id = b.Expert.Id,
                    FirstName = b.Expert.FirstName,
                    LastName = b.Expert.LastName,
                    PhoneNumber = b.Expert.PhoneNumber,
                    UserType = UserTypeEnum.Expert,
                    CreatedAt = b.Expert.CreatedAt,
                    ProfileImgPath = b.Expert.ProfileImgPath ?? "/Images/Profile/default.png",
                    WalletBalance = b.Expert.WalletBalance,
                    CityName = b.Expert.City != null ? b.Expert.City.Name : null,
                    Email = b.Expert.Email
                },
                Customer = new AppUserFullDto()
                {
                    Id = b.Request.Customer.Id,
                    FirstName = b.Request.Customer.FirstName,
                    LastName = b.Request.Customer.LastName,
                    PhoneNumber = b.Request.Customer.PhoneNumber,
                    UserType = UserTypeEnum.Customer,
                    CreatedAt = b.Request.Customer.CreatedAt,
                    ProfileImgPath = b.Request.Customer.ProfileImgPath ?? "/Images/Profile/default.png",
                    WalletBalance = b.Request.Customer.WalletBalance,
                    CityName = b.Request.Customer.City != null ? b.Request.Customer.City.Name : null,
                    Email = b.Request.Customer.Email,
                    Address = b.Request.Customer.Address
                },
                Request = new RequestFullDto()
                {
                    Id = b.Request.Id,
                    Title = b.Request.Title,
                    Description = b.Request.Description,
                    WorkTitle = b.Request.Work.Title,
                    ProposedPrice = b.Request.ProposedPrice ?? 0,
                    Address = b.Request.Address,
                    PreferredVisitDateTime = b.Request.PreferredVisitDateTime,
                    CreatedAt = b.Request.CreatedAt,
                    Status = b.Request.Status,
                    CustomerId = b.Request.CustomerId,
                }
            })
            .FirstOrDefaultAsync(ct);
    }
}
