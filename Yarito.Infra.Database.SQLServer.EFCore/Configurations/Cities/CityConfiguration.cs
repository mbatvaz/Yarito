using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Cities;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Cities;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Name)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode();

        builder.HasOne(ci => ci.Parent)
            .WithMany(ci => ci.Children)
            .HasForeignKey(ci => ci.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
