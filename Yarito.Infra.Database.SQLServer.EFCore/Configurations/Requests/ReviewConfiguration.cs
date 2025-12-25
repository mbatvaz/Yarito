using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Requests;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        // Primary Key
        builder.HasKey(r => r.Id);

        // Indexes
        builder.HasIndex(r => r.RequestId)
            .IsUnique();
        
        builder.HasIndex(r => r.ExpertId);


        // Property Configurations
        builder.Property(r => r.Rating)
            .IsRequired();

        builder.Property(r => r.Comment)
            .HasMaxLength(1000)
            .IsUnicode();

        builder.Property(r => r.ReviewStatus)
            .HasDefaultValue(ReviewStatusEnum.Pending);

        // Relationships
        builder.HasOne(r => r.Request)
            .WithOne(req => req.Review)
            .HasForeignKey<Review>(r => r.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Expert)
            .WithMany(e => e.Reviews)
            .HasForeignKey(r => r.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        // Query Filter
        builder.HasQueryFilter(x => !x.IsDeleted);

        // Seed Data
        builder.HasData(
            // Review for Request 1 (سیم‌کشی آشپزخانه)
            new Review
            {
                Id = SeedDataIds.Review1Id,
                Rating = 5,
                Comment = "کار بسیار عالی و حرفه‌ای انجام شد. آقای برقکار بسیار دقیق و وقت‌شناس بودند. پیشنهاد می‌کنم.",
                ReviewStatus = ReviewStatusEnum.Approved,
                RequestId = SeedDataIds.Request1Id,
                CustomerId = SeedDataIds.Customer1Id,
                ExpertId = SeedDataIds.Expert1Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-18)
            },

            // Review for Request 2 (تعمیر شیر آب)
            new Review
            {
                Id = SeedDataIds.Review2Id,
                Rating = 4,
                Comment = "کار خوبی انجام شد. فقط کمی دیرتر از موعد مقرر آمدند.",
                ReviewStatus = ReviewStatusEnum.Approved,
                RequestId = SeedDataIds.Request2Id,
                CustomerId = SeedDataIds.Customer2Id,
                ExpertId = SeedDataIds.Expert2Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-13)
            },

            // Review for Request 3 (نظافت منزل)
            new Review
            {
                Id = SeedDataIds.Review3Id,
                Rating = 5,
                Comment = "نظافت فوق‌العاده دقیق و تمیز. خانم نظافتچی بسیار محترم و مودب بودند.",
                ReviewStatus = ReviewStatusEnum.Approved,
                RequestId = SeedDataIds.Request3Id,
                CustomerId = SeedDataIds.Customer3Id,
                ExpertId = SeedDataIds.Expert3Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-8)
            },

            // Review for Request 10 (تعمیر ماشین لباسشویی)
            new Review
            {
                Id = SeedDataIds.Review4Id,
                Rating = 5,
                Comment = "خانم تعمیرکار بسیار متخصص و حرفه‌ای بودند. مشکل به سرعت حل شد.",
                ReviewStatus = ReviewStatusEnum.Approved,
                RequestId = SeedDataIds.Request10Id,
                CustomerId = SeedDataIds.Customer5Id,
                ExpertId = SeedDataIds.Expert5Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-6)
            },

            // Review with Pending status
            new Review
            {
                Id = SeedDataIds.Review5Id,
                Rating = 3,
                Comment = "کار خوب بود ولی قیمت کمی بالا بود نسبت به بازار.",
                ReviewStatus = ReviewStatusEnum.Pending,
                RequestId = SeedDataIds.Request4Id,
                CustomerId = SeedDataIds.Customer4Id,
                ExpertId = SeedDataIds.Expert4Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-1)
            },

            // Review with Rejected status
            new Review
            {
                Id = SeedDataIds.Review6Id,
                Rating = 2,
                Comment = "متاسفانه کار طبق توافق انجام نشد. از کیفیت راضی نیستم.",
                ReviewStatus = ReviewStatusEnum.Rejected,
                RequestId = SeedDataIds.Request5Id,
                CustomerId = SeedDataIds.Customer5Id,
                ExpertId = SeedDataIds.Expert5Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddHours(-10)
            },

            // Another Approved review
            new Review
            {
                Id = SeedDataIds.Review7Id,
                Rating = 4,
                Comment = "به طور کلی راضی هستم. کار خوبی انجام شد و قیمت مناسب بود.",
                ReviewStatus = ReviewStatusEnum.Approved,
                RequestId = SeedDataIds.Request6Id,
                CustomerId = SeedDataIds.Customer1Id,
                ExpertId = SeedDataIds.Expert1Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddHours(-5)
            }
        );
    }
}
