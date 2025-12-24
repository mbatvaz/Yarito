using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Images;
using static Yarito.Infra.Database.SQLServer.EFCore.Configurations.SeedDataGuids;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Images;

public class ExpertImagesConfiguration : IEntityTypeConfiguration<ExpertImage>
{
    public void Configure(EntityTypeBuilder<ExpertImage> builder)
    {
        builder.HasKey(ei => ei.Id);

        builder.Property(ei => ei.Url)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ei => ei.ExpertId)
            .IsRequired();

        builder.HasOne(ei => ei.Expert)
            .WithMany(e => e.ExpertImages)
            .HasForeignKey(ei => ei.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);

        // SeedData - تصاویر نمونه کار متخصصان
        builder.HasData(
            // تصاویر رضا نوری (لوله‌کش و برق‌کار)
            new ExpertImage { Id = Expert1Image1Id, ExpertId = Expert1Id, Url = "/images/experts/works/expert1-work1.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertImage { Id = Expert1Image2Id, ExpertId = Expert1Id, Url = "/images/experts/works/expert1-work2.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertImage { Id = Expert1Image3Id, ExpertId = Expert1Id, Url = "/images/experts/works/expert1-work3.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // تصاویر محمد صادقی (نقاش و کاشی‌کار)
            new ExpertImage { Id = Expert2Image1Id, ExpertId = Expert2Id, Url = "/images/experts/works/expert2-work1.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertImage { Id = Expert2Image2Id, ExpertId = Expert2Id, Url = "/images/experts/works/expert2-work2.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertImage { Id = Expert2Image3Id, ExpertId = Expert2Id, Url = "/images/experts/works/expert2-work3.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // تصاویر فاطمه موسوی (نظافت و تعمیرکار)
            new ExpertImage { Id = Expert3Image1Id, ExpertId = Expert3Id, Url = "/images/experts/works/expert3-work1.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertImage { Id = Expert3Image2Id, ExpertId = Expert3Id, Url = "/images/experts/works/expert3-work2.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // تصاویر امیر قاسمی (مکانیک و صافکار)
            new ExpertImage { Id = Expert4Image1Id, ExpertId = Expert4Id, Url = "/images/experts/works/expert4-work1.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertImage { Id = Expert4Image2Id, ExpertId = Expert4Id, Url = "/images/experts/works/expert4-work2.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertImage { Id = Expert4Image3Id, ExpertId = Expert4Id, Url = "/images/experts/works/expert4-work3.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // تصاویر نرگس کاظمی (آموزش زبان و موسیقی)
            new ExpertImage { Id = Expert5Image1Id, ExpertId = Expert5Id, Url = "/images/experts/works/expert5-work1.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertImage { Id = Expert5Image2Id, ExpertId = Expert5Id, Url = "/images/experts/works/expert5-work2.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // تصاویر احمد رحیمی (برق‌کار و تعمیرکار)
            new ExpertImage { Id = Expert6Image1Id, ExpertId = Expert6Id, Url = "/images/experts/works/expert6-work1.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertImage { Id = Expert6Image2Id, ExpertId = Expert6Id, Url = "/images/experts/works/expert6-work2.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // تصاویر مریم یزدانی (آرایشگر و ماساژور)
            new ExpertImage { Id = Expert7Image1Id, ExpertId = Expert7Id, Url = "/images/experts/works/expert7-work1.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertImage { Id = Expert7Image2Id, ExpertId = Expert7Id, Url = "/images/experts/works/expert7-work2.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // تصاویر سعید حیدری (مدرس کامپیوتر و لوله‌کش)
            new ExpertImage { Id = Expert8Image1Id, ExpertId = Expert8Id, Url = "/images/experts/works/expert8-work1.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertImage { Id = Expert8Image2Id, ExpertId = Expert8Id, Url = "/images/experts/works/expert8-work2.jpg", IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );
    }
}
