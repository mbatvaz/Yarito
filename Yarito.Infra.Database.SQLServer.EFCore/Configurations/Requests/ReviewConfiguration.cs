using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;
using static Yarito.Infra.Database.SQLServer.EFCore.Configurations.SeedDataGuids;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Requests;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasIndex(r => new {r.RequestId, r.BidId })
            .IsUnique();
        
        builder.HasIndex(r => r.ExpertId);

        builder.HasIndex(r => r.CustomerId);

        builder.Property(r => r.Rating)
            .IsRequired();

        builder.Property(r => r.Comment)
            .HasMaxLength(1000)
            .IsUnicode();

        builder.HasOne(r => r.Request)
            .WithOne(req => req.Review)
            .HasForeignKey<Review>(r => r.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Bid)
            .WithOne(b => b.Review)
            .HasForeignKey<Review>(r => r.BidId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Expert)
            .WithMany(e => e.Reviews)
            .HasForeignKey(r => r.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);

        var now = DateTime.UtcNow;

        // SeedData - نظرات مشتریان برای درخواست‌های تکمیل شده
        builder.HasData(
            // نظر برای درخواست 6 - رنگ‌آمیزی اتاق توسط محمد صادقی
            new Review 
            { 
                Id = Guid.Parse("90000000-0000-0000-0000-000000000001"), 
                RequestId = Request6Id, 
                BidId = Bid2Id, 
                CustomerId = Customer2Id, 
                ExpertId = Expert2Id, 
                Rating = 5, 
                Comment = "کار بسیار عالی و تمیز انجام شد. آقای صادقی بسیار دقیق و حرفه‌ای هستند. رنگ دیوار یکدست و بدون هیچ لکه‌ای شد. از دقت و وقت‌شناسی ایشان بسیار راضی هستم.",
                ReviewStatus = ReviewStatusEnum.Approved,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-19)
            },

            // نظر برای درخواست 7 - تعمیر یخچال توسط فاطمه موسوی
            new Review 
            { 
                Id = Guid.Parse("90000000-0000-0000-0000-000000000002"), 
                RequestId = Request7Id, 
                BidId = Bid6Id, 
                CustomerId = Customer3Id, 
                ExpertId = Expert3Id, 
                Rating = 4, 
                Comment = "خانم موسوی کار خوبی انجام دادند. یخچال به خوبی کار می‌کند اما کمی زمان بیشتری نسبت به وعده داده شده طول کشید. در کل راضی هستم.",
                ReviewStatus = ReviewStatusEnum.Approved,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-17)
            },

            // نظر برای درخواست 8 - آرایشگری توسط مریم یزدانی
            new Review 
            { 
                Id = Guid.Parse("90000000-0000-0000-0000-000000000003"), 
                RequestId = Request8Id, 
                BidId = Bid14Id, 
                CustomerId = Customer5Id, 
                ExpertId = Expert7Id, 
                Rating = 5, 
                Comment = "خانم یزدانی فوق‌العاده حرفه‌ای هستند. مدل موی من دقیقاً همان چیزی بود که می‌خواستم. رنگ مو هم عالی شد. خیلی مهربان و با ذوق هستند. قطعاً دوباره از خدمات ایشان استفاده می‌کنم.",
                ReviewStatus = ReviewStatusEnum.Approved,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-14)
            },

            // نظر برای درخواست 9 - تعویض لوله‌ها توسط سعید حیدری
            new Review 
            { 
                Id = Guid.Parse("90000000-0000-0000-0000-000000000004"), 
                RequestId = Request9Id, 
                BidId = Bid17Id, 
                CustomerId = Customer4Id, 
                ExpertId = Expert8Id, 
                Rating = 5, 
                Comment = "آقای حیدری واقعاً متخصص هستند. کار را با دقت و سرعت بالا انجام دادند. لوله‌های قدیمی رو تعویض کردند و همه چیز عالی کار می‌کنه. قیمت هم منصفانه بود. پیشنهاد می‌کنم.",
                ReviewStatus = ReviewStatusEnum.Approved,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-11)
            }
        );
    }
}
