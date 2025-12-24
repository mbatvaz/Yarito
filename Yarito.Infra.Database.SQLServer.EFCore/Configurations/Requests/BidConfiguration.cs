using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Requests;

public class BidConfiguration : IEntityTypeConfiguration<Bid>
{
    public void Configure(EntityTypeBuilder<Bid> builder)
    {
        builder.HasKey(b => b.Id);
        
        builder.HasIndex(b => new { b.RequestId, b.ExpertId })
        .IsUnique();
        
        builder.HasIndex(b => b.ExpertId);
        
        builder.HasIndex(b => b.Status);

        builder.Property(b => b.Description)
            .HasMaxLength(1000)
            .IsUnicode();

        builder.Property(b => b.ProposedPrice)
            .IsRequired()
            .HasColumnType("decimal(18,0)");

        builder.Property(b => b.Status)
            .IsRequired()
            .HasDefaultValue(BidStatusEnum.Pending);

        builder.Property(b => b.ProposedVisitDateTime)
            .IsRequired(false);

        builder.HasOne(b => b.Request)
            .WithMany(r => r.Bids)
            .HasForeignKey(b => b.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Expert)
            .WithMany(e => e.Bids)
            .HasForeignKey(b => b.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
