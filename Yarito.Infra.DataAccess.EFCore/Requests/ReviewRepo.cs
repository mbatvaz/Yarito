using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.DTOs.Requests;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums._Common;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Requests;

public class ReviewRepo(AppDbContext _db) : IReviewRepo
{
    private IQueryable<Review> ApplyingFiltersToQueries(ReviewReqDto q)
    {
        var query = _db.Reviews.AsNoTracking();

        // Status filter
        query = q.ApprovalStatus switch
        {
            ReviewStatusEnum.All => query.Where(r
                => r.ReviewStatus == ReviewStatusEnum.All),
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
            query = query.Where(r => r.Comment.Contains(q.TextSearch));

        // Sorting
        if (q.Sort is not null)
        {
            query = (q.Sort.SortBy, q.Sort.Direction) switch
            {
                (ReviewSortableEnum.Rating, SortDirectionEnum.Ascending)
                    => query.OrderBy(r => r.Rating),

                (ReviewSortableEnum.Rating, SortDirectionEnum.Descending)
                    => query.OrderByDescending(r => r.Rating),

                (ReviewSortableEnum.CreatedAt, SortDirectionEnum.Ascending)
                    => query.OrderBy(r => r.CreatedAt),
                
                (ReviewSortableEnum.CreatedAt, SortDirectionEnum.Descending)
                    => query.OrderByDescending(r => r.CreatedAt),

                _ => query
            };
        }

        return query;
    }

    public async Task<bool> AddAsync(Review newReview, CancellationToken ct)
    {
        _db.Reviews.Add(newReview);
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

    public async Task<bool> ChangeStatusAsync(int reviewId, ReviewStatusEnum newStatus, CancellationToken ct)
    {
        _db.ChangeTracker.Clear();
        return await _db.Reviews
            .Where(r => r.Id == reviewId && r.ReviewStatus != newStatus)
            .ExecuteUpdateAsync(r => r
                .SetProperty(reviews => reviews.ReviewStatus, newStatus), ct) > 0;
    }

    public async Task<IReadOnlyList<HomePageReviewDto>> GetReviewsForHomePageAsync(ReviewReqDto q, CancellationToken ct)
    {
        return await ApplyingFiltersToQueries(q)
            .Take(q.PageSize)
            .Select(r => new HomePageReviewDto()
            {
                FirstName = r.Customer.FirstName ?? "ناشناس",
                Rating = r.Rating,
                Comment = r.Comment ?? "عالی",
            }).ToArrayAsync(ct);
    }
}