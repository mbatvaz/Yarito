using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Users;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // Property Configurations
        builder.Property(c => c.Address)
            .HasMaxLength(500)
            .IsUnicode();

        // Relationships
        builder.HasMany(c => c.Requests)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Reviews)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed Data
        builder.HasData(
            new Customer
            {
                Id = SeedDataIds.Customer1Id,
                FirstName = "علی",
                LastName = "محمدی",
                Email = "ali.mohammadi@example.com",
                PhoneNumber = "09121234567",
                WalletBalance = 5000000,
                ProfileImgPath = "/images/profiles/customer1.jpg",
                CityId = SeedDataIds.TehranCityId,
                Address = "تهران، خیابان ولیعصر، پلاک 123",
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Customer
            {
                Id = SeedDataIds.Customer2Id,
                FirstName = "زهرا",
                LastName = "احمدی",
                Email = "zahra.ahmadi@example.com",
                PhoneNumber = "09131234567",
                WalletBalance = 3000000,
                ProfileImgPath = "/images/profiles/customer2.jpg",
                CityId = SeedDataIds.IsfahanCityId,
                Address = "اصفهان، خیابان چهارباغ، پلاک 45",
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Customer
            {
                Id = SeedDataIds.Customer3Id,
                FirstName = "محمد",
                LastName = "رضایی",
                Email = "mohammad.rezaei@example.com",
                PhoneNumber = "09141234567",
                WalletBalance = 8000000,
                ProfileImgPath = "/images/profiles/customer3.jpg",
                CityId = SeedDataIds.ShirazCityId,
                Address = "شیراز، خیابان زند، پلاک 78",
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Customer
            {
                Id = SeedDataIds.Customer4Id,
                FirstName = "فاطمه",
                LastName = "حسینی",
                Email = "fatemeh.hosseini@example.com",
                PhoneNumber = "09151234567",
                WalletBalance = 2000000,
                ProfileImgPath = "/images/profiles/customer4.jpg",
                CityId = SeedDataIds.KarajCityId,
                Address = "کرج، میدان آزادگان، پلاک 56",
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            },
            new Customer
            {
                Id = SeedDataIds.Customer5Id,
                FirstName = "حسین",
                LastName = "کریمی",
                Email = "hossein.karimi@example.com",
                PhoneNumber = "09161234567",
                WalletBalance = 10000000,
                ProfileImgPath = "/images/profiles/customer5.jpg",
                CityId = SeedDataIds.KashanCityId,
                Address = "کاشان، خیابان کمال الملک، پلاک 90",
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate
            }
        );
    }
}
