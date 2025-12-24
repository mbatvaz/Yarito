using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Categories;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(c => c.Description)
            .HasMaxLength(1000)
            .IsUnicode();

        builder.Property(c => c.BasePrice)
            .IsRequired()
            .HasColumnType("decimal(18,0)");

        builder.HasOne(c => c.Parent)
            .WithMany(c => c.Children)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
