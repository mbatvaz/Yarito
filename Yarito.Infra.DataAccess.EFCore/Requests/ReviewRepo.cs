using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Requests;

public class ReviewRepo(AppDbContext _db) : IReviewRepo
{
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
}