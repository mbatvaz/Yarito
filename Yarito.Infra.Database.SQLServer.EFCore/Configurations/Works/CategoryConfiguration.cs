using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Works;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Works;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Primary Key
        builder.HasKey(c => c.Id);

        // Property Configurations
        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(c => c.Description)
            .HasMaxLength(1000)
            .IsUnicode();

        // Query Filter
        builder.HasQueryFilter(x => !x.IsDeleted);

        // Seed Data
        builder.HasData(
            new Category
            {
                Id = SeedDataIds.ElectricCategoryId,
                Title = "خدمات برق",
                Description = "نصب، تعمیر و نگهداری تاسیسات برقی ساختمان",
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Category
            {
                Id = SeedDataIds.PlumbingCategoryId,
                Title = "خدمات لوله‌کشی",
                Description = "نصب، تعمیر و رفع نشتی لوله‌های آب و فاضلاب",
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Category
            {
                Id = SeedDataIds.CleaningCategoryId,
                Title = "خدمات نظافت",
                Description = "نظافت منزل، راه پله، پنجره و فرش",
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Category
            {
                Id = SeedDataIds.PaintingCategoryId,
                Title = "خدمات نقاشی",
                Description = "رنگ‌آمیزی داخلی و خارجی ساختمان",
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Category
            {
                Id = SeedDataIds.ApplianceRepairCategoryId,
                Title = "تعمیر لوازم خانگی",
                Description = "تعمیر و سرویس انواع لوازم خانگی",
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            }
        );
    }
}
