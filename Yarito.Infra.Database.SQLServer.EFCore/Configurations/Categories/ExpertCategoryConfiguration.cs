using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Categories;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Categories;

public class ExpertCategoryConfiguration : IEntityTypeConfiguration<ExpertCategory>
{
    public void Configure(EntityTypeBuilder<ExpertCategory> builder)
    {
        builder.HasKey(ec => ec.Id);

        builder.HasOne(ec => ec.Expert)
            .WithMany(e => e.ExpertCategories)
            .HasForeignKey(ec => ec.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ec => ec.Category)
            .WithMany(c => c.ExpertCategories)
            .HasForeignKey(ec => ec.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(ec => new { ec.ExpertId, ec.CategoryId })
            .IsUnique();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
