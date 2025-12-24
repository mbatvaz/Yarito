using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Cities;
using static Yarito.Infra.Database.SQLServer.EFCore.Configurations.SeedDataGuids;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Cities;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Name)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode();

        builder.HasOne(ci => ci.Parent)
            .WithMany(ci => ci.Children)
            .HasForeignKey(ci => ci.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);

        // SeedData - استان‌های ایران
        builder.HasData(
            new City { Id = TehranProvinceId, Name = "تهران", ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = IsfahanProvinceId, Name = "اصفهان", ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = KhorasanProvinceId, Name = "خراسان رضوی", ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = FarsProvinceId, Name = "فارس", ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = AzerbaijanProvinceId, Name = "آذربایجان شرقی", ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = KhuzestanProvinceId, Name = "خوزستان", ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = GilanProvinceId, Name = "گیلان", ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = MazandaranProvinceId, Name = "مازندران", ParentId = null, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - شهرهای استان تهران
        builder.HasData(
            new City { Id = TehranCityId, Name = "تهران", ParentId = TehranProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = KarajCityId, Name = "کرج", ParentId = TehranProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = ReyCityId, Name = "ری", ParentId = TehranProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - شهرهای استان اصفهان
        builder.HasData(
            new City { Id = IsfahanCityId, Name = "اصفهان", ParentId = IsfahanProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = KashanCityId, Name = "کاشان", ParentId = IsfahanProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = NajafAbadCityId, Name = "نجف‌آباد", ParentId = IsfahanProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - شهرهای استان خراسان رضوی
        builder.HasData(
            new City { Id = MashhadCityId, Name = "مشهد", ParentId = KhorasanProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = NeishaborCityId, Name = "نیشابور", ParentId = KhorasanProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = SabzevarCityId, Name = "سبزوار", ParentId = KhorasanProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - شهرهای استان فارس
        builder.HasData(
            new City { Id = ShirazCityId, Name = "شیراز", ParentId = FarsProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = MarvDashtCityId, Name = "مرودشت", ParentId = FarsProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - شهرهای استان آذربایجان شرقی
        builder.HasData(
            new City { Id = TabrizCityId, Name = "تبریز", ParentId = AzerbaijanProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = MaragheCityId, Name = "مراغه", ParentId = AzerbaijanProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - شهرهای استان خوزستان
        builder.HasData(
            new City { Id = AhvazCityId, Name = "اهواز", ParentId = KhuzestanProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = AbadanCityId, Name = "آبادان", ParentId = KhuzestanProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - شهرهای استان گیلان
        builder.HasData(
            new City { Id = RashtCityId, Name = "رشت", ParentId = GilanProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = AnzaliCityId, Name = "انزلی", ParentId = GilanProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );

        // SeedData - شهرهای استان مازندران
        builder.HasData(
            new City { Id = SariCityId, Name = "ساری", ParentId = MazandaranProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new City { Id = BabolCityId, Name = "بابل", ParentId = MazandaranProvinceId, IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );
    }
}
