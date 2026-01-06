using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Yarito.Domain.Core.Entities.Images;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Images;

public class RequestImagesConfiguration : IEntityTypeConfiguration<RequestImage>
{
    public void Configure(EntityTypeBuilder<RequestImage> builder)
    {
        // Primary Key
        builder.HasKey(i => i.Id);

        // Indexes
        builder.HasIndex(i => i.RequestId);

        // Property Configurations
        builder.Property(i => i.ImgPath)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.RequestId)
            .IsRequired();

        // Relationships
        builder.HasOne(i => i.Request)
            .WithMany(r => r.RequestImages)
            .HasForeignKey(i => i.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        // Query Filter
        builder.HasQueryFilter(x => !x.IsDeleted);

        // Seed Data
        builder.HasData(
            // Images for Request 1 (سیم‌کشی آشپزخانه)
            new RequestImage
            {
                Id = SeedDataIds.RequestImage1Id,
                ImgPath = "/images/request/1.jpg",
                RequestId = SeedDataIds.Request1Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-25)
            },
            new RequestImage
            {
                Id = SeedDataIds.RequestImage2Id,
                ImgPath = "/images/request/2.jpg",
                RequestId = SeedDataIds.Request1Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-25)
            },

            // Images for Request 2 (تعمیر شیر آب)
            new RequestImage
            {
                Id = SeedDataIds.RequestImage3Id,
                ImgPath = "/images/request/1.jpg",
                RequestId = SeedDataIds.Request2Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-18)
            },

            // Images for Request 3 (نظافت منزل)
            new RequestImage
            {
                Id = SeedDataIds.RequestImage4Id,
                ImgPath = "/images/request/1.jpg",
                RequestId = SeedDataIds.Request3Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-12)
            },
            new RequestImage
            {
                Id = SeedDataIds.RequestImage5Id,
                ImgPath = "/images/request/2.jpg",
                RequestId = SeedDataIds.Request3Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-12)
            },
            new RequestImage
            {
                Id = SeedDataIds.RequestImage6Id,
                ImgPath = "/images/request/3.jpg",
                RequestId = SeedDataIds.Request3Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-12)
            },

            // Images for Request 4 (رنگ اتاق)
            new RequestImage
            {
                Id = SeedDataIds.RequestImage7Id,
                ImgPath = "/images/request/1.jpg",
                RequestId = SeedDataIds.Request4Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-5)
            },
            new RequestImage
            {
                Id = SeedDataIds.RequestImage8Id,
                ImgPath = "/images/request/2.jpg",
                RequestId = SeedDataIds.Request4Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-5)
            },

            // Images for Request 5 (تعمیر یخچال)
            new RequestImage
            {
                Id = SeedDataIds.RequestImage9Id,
                ImgPath = "/images/request/1.jpg",
                RequestId = SeedDataIds.Request5Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-3)
            },

            // Images for Request 6 (نصب لوستر)
            new RequestImage
            {
                Id = SeedDataIds.RequestImage10Id,
                ImgPath = "/images/request/1.jpg",
                RequestId = SeedDataIds.Request6Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-1)
            },
            new RequestImage
            {
                Id = SeedDataIds.RequestImage11Id,
                ImgPath = "/images/request/2.jpg",
                RequestId = SeedDataIds.Request6Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-1)
            },

            // Images for Request 7 (قالیشویی)
            new RequestImage
            {
                Id = SeedDataIds.RequestImage12Id,
                ImgPath = "/images/request/1.jpg",
                RequestId = SeedDataIds.Request7Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddHours(-12)
            },

            // Images for Request 8 (تعمیر توالت)
            new RequestImage
            {
                Id = SeedDataIds.RequestImage13Id,
                ImgPath = "/images/request/1.jpg",
                RequestId = SeedDataIds.Request8Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddHours(-8)
            },

            // Images for Request 10 (تعمیر ماشین لباسشویی)
            new RequestImage
            {
                Id = SeedDataIds.RequestImage14Id,
                ImgPath = "/images/request/1.jpg",
                RequestId = SeedDataIds.Request10Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-14)
            },
            new RequestImage
            {
                Id = SeedDataIds.RequestImage15Id,
                ImgPath = "/images/request/2.jpg",
                RequestId = SeedDataIds.Request10Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-14)
            }
        );
    }
}
