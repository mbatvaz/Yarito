using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Images;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Images;
public class BidImageConfiguration : IEntityTypeConfiguration<BidImage>
{
    public void Configure(EntityTypeBuilder<BidImage> builder)
    {
        builder.HasKey(x => new { x.BidId, x.ExpertImageId });

        builder.HasOne(x => x.Bid)
            .WithMany(b => b.BidImage)
            .HasForeignKey(x => x.BidId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ExpertImage)
            .WithMany() 
            .HasForeignKey(x => x.ExpertImageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ExpertImageId);
    }
}
