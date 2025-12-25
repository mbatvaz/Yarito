using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Images;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Images;

public class ExpertImagesConfiguration : IEntityTypeConfiguration<ExpertPortfolioImage>
{
    public void Configure(EntityTypeBuilder<ExpertPortfolioImage> builder)
    {
        // Primary Key
        builder.HasKey(ei => ei.Id);

        // Indexes
        builder.HasIndex(ei => ei.ExpertId);

        // Property Configurations
        builder.Property(ei => ei.ImgPath)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ei => ei.Title)
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(ei => ei.ExpertId)
            .IsRequired();

        // Relationships
        builder.HasOne(ei => ei.Expert)
            .WithMany(e => e.ExpertImages)
            .HasForeignKey(ei => ei.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        // Query Filter
        builder.HasQueryFilter(x => !x.IsDeleted);

        // Seed Data
        builder.HasData(
            // Portfolio images for Expert 1 (رضا برقکار - متخصص برق)
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage1Id,
                ImgPath = "/images/portfolio/expert1_p1_a3f8d2e7.jpg",
                Title = "سیم‌کشی ساختمان مسکونی",
                ExpertId = SeedDataIds.Expert1Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-60)
            },
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage2Id,
                ImgPath = "/images/portfolio/expert1_p2_b9c5e4f1.jpg",
                Title = "نصب تابلو برق صنعتی",
                ExpertId = SeedDataIds.Expert1Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-45)
            },
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage3Id,
                ImgPath = "/images/portfolio/expert1_p3_c6d2a7e9.jpg",
                Title = "نصب لوستر کریستالی",
                ExpertId = SeedDataIds.Expert1Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-30)
            },

            // Portfolio images for Expert 2 (مهدی لوله‌کش)
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage4Id,
                ImgPath = "/images/portfolio/expert2_p1_d8f3c5b2.jpg",
                Title = "لوله‌کشی آشپزخانه مدرن",
                ExpertId = SeedDataIds.Expert2Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-55)
            },
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage5Id,
                ImgPath = "/images/portfolio/expert2_p2_e4a9d6f8.jpg",
                Title = "نصب شیرآلات لوکس",
                ExpertId = SeedDataIds.Expert2Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-40)
            },
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage6Id,
                ImgPath = "/images/portfolio/expert2_p3_f7c2e8d5.jpg",
                Title = "تعمیر سیستم فاضلاب",
                ExpertId = SeedDataIds.Expert2Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-25)
            },

            // Portfolio images for Expert 3 (سارا نظافتچی)
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage7Id,
                ImgPath = "/images/portfolio/expert3_p1_g2d5a8f3.jpg",
                Title = "نظافت ویلای لوکس",
                ExpertId = SeedDataIds.Expert3Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-50)
            },
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage8Id,
                ImgPath = "/images/portfolio/expert3_p2_h9e6b4c7.jpg",
                Title = "شستشوی نما و پنجره‌های برج",
                ExpertId = SeedDataIds.Expert3Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-35)
            },
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage9Id,
                ImgPath = "/images/portfolio/expert3_p3_i5f8c2d9.jpg",
                Title = "قالیشویی فرش دستباف",
                ExpertId = SeedDataIds.Expert3Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-20)
            },

            // Portfolio images for Expert 4 (احمد نقاش)
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage10Id,
                ImgPath = "/images/portfolio/expert4_p1_j8c3d7e4.jpg",
                Title = "نقاشی آپارتمان 150 متری",
                ExpertId = SeedDataIds.Expert4Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-65)
            },
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage11Id,
                ImgPath = "/images/portfolio/expert4_p2_k4e9f5a2.jpg",
                Title = "رنگ‌آمیزی نمای ساختمان",
                ExpertId = SeedDataIds.Expert4Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-48)
            },
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage12Id,
                ImgPath = "/images/portfolio/expert4_p3_l7a2c6d8.jpg",
                Title = "کاغذ دیواری اتاق کودک",
                ExpertId = SeedDataIds.Expert4Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-32)
            },

            // Portfolio images for Expert 5 (نرگس تعمیرکار)
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage13Id,
                ImgPath = "/images/portfolio/expert5_p1_m3d8e5f9.jpg",
                Title = "تعمیر یخچال فریزر دوقلو",
                ExpertId = SeedDataIds.Expert5Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-52)
            },
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage14Id,
                ImgPath = "/images/portfolio/expert5_p2_n6f4a7c2.jpg",
                Title = "سرویس کولر گازی اسپلیت",
                ExpertId = SeedDataIds.Expert5Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-38)
            },
            new ExpertPortfolioImage
            {
                Id = SeedDataIds.ExpertPortfolioImage15Id,
                ImgPath = "/images/portfolio/expert5_p3_o9c5d8e6.jpg",
                Title = "تعمیر ماشین لباسشویی اتوماتیک",
                ExpertId = SeedDataIds.Expert5Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-22)
            }
        );
    }
}
