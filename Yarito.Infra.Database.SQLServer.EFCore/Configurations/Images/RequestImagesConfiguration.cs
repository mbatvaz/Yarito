using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Images;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Images;

public class RequestImagesConfiguration : IEntityTypeConfiguration<RequestImage>
{
    public void Configure(EntityTypeBuilder<RequestImage> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Url)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(i => i.Request)
            .WithMany(r => r.RequestImages)
            .HasForeignKey(i => i.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
