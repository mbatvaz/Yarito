using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Categories;
using static Yarito.Infra.Database.SQLServer.EFCore.Configurations.SeedDataGuids;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Categories;

public class ExpertCategoryConfiguration : IEntityTypeConfiguration<ExpertCategory>
{
    public void Configure(EntityTypeBuilder<ExpertCategory> builder)
    {
        builder.HasKey(ec => ec.Id);

        builder.Property(ec => ec.ExpertId)
            .IsRequired();

        builder.Property(ec => ec.CategoryId)
            .IsRequired();

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

        // SeedData - ارتباط متخصصان با دسته‌بندی‌ها
        builder.HasData(
            // رضا نوری - لوله‌کشی و برق‌کاری
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000001"), ExpertId = Expert1Id, CategoryId = PlumbingId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000002"), ExpertId = Expert1Id, CategoryId = ElectricalId, IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // محمد صادقی - نقاشی ساختمان و کاشی‌کاری
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000003"), ExpertId = Expert2Id, CategoryId = PaintingId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000004"), ExpertId = Expert2Id, CategoryId = TilingId, IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // فاطمه موسوی - نظافت منزل و تعمیر لوازم خانگی
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000005"), ExpertId = Expert3Id, CategoryId = CleaningId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000006"), ExpertId = Expert3Id, CategoryId = ApplianceRepairId, IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // امیر قاسمی - مکانیک خودرو و صافکاری
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000007"), ExpertId = Expert4Id, CategoryId = MechanicId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000008"), ExpertId = Expert4Id, CategoryId = BodyWorkId, IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // نرگس کاظمی - آموزش زبان و موسیقی
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000009"), ExpertId = Expert5Id, CategoryId = LanguageId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000010"), ExpertId = Expert5Id, CategoryId = MusicId, IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // احمد رحیمی - برق‌کاری و تعمیر لوازم خانگی
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000011"), ExpertId = Expert6Id, CategoryId = ElectricalId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000012"), ExpertId = Expert6Id, CategoryId = ApplianceRepairId, IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // مریم یزدانی - آرایشگری زنانه و ماساژ درمانی
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000013"), ExpertId = Expert7Id, CategoryId = WomenHairdresserId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000014"), ExpertId = Expert7Id, CategoryId = MassageId, IsDeleted = false, CreatedAt = DateTime.UtcNow },

            // سعید حیدری - آموزش کامپیوتر و لوله‌کشی
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000015"), ExpertId = Expert8Id, CategoryId = ComputerId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new ExpertCategory { Id = Guid.Parse("40000000-0000-0000-0000-000000000016"), ExpertId = Expert8Id, CategoryId = PlumbingId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );
    }
}
