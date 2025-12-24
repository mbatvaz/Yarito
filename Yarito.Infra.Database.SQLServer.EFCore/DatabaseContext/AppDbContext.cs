using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Yarito.Domain.Core.Entities._Common;
using Yarito.Domain.Core.Entities.Categories;
using Yarito.Domain.Core.Entities.Cities;
using Yarito.Domain.Core.Entities.Images;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Entities.Users;


namespace Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = DateTime.UtcNow;
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = DateTime.UtcNow;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ExpertCategory> ExpertCategories { get; set; }
    public DbSet<RequestImage> RequestImages { get; set; }
    public DbSet<ExpertImage> ExpertImages { get; set; }
    public DbSet<BidImage> BidImages { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Review> Reviews { get; set; }
}