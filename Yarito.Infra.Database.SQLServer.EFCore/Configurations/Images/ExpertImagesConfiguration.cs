using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Images;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Images;

public class ExpertImagesConfiguration : IEntityTypeConfiguration<ExpertImage>
{
    public void Configure(EntityTypeBuilder<ExpertImage> builder)
    {
        builder.HasKey(ei => ei.Id);

        builder.Property(ei => ei.Url)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(ei => ei.Expert)
            .WithMany(e => e.ExpertImages)
            .HasForeignKey(ei => ei.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
