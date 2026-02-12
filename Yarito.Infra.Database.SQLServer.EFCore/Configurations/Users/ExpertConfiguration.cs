using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Users;

public class ExpertConfiguration : IEntityTypeConfiguration<Expert>
{
    public void Configure(EntityTypeBuilder<Expert> builder)
    {
        builder.HasMany(e => e.Bids)
            .WithOne(b => b.Expert)
            .HasForeignKey(b => b.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Reviews)
            .WithOne(r => r.Expert)
            .HasForeignKey(r => r.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed Data
        builder.HasData(
            new Expert
            {
                Id = SeedDataIds.Expert1Id,
                FirstName = "رضا",
                LastName = "برقکار",
                Email = "reza.bargkar@example.com",
                PhoneNumber = "09171234567",
                WalletBalance = 15000000,
                ProfileImgPath = "/images/profiles/expert1.jpg",
                CityId = SeedDataIds.TehranCityId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Expert
            {
                Id = SeedDataIds.Expert2Id,
                FirstName = "مهدی",
                LastName = "لوله‌کش",
                Email = "mehdi.loolehkesh@example.com",
                PhoneNumber = "09181234567",
                WalletBalance = 12000000,
                ProfileImgPath = "/images/profiles/expert2.jpg",
                CityId = SeedDataIds.IsfahanCityId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Expert
            {
                Id = SeedDataIds.Expert3Id,
                FirstName = "سارا",
                LastName = "نظافتچی",
                Email = "sara.nezafatchi@example.com",
                PhoneNumber = "09191234567",
                WalletBalance = 8000000,
                ProfileImgPath = "/images/profiles/expert3.jpg",
                CityId = SeedDataIds.ShirazCityId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Expert
            {
                Id = SeedDataIds.Expert4Id,
                FirstName = "احمد",
                LastName = "نقاش",
                Email = "ahmad.naghash@example.com",
                PhoneNumber = "09201234567",
                WalletBalance = 20000000,
                ProfileImgPath = "/images/profiles/expert4.jpg",
                CityId = SeedDataIds.KarajCityId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Expert
            {
                Id = SeedDataIds.Expert5Id,
                FirstName = "نرگس",
                LastName = "تعمیرکار",
                Email = "narges.tamirkar@example.com",
                PhoneNumber = "09211234567",
                WalletBalance = 18000000,
                ProfileImgPath = "/images/profiles/expert5.jpg",
                CityId = SeedDataIds.TehranCityId,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            }
        );
    }
}
