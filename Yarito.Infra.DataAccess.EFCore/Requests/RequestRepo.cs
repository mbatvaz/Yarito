using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.DTOs.Users;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Domain.Core.Enums.Users;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;
using Request = Yarito.Domain.Core.Entities.Requests.Request;

namespace Yarito.Infra.DataAccess.EFCore.Requests;

public class RequestRepo(AppDbContext _db) : IRequestRepo
{

    private IQueryable<Request> ApplyFilters(RequestReqDto q)
    {
        var query = _db.Requests.AsNoTracking().AsQueryable();

        // Status filter
        if (q.FirstStatus is not null && q.SecondStatus is not null)
            query = query.Where(r => r.Status == q.FirstStatus || r.Status == q.SecondStatus);
        else if (q.FirstStatus is not null)
            query = query.Where(r => r.Status == q.FirstStatus);
        else if (q.SecondStatus is not null)
            query = query.Where(r => r.Status == q.SecondStatus);

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

        if (q.CityId is not null)
            query = query.Where(r => r.Customer.CityId == q.CityId.Value);

        // Date filters
        if (q.From is not null)
            query = query.Where(r => r.CreatedAt >= q.From.Value);

        if (q.To is not null)
            query = query.Where(r => r.CreatedAt <= q.To.Value);

        if (q.PreferredFrom is not null)
            query = query.Where(r => r.PreferredVisitDateTime != null && r.PreferredVisitDateTime >= q.PreferredFrom.Value);

        if (q.PreferredTo is not null)
            query = query.Where(r => r.PreferredVisitDateTime != null && r.PreferredVisitDateTime < q.PreferredTo.Value);


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
    
    public async Task<int> AddAsync(RequestNewDto dto, CancellationToken ct)
    {
        var request = new Request()
        {
            Title = dto.Title,
            Address = dto.Address!,
            Status = RequestStatusEnum.Pending,
            CustomerId = dto.UserId,
            Description = dto.Description,
            PreferredVisitDateTime = dto.PreferredVisitDateTime,
            ProposedPrice = dto.ProposedPrice,
            WorkId = dto.WorkId,
            
        };
        _db.Requests.Add(request);

        return await _db.SaveChangesAsync(ct) <= 0 
            ? 0 
            : request.Id;
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

    public async Task<bool> AcceptBidAsync(int requestId, int bidId, CancellationToken ct, bool save)
    {
        var request = await _db.Requests.FindAsync([requestId], ct);
        if (request is null || request.AcceptedBidId != null) return false;

        request.AcceptedBidId = bidId;

        if (save)
            return await _db.SaveChangesAsync(ct) > 0;

        return true;
    }

    public async Task<bool> ChangeStatusAsync(int requestId, RequestStatusEnum newStatus, CancellationToken ct, bool save)
    {
        var request = await _db.Requests.FirstOrDefaultAsync(r => r.Id == requestId, ct);

        if (request is null) return false;
        if (request.Status == newStatus) return false;

        request.Status = newStatus;

        if (save)
            return await _db.SaveChangesAsync(ct) > 0;

        return true;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await _db.SaveChangesAsync(ct) > 0;
    }



    public async Task<PagedResult<RequestsSummaryDto>> GetRequestsSummaryListAsync(RequestReqDto q, CancellationToken ct)
    {
        var query = ApplyFilters(q);
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

    public async Task<PagedResult<RequestCardDto>> GetRequestsCardListAsync(RequestReqDto q, CancellationToken ct)
    {
        var query = ApplyFilters(q);
        var total = await query.CountAsync(ct);
        var skip = (q.Page - 1) * q.PageSize;
        var items = await query
            .Skip(skip)
            .Take(q.PageSize)
            .Select(r => new RequestCardDto()
            {
                Id = r.Id,
                Title = r.Title,
                Status = r.Status,
                FirstName = r.Customer.FirstName,
                LastName = r.Customer.LastName,
                CityName = r.Customer.City!.Name ,
                CreatedAt = r.CreatedAt,
                CustomerProfileImagePath = r.Customer.ProfileImgPath,
                Description = r.Description,
                BidCount = r.Bids.Count(b =>b.Expert.IsDeleted==false)
            }).ToListAsync(ct);

        return new PagedResult<RequestCardDto>
        {
            Items = items,
            Page = q.Page,
            PageSize = q.PageSize,
            TotalCount = total
        };
    }

    public async Task<RequestSuccessDto?> GetRequestSuccessInfoByIdAsync(int requestId, CancellationToken ct)
    {
        return await _db.Requests.AsNoTracking()
            .Where(r => r.Id == requestId)
            .Select(r => new RequestSuccessDto()
            {
                Title = r.Title,
                Status = r.Status,
                Address = r.Address,
                Date = r.CreatedAt,
                WorkTitle = r.Work.Title
            }).FirstOrDefaultAsync(ct);
    }

    public async Task<RequestFullDto?> GetRequestFullByIdAsync(int requestId, CancellationToken ct)
    {
        return await _db.Requests.AsNoTracking()
            .Where(r => r.Id == requestId)
            .Select(r => new RequestFullDto()
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                ProposedPrice = r.ProposedPrice ?? 0,
                Address = r.Address,
                PreferredVisitDateTime = r.PreferredVisitDateTime,
                CreatedAt = r.CreatedAt,
                Status = r.Status,
                CustomerId = r.CustomerId,
                WorkTitle = r.Work.Title,
                AcceptedBidId = r.AcceptedBidId,
                RequestImagesPath = r.RequestImages.Select(i => i.ImgPath).ToList()
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<int> CountOfOpenRequestForCustomerIdAsync(int customerId, CancellationToken ct)
    {
        return await _db.Requests.CountAsync(r =>
                r.CustomerId == customerId &&
                (r.Status == RequestStatusEnum.InProgress ||
                 r.Status == RequestStatusEnum.Pending),
            ct);
    }

    public void ClearChangeTracker()
    {
        _db.ChangeTracker.Clear();
    }

    public async Task<PagedResult<ExpertDashboardVisitDto>> GetExpertVisitsAsync(RequestReqDto q, CancellationToken ct)
    {
        var query = ApplyFilters(q);
        var total = await query.CountAsync(ct);
        var skip = (q.Page - 1) * q.PageSize;
        var items = await query
            .Skip(skip)
            .Take(q.PageSize)
            .Select(r => new ExpertDashboardVisitDto
            {
                Id = r.Id,
                BidId = r.AcceptedBidId!.Value,
                Title = r.Title,
                FirstName = r.Customer.FirstName!,
                LastName = r.Customer.LastName!,
                CustomerPhoneNumber = r.Customer.PhoneNumber,
                VisitDateTime = r.AcceptedBid!.ProposedVisitDateTime
            }).ToListAsync(ct);

        return new PagedResult<ExpertDashboardVisitDto>
        {
            Items = items,
            Page = q.Page,
            PageSize = q.PageSize,
            TotalCount = total
        };
    }
}
