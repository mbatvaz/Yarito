using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Requests;

public class ReviewRepo(AppDbContext _db) : IReviewRepo
{
    private IQueryable<Review> ApplyFilters(ReviewReqDto q)
    {
        var query = _db.Reviews.AsNoTracking().AsQueryable();

        // Status filter
        query = q.ApprovalStatus switch
        {
            ReviewStatusEnum.Pending => query.Where(r
                => r.ReviewStatus == ReviewStatusEnum.Pending),
            ReviewStatusEnum.Approved => query.Where(r
                => r.ReviewStatus == ReviewStatusEnum.Approved),
            ReviewStatusEnum.Rejected => query.Where(r
                => r.ReviewStatus == ReviewStatusEnum.Rejected),
            _ => query
        };

        // Rating filters
        if (q.Rating is not null)
            query = query.Where(r => r.Rating == q.Rating);
        else
        {
            if (q.MinRating is not null)
                query = query.Where(r => r.Rating >= q.MinRating.Value);

            if (q.MaxRating is not null)
                query = query.Where(r => r.Rating <= q.MaxRating.Value);
        }

        // Extra filters
        if (q.ExpertId is not null)
            query = query.Where(r => r.ExpertId == q.ExpertId);

        if (q.CustomerId is not null)
            query = query.Where(r => r.CustomerId == q.CustomerId);

        if (q.RequestId is not null)
            query = query.Where(r => r.RequestId == q.RequestId);

        if (q.From is not null)
            query = query.Where(r => r.CreatedAt >= q.From.Value);

        if (q.To is not null)
            query = query.Where(r => r.CreatedAt <= q.To.Value);

        if (q.TextSearch is not null)
        {
            query = query.Where(r => r.Comment.Contains(q.TextSearch)
                                     || r.Customer.FirstName.Contains(q.TextSearch)
                                     || r.Customer.LastName.Contains(q.TextSearch));
        }

        // Sorting
        var sortBy = q.Sort?.SortBy ?? ReviewSortableEnum.CreatedAt;
        var dir = q.Sort?.Direction ?? SortDirectionEnum.Descending;

        query = (sortBy, dir) switch
        {
            (ReviewSortableEnum.Rating, SortDirectionEnum.Ascending)
                => query.OrderBy(r => r.Rating),

            (ReviewSortableEnum.Rating, SortDirectionEnum.Descending)
                => query.OrderByDescending(r => r.Rating),

            (ReviewSortableEnum.CreatedAt, SortDirectionEnum.Ascending)
                => query.OrderBy(r => r.CreatedAt),

            _ => query.OrderByDescending(r => r.CreatedAt),
        };

        return query;
    }

    public async Task<bool> AddAsync(AddNewReviewDto newReview, CancellationToken ct)
    {
        var review = new Review
        {
            RequestId = newReview.RequestId,
            CustomerId = newReview.CustomerId,
            ExpertId = newReview.ExpertId,
            Rating = newReview.Rating,
            Comment = newReview.Comment,
            ReviewStatus = newReview.ReviewStatus
        };
        _db.Reviews.Add(review);
        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> UpdateAsync(Review newReview, CancellationToken ct)
    {
        var dbReview = await _db.Reviews.FindAsync([newReview.Id], ct);
        if (dbReview is null) return false;

        dbReview.Comment = newReview.Comment;
        dbReview.Rating = newReview.Rating;
        dbReview.ReviewStatus = ReviewStatusEnum.Pending;

        return await _db.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> DeleteAsync(int reviewId, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.Reviews
            .Where(r => r.Id == reviewId && r.IsDeleted == false)
            .ExecuteUpdateAsync(r => r
                .SetProperty(reviews => reviews.IsDeleted, true), ct) > 0;
    }

    public async Task<bool> ChangeStatusAsync(int reviewId, ReviewStatusEnum newStatus, CancellationToken ct, bool save)
    {
        var review = await _db.Reviews.FindAsync([reviewId], ct);
        if (review is null || review.ReviewStatus == newStatus) return false;

        review.ReviewStatus = newStatus;

        if (save)
            return await _db.SaveChangesAsync(ct) > 0;

        return true;
    }

    public async Task<IReadOnlyList<ReviewSummaryDto>> GetReviewsForHomePageAsync(ReviewReqDto q, CancellationToken ct)
    {
        return await ApplyFilters(q)
            .Take(q.PageSize)
            .Select(r => new ReviewSummaryDto()
            {
                Id = r.Id,
                FirstName = r.Customer.FirstName ?? "ناشناس",
                Rating = r.Rating,
                Comment = r.Comment ?? "عالی",
                Status = r.ReviewStatus
            }).ToListAsync(ct);
    }

    public async Task<PagedResult<ReviewFullDto>> GetReviewsListAsync(ReviewReqDto q, CancellationToken ct)
    {
        var query = ApplyFilters(q);
        var total = await query.CountAsync(ct);
        var skip = (q.Page - 1) * q.PageSize;
        var items = await query
            .Skip(skip)
            .Take(q.PageSize)
            .Select(r => new ReviewFullDto()
            {
                ReviewId = r.Id,
                BidId = r.Request.AcceptedBidId ?? 0,
                FirstName = r.Customer.FirstName,
                LastName = r.Customer.LastName,
                Rating = r.Rating,
                ReviewStatus = r.ReviewStatus,
                Comment = r.Comment,
                CreateAt = r.CreatedAt,
                RequestId = r.Request.Id
            }).ToListAsync(ct);

        return new PagedResult<ReviewFullDto>
        {
            Items = items,
            Page = q.Page,
            PageSize = q.PageSize,
            TotalCount = total
        };
    }

    public async Task<ReviewSummaryDto?> GetReviewsForRequestByIdAsync(int requestId, CancellationToken ct)
    {
        return await _db.Reviews
            .AsNoTracking()
            .Where(r => r.RequestId == requestId)
            .Select(r => new ReviewSummaryDto()
            {
                Id = r.Id,
                FirstName = r.Customer.FirstName,
                Comment = r.Comment,
                Rating = r.Rating,
                Status = r.ReviewStatus
            }).FirstOrDefaultAsync(ct);

    }

    public async Task<bool> HasReviewForRequestAsync(int requestId, CancellationToken ct)
    {
        return await _db.Reviews.AnyAsync(r => r.RequestId == requestId, ct);
    }
}