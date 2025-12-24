using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Images;
using static Yarito.Infra.Database.SQLServer.EFCore.Configurations.SeedDataGuids;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Images;

public class RequestImagesConfiguration : IEntityTypeConfiguration<RequestImage>
{
    public void Configure(EntityTypeBuilder<RequestImage> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Url)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.RequestId)
            .IsRequired();

        builder.HasOne(i => i.Request)
            .WithMany(r => r.RequestImages)
            .HasForeignKey(i => i.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);

        var now = DateTime.UtcNow;

        // SeedData - تصاویر درخواست‌ها
        builder.HasData(
            // تصاویر درخواست 1 - تعمیر شیر آب
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000001"), RequestId = Request1Id, Url = "/images/requests/request1-img1.jpg", IsDeleted = false, CreatedAt = now.AddDays(-5) },
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000002"), RequestId = Request1Id, Url = "/images/requests/request1-img2.jpg", IsDeleted = false, CreatedAt = now.AddDays(-5) },

            // تصاویر درخواست 2 - نظافت منزل
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000003"), RequestId = Request2Id, Url = "/images/requests/request2-img1.jpg", IsDeleted = false, CreatedAt = now.AddDays(-3) },

            // تصاویر درخواست 3 - تعمیر موتور خودرو
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000004"), RequestId = Request3Id, Url = "/images/requests/request3-img1.jpg", IsDeleted = false, CreatedAt = now.AddDays(-2) },
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000005"), RequestId = Request3Id, Url = "/images/requests/request3-img2.jpg", IsDeleted = false, CreatedAt = now.AddDays(-2) },
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000006"), RequestId = Request3Id, Url = "/images/requests/request3-img3.jpg", IsDeleted = false, CreatedAt = now.AddDays(-2) },

            // تصاویر درخواست 4 - نصب لوستر
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000007"), RequestId = Request4Id, Url = "/images/requests/request4-img1.jpg", IsDeleted = false, CreatedAt = now.AddDays(-10) },
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000008"), RequestId = Request4Id, Url = "/images/requests/request4-img2.jpg", IsDeleted = false, CreatedAt = now.AddDays(-10) },

            // تصاویر درخواست 6 - رنگ‌آمیزی اتاق
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000009"), RequestId = Request6Id, Url = "/images/requests/request6-img1.jpg", IsDeleted = false, CreatedAt = now.AddDays(-30) },
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000010"), RequestId = Request6Id, Url = "/images/requests/request6-img2.jpg", IsDeleted = false, CreatedAt = now.AddDays(-30) },

            // تصاویر درخواست 7 - تعمیر یخچال
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000011"), RequestId = Request7Id, Url = "/images/requests/request7-img1.jpg", IsDeleted = false, CreatedAt = now.AddDays(-25) },

            // تصاویر درخواست 9 - تعویض لوله‌ها
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000012"), RequestId = Request9Id, Url = "/images/requests/request9-img1.jpg", IsDeleted = false, CreatedAt = now.AddDays(-20) },
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000013"), RequestId = Request9Id, Url = "/images/requests/request9-img2.jpg", IsDeleted = false, CreatedAt = now.AddDays(-20) },

            // تصاویر درخواست 10 - صافکاری خودرو (لغو شده)
            new RequestImage { Id = Guid.Parse("80000000-0000-0000-0000-000000000014"), RequestId = Request10Id, Url = "/images/requests/request10-img1.jpg", IsDeleted = false, CreatedAt = now.AddDays(-18) }
        );
    }
}
