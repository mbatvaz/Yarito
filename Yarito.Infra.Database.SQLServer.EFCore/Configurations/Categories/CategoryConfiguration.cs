using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Categories;
using static Yarito.Infra.Database.SQLServer.EFCore.Configurations.SeedDataGuids;

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

        // SeedData - دسته‌بندی‌های اصلی
        builder.HasData(
            new Category { Id = BuildingRepairId, Title = "تعمیرات ساختمان", Description = "خدمات تعمیر و نگهداری ساختمان", BasePrice = 500_000, ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = HomeServicesId, Title = "خدمات منزل", Description = "خدمات مورد نیاز خانه و منزل", BasePrice = 300_000, ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = CarServicesId, Title = "خدمات خودرو", Description = "تعمیر و نگهداری خودرو", BasePrice = 600_000, ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = EducationId, Title = "آموزش", Description = "خدمات آموزشی", BasePrice = 400_000, ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = BeautyHealthId, Title = "زیبایی و سلامت", Description = "خدمات آرایشگری و بهداشتی", BasePrice = 350_000, ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - زیر دسته‌بندی‌های تعمیرات ساختمان
        builder.HasData(
            new Category { Id = PlumbingId, Title = "لوله‌کشی", Description = "تعمیر و نصب لوله‌کشی", BasePrice = 600_000, ParentId = BuildingRepairId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = ElectricalId, Title = "برق‌کاری", Description = "نصب و تعمیر تاسیسات برق", BasePrice = 550_000, ParentId = BuildingRepairId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = PaintingId, Title = "نقاشی ساختمان", Description = "رنگ‌آمیزی و نقاشی دیوار", BasePrice = 500_000, ParentId = BuildingRepairId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = TilingId, Title = "کاشی‌کاری", Description = "نصب و تعمیر کاشی و سرامیک", BasePrice = 700_000, ParentId = BuildingRepairId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - زیر دسته‌بندی‌های خدمات منزل
        builder.HasData(
            new Category { Id = CleaningId, Title = "نظافت منزل", Description = "خدمات نظافت و پاکیزگی", BasePrice = 400_000, ParentId = HomeServicesId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = MovingId, Title = "باربری", Description = "حمل و نقل اثاثیه منزل", BasePrice = 800_000, ParentId = HomeServicesId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = ApplianceRepairId, Title = "تعمیر لوازم خانگی", Description = "تعمیر یخچال، ماشین لباسشویی و...", BasePrice = 500_000, ParentId = HomeServicesId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = CookingId, Title = "آشپزی", Description = "خدمات پخت و پز غذا", BasePrice = 350_000, ParentId = HomeServicesId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - زیر دسته‌بندی‌های خدمات خودرو
        builder.HasData(
            new Category { Id = MechanicId, Title = "مکانیک خودرو", Description = "تعمیر موتور و قطعات خودرو", BasePrice = 700_000, ParentId = CarServicesId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = BodyWorkId, Title = "صافکاری و نقاشی", Description = "صافکاری و رنگ‌آمیزی بدنه", BasePrice = 900_000, ParentId = CarServicesId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = CarWashId, Title = "کارواش", Description = "شستشو و تمیزکاری خودرو", BasePrice = 300_000, ParentId = CarServicesId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - زیر دسته‌بندی‌های آموزش
        builder.HasData(
            new Category { Id = LanguageId, Title = "آموزش زبان", Description = "آموزش زبان‌های خارجی", BasePrice = 500_000, ParentId = EducationId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = MusicId, Title = "آموزش موسیقی", Description = "آموزش سازهای موسیقی", BasePrice = 600_000, ParentId = EducationId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = ComputerId, Title = "آموزش کامپیوتر", Description = "آموزش برنامه‌نویسی و نرم‌افزار", BasePrice = 550_000, ParentId = EducationId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - زیر دسته‌بندی‌های زیبایی و سلامت
        builder.HasData(
            new Category { Id = MenHairdresserId, Title = "آرایشگری مردانه", Description = "خدمات آرایشگری و اصلاح", BasePrice = 250_000, ParentId = BeautyHealthId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = WomenHairdresserId, Title = "آرایشگری زنانه", Description = "خدمات آرایش و زیبایی بانوان", BasePrice = 400_000, ParentId = BeautyHealthId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = MassageId, Title = "ماساژ درمانی", Description = "خدمات ماساژ و فیزیوتراپی", BasePrice = 500_000, ParentId = BeautyHealthId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );
    }
}
