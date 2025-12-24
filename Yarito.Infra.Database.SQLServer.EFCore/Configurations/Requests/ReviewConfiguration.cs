using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Requests;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Requests;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasIndex(r => new {r.RequestId, r.BidId })
            .IsUnique();
        
        builder.HasIndex(r => r.ExpertId);

        builder.HasIndex(r => r.CustomerId);

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.Property(r => r.Comment)
            .HasMaxLength(1000)
            .IsUnicode();

        builder.HasOne(r => r.Request)
            .WithOne(req => req.Review)
            .HasForeignKey<Review>(r => r.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Bid)
            .WithOne(b => b.Review)
            .HasForeignKey<Review>(r => r.BidId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Expert)
            .WithMany(e => e.Reviews)
            .HasForeignKey(r => r.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
