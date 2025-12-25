using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Works;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Works;

public class WorkConfiguration : IEntityTypeConfiguration<Work>
{
    public void Configure(EntityTypeBuilder<Work> builder)
    {
        // Primary Key
        builder.HasKey(w => w.Id);

        // Indexes
        builder.HasIndex(w => w.CategoryId);

        // Property Configurations
        builder.Property(w => w.Title)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(w => w.BasePrice)
            .IsRequired()
            .HasColumnType("decimal(18,0)");

        // Relationships
        builder.HasOne(w => w.Category)
            .WithMany(c => c.Works)
            .HasForeignKey(w => w.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(w => w.Requests)
            .WithOne(r => r.Work)
            .HasForeignKey(r => r.WorkId)
            .OnDelete(DeleteBehavior.Restrict);

        // Query Filter
        builder.HasQueryFilter(x => !x.IsDeleted);

        // Seed Data
        builder.HasData(
            // خدمات برق
            new Work
            {
                Id = SeedDataIds.HomeWiringWorkId,
                Title = "سیم‌کشی ساختمان",
                BasePrice = 2000000,
                CategoryId = SeedDataIds.ElectricCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Work
            {
                Id = SeedDataIds.LightInstallationWorkId,
                Title = "نصب لوستر و چراغ",
                BasePrice = 500000,
                CategoryId = SeedDataIds.ElectricCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Work
            {
                Id = SeedDataIds.SwitchRepairWorkId,
                Title = "تعمیر کلید و پریز برق",
                BasePrice = 300000,
                CategoryId = SeedDataIds.ElectricCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },

            // خدمات لوله‌کشی
            new Work
            {
                Id = SeedDataIds.PipeRepairWorkId,
                Title = "تعمیر و رفع نشتی لوله",
                BasePrice = 800000,
                CategoryId = SeedDataIds.PlumbingCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Work
            {
                Id = SeedDataIds.FaucetInstallationWorkId,
                Title = "نصب شیر آلات",
                BasePrice = 400000,
                CategoryId = SeedDataIds.PlumbingCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Work
            {
                Id = SeedDataIds.ToiletRepairWorkId,
                Title = "تعمیر توالت فرنگی",
                BasePrice = 600000,
                CategoryId = SeedDataIds.PlumbingCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },

            // خدمات نظافت
            new Work
            {
                Id = SeedDataIds.HomeCleaningWorkId,
                Title = "نظافت منزل",
                BasePrice = 1500000,
                CategoryId = SeedDataIds.CleaningCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Work
            {
                Id = SeedDataIds.WindowCleaningWorkId,
                Title = "شستشوی پنجره",
                BasePrice = 700000,
                CategoryId = SeedDataIds.CleaningCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Work
            {
                Id = SeedDataIds.CarpetCleaningWorkId,
                Title = "قالیشویی",
                BasePrice = 2500000,
                CategoryId = SeedDataIds.CleaningCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },

            // خدمات نقاشی
            new Work
            {
                Id = SeedDataIds.InteriorPaintingWorkId,
                Title = "نقاشی داخلی ساختمان",
                BasePrice = 3000000,
                CategoryId = SeedDataIds.PaintingCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Work
            {
                Id = SeedDataIds.ExteriorPaintingWorkId,
                Title = "نقاشی نمای ساختمان",
                BasePrice = 5000000,
                CategoryId = SeedDataIds.PaintingCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Work
            {
                Id = SeedDataIds.WallPaperingWorkId,
                Title = "کاغذ دیواری",
                BasePrice = 2000000,
                CategoryId = SeedDataIds.PaintingCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },

            // تعمیر لوازم خانگی
            new Work
            {
                Id = SeedDataIds.WashingMachineRepairWorkId,
                Title = "تعمیر ماشین لباسشویی",
                BasePrice = 1000000,
                CategoryId = SeedDataIds.ApplianceRepairCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Work
            {
                Id = SeedDataIds.RefrigeratorRepairWorkId,
                Title = "تعمیر یخچال",
                BasePrice = 1200000,
                CategoryId = SeedDataIds.ApplianceRepairCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Work
            {
                Id = SeedDataIds.ACRepairWorkId,
                Title = "تعمیر کولر گازی",
                BasePrice = 1500000,
                CategoryId = SeedDataIds.ApplianceRepairCategoryId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            }
        );
    }
}
