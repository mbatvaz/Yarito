using Microsoft.EntityFrameworkCore;
using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

namespace Yarito.Infra.DataAccess.EFCore.Requests;

public class RequestRepo(AppDbContext _db) : IRequestRepo
{
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
}
