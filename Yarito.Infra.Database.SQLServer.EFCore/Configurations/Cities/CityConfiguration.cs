using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Cities;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Cities;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        // Primary Key
        builder.HasKey(ci => ci.Id);

        // Property Configurations
        builder.Property(ci => ci.Name)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode();

        // Relationships
        builder.HasOne(ci => ci.Parent)
            .WithMany(ci => ci.Children)
            .HasForeignKey(ci => ci.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Query Filter
        builder.HasQueryFilter(x => !x.IsDeleted);

        // Seed Data
        builder.HasData(
            new City
            {
                Id = SeedDataIds.TehranProvinceId,
                Name = "تهران",
                ParentId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new City
            {
                Id = SeedDataIds.IsfahanProvinceId,
                Name = "اصفهان",
                ParentId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new City
            {
                Id = SeedDataIds.ShirazProvinceId,
                Name = "فارس",
                ParentId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
        // شهرهای تهران
            new City
            {
                Id = SeedDataIds.TehranCityId,
                Name = "تهران",
                ParentId = SeedDataIds.TehranProvinceId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new City
            {
                Id = SeedDataIds.KarajCityId,
                Name = "کرج",
                ParentId = SeedDataIds.TehranProvinceId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new City
            {
                Id = SeedDataIds.RayCityId,
                Name = "ری",
                ParentId = SeedDataIds.TehranProvinceId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
        // شهرهای اصفهان
            new City
            {
                Id = SeedDataIds.IsfahanCityId,
                Name = "اصفهان",
                ParentId = SeedDataIds.IsfahanProvinceId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new City
            {
                Id = SeedDataIds.NajafabadCityId,
                Name = "نجف‌آباد",
                ParentId = SeedDataIds.IsfahanProvinceId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new City
            {
                Id = SeedDataIds.KashanCityId,
                Name = "کاشان",
                ParentId = SeedDataIds.IsfahanProvinceId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
        // شهرهای فارس
            new City
            {
                Id = SeedDataIds.ShirazCityId,
                Name = "شیراز",
                ParentId = SeedDataIds.ShirazProvinceId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new City
            {
                Id = SeedDataIds.MarvsdashtCityId,
                Name = "مرودشت",
                ParentId = SeedDataIds.ShirazProvinceId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new City
            {
                Id = SeedDataIds.JahromCityId,
                Name = "جهرم",
                ParentId = SeedDataIds.ShirazProvinceId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            }
        );
    }
}
