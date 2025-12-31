using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Entities.Users;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Requests;

public class RequestRepo(AppDbContext _db) : IRequestRepo
{

    private IQueryable<Request> ApplyingFiltersToQueries(RequestReqDto q)
    {
        var query = _db.Requests.AsNoTracking().AsQueryable();

        // Status filter
        if (q.Status is not null)
            query = query.Where(r => r.Status == q.Status);

        // Price filters
        if (q.MinProposedPrice is not null)
            query = query.Where(r => r.ProposedPrice != null && r.ProposedPrice >= q.MinProposedPrice.Value);

        if (q.MaxProposedPrice is not null)
            query = query.Where(r => r.ProposedPrice != null && r.ProposedPrice <= q.MaxProposedPrice.Value);

        // Extra filters
        if (q.CustomerId is not null)
            query = query.Where(r => r.CustomerId == q.CustomerId.Value);

        if (q.WorkId is not null)
            query = query.Where(r => r.WorkId == q.WorkId.Value);

        if (q.ExpertId is not null)
            query = query.Where(r => r.AcceptedBid != null && r.AcceptedBid.ExpertId == q.ExpertId.Value);

        if (!string.IsNullOrWhiteSpace(q.City))
            query = query.Where(r => r.Customer.City != null && r.Customer.City.Name.Contains(q.City));

        // Date filters
        if (q.From is not null)
            query = query.Where(r => r.CreatedAt >= q.From.Value);

        if (q.To is not null)
            query = query.Where(r => r.CreatedAt <= q.To.Value);

        if (q.PreferredFrom is not null)
            query = query.Where(r => r.PreferredVisitDateTime != null && r.PreferredVisitDateTime >= q.PreferredFrom.Value);

        if (q.PreferredTo is not null)
            query = query.Where(r => r.PreferredVisitDateTime != null && r.PreferredVisitDateTime <= q.PreferredTo.Value);


        // Text search
        if (!string.IsNullOrWhiteSpace(q.TextSearch))
        {
            var ts = q.TextSearch.Trim();

            query = query.Where(r =>
                r.Title.Contains(ts) 
                || (r.Description != null && r.Description.Contains(ts)) 
                ||  r.Address.Contains(ts) 
                || (r.Customer.FirstName != null && r.Customer.FirstName.Contains(ts)) 
                || (r.Customer.LastName != null && r.Customer.LastName.Contains(ts)) 
                ||  r.Work.Title.Contains(ts)
            );
        }

        // Sorting
        var sortBy = q.Sort?.SortBy ?? RequestSortableEnum.CreatedAt;
        var dir = q.Sort?.Direction ?? SortDirectionEnum.Descending;

        query = (sortBy, dir) switch
        {
            (RequestSortableEnum.ProposedPrice, SortDirectionEnum.Ascending)
                => query.OrderBy(r => r.ProposedPrice),

            (RequestSortableEnum.ProposedPrice, SortDirectionEnum.Descending)
                => query.OrderByDescending(r => r.ProposedPrice),

            (RequestSortableEnum.PreferredVisitDateTime, SortDirectionEnum.Ascending)
                => query.OrderBy(r => r.PreferredVisitDateTime),

            (RequestSortableEnum.PreferredVisitDateTime, SortDirectionEnum.Descending)
                => query.OrderByDescending(r => r.PreferredVisitDateTime),

            (RequestSortableEnum.CreatedAt, SortDirectionEnum.Ascending)
                => query.OrderBy(r => r.CreatedAt),

            _ => query.OrderByDescending(r => r.CreatedAt),
        };

        return query;
    }
    public async Task<int> GetCountAsync(CancellationToken ct)
    {
        return await _db.Requests.AsNoTracking()
            .CountAsync(ct);
    }
    
    public async Task<bool> AddAsync(Request newRequest, CancellationToken ct)
    {
        _db.Requests.Add(newRequest);
        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> UpdateAsync(Request newRequest, CancellationToken ct)
    {
        var dbRequest = await _db.Requests.FindAsync([newRequest.Id], ct);
        if(dbRequest is null) return false;

        dbRequest.PreferredVisitDateTime = newRequest.PreferredVisitDateTime;
        dbRequest.ProposedPrice = newRequest.ProposedPrice;
        dbRequest.Description = newRequest.Description;
        dbRequest.Address = newRequest.Address;
        dbRequest.Title = newRequest.Title;
        dbRequest.RequestImages = newRequest.RequestImages;

        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> AcceptBidAsync(int requestId, int bidId, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.Requests
            .Where(r => r.Id == requestId && r.AcceptedBidId == null)
            .ExecuteUpdateAsync(r => r
                    .SetProperty(request => request.AcceptedBidId, bidId), ct) > 0;
    }

    public async Task<bool> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.Requests
            .Where(r => r.Id == requestId && r.Status != newStatus)
            .ExecuteUpdateAsync(r => r
                    .SetProperty(request => request.Status, newStatus), ct) > 0;
    }

    public async Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q, CancellationToken ct)
    {
        var query = ApplyingFiltersToQueries(q);
        var total = await query.CountAsync(ct);
        var skip = (q.Page - 1) * q.PageSize;
        var items = await query
            .Skip(skip)
            .Take(q.PageSize)
            .Select(r => new RequestsSummaryDto()
            {
                Id = r.Id,
                Status = r.Status,
                CityName = r.Customer.City!.Name,
                ServicesTitle = r.Work.Title,
                Title = r.Title,
                CreateAt = r.CreatedAt
            }).ToListAsync(ct);

        return new PagedResult<RequestsSummaryDto>
        {
            Items = items,
            Page = q.Page,
            PageSize = q.PageSize,
            TotalCount = total
        };
    }
}
