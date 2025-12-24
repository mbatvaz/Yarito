using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Users;
using static Yarito.Infra.Database.SQLServer.EFCore.Configurations.SeedDataGuids;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Users;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUsers");

        builder.HasDiscriminator<string>("UserType")
            .HasValue<Customer>("Customer")
            .HasValue<Expert>("Expert");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .HasMaxLength(50)
            .IsUnicode();

        builder.Property(u => u.LastName)
            .HasMaxLength(50)
            .IsUnicode();

        builder.Property(u => u.Email)
            .HasMaxLength(256);

        builder.Property(u => u.PhoneNumber)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(u => u.WalletBalance)
            .HasColumnType("decimal(18,0)")
            .HasDefaultValue(0);

        builder.Property(u => u.ProfileImageUrl)
            .HasMaxLength(100);

        builder.HasOne(au => au.City)
            .WithMany(ci => ci.Users)
            .HasForeignKey(au => au.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);

        // SeedData - مشتریان (Customers)
        builder.HasData(
            new Customer 
            { 
                Id = Customer1Id, 
                FirstName = "علی", 
                LastName = "احمدی", 
                Email = "ali.ahmadi@example.com", 
                PhoneNumber = "09121234567",
                CityId = TehranCityId,
                Address = "تهران، میدان ولیعصر، خیابان کریمخان",
                WalletBalance = 5000000,
                ProfileImageUrl = "/images/users/profile1.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            },
            new Customer 
            { 
                Id = Customer2Id, 
                FirstName = "سارا", 
                LastName = "محمدی", 
                Email = "sara.mohammadi@example.com", 
                PhoneNumber = "09121234568",
                CityId = TehranCityId,
                Address = "تهران، میدان آزادی، خیابان انقلاب",
                WalletBalance = 3000000,
                ProfileImageUrl = "/images/users/profile2.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            },
            new Customer 
            { 
                Id = Customer3Id, 
                FirstName = "حسین", 
                LastName = "رضایی", 
                Email = "hosein.rezaei@example.com", 
                PhoneNumber = "09131234567",
                CityId = IsfahanCityId,
                Address = "اصفهان، میدان نقش جهان، چهارباغ عباسی",
                WalletBalance = 2000000,
                ProfileImageUrl = "/images/users/profile3.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            },
            new Customer 
            { 
                Id = Customer4Id, 
                FirstName = "زهرا", 
                LastName = "کریمی", 
                Email = "zahra.karimi@example.com", 
                PhoneNumber = "09151234567",
                CityId = MashhadCityId,
                Address = "مشهد، بلوار کوهسنگی، خیابان امام رضا",
                WalletBalance = 4000000,
                ProfileImageUrl = "/images/users/profile4.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            },
            new Customer 
            { 
                Id = Customer5Id, 
                FirstName = "مهدی", 
                LastName = "حسینی", 
                Email = "mehdi.hosseini@example.com", 
                PhoneNumber = "09171234567",
                CityId = ShirazCityId,
                Address = "شیراز، میدان شهدا، خیابان زند",
                WalletBalance = 3500000,
                ProfileImageUrl = "/images/users/profile5.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            }
        );

        // SeedData - متخصصان (Experts)
        builder.HasData(
            new Expert 
            { 
                Id = Expert1Id, 
                FirstName = "رضا", 
                LastName = "نوری", 
                Email = "reza.noori@example.com", 
                PhoneNumber = "09121111111",
                CityId = TehranCityId,
                WalletBalance = 10000000,
                ProfileImageUrl = "/images/experts/expert1.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            },
            new Expert 
            { 
                Id = Expert2Id, 
                FirstName = "محمد", 
                LastName = "صادقی", 
                Email = "mohammad.sadeghi@example.com", 
                PhoneNumber = "09121111112",
                CityId = TehranCityId,
                WalletBalance = 8000000,
                ProfileImageUrl = "/images/experts/expert2.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            },
            new Expert 
            { 
                Id = Expert3Id, 
                FirstName = "فاطمه", 
                LastName = "موسوی", 
                Email = "fatemeh.mousavi@example.com", 
                PhoneNumber = "09131111111",
                CityId = IsfahanCityId,
                WalletBalance = 6000000,
                ProfileImageUrl = "/images/experts/expert3.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            },
            new Expert 
            { 
                Id = Expert4Id, 
                FirstName = "امیر", 
                LastName = "قاسمی", 
                Email = "amir.ghasemi@example.com", 
                PhoneNumber = "09151111111",
                CityId = MashhadCityId,
                WalletBalance = 9000000,
                ProfileImageUrl = "/images/experts/expert4.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            },
            new Expert 
            { 
                Id = Expert5Id, 
                FirstName = "نرگس", 
                LastName = "کاظمی", 
                Email = "narges.kazemi@example.com", 
                PhoneNumber = "09171111111",
                CityId = ShirazCityId,
                WalletBalance = 7500000,
                ProfileImageUrl = "/images/experts/expert5.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            },
            new Expert 
            { 
                Id = Expert6Id, 
                FirstName = "احمد", 
                LastName = "رحیمی", 
                Email = "ahmad.rahimi@example.com", 
                PhoneNumber = "09121111113",
                CityId = TehranCityId,
                WalletBalance = 8500000,
                ProfileImageUrl = "/images/experts/expert6.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            },
            new Expert 
            { 
                Id = Expert7Id, 
                FirstName = "مریم", 
                LastName = "یزدانی", 
                Email = "maryam.yazdani@example.com", 
                PhoneNumber = "09131111112",
                CityId = IsfahanCityId,
                WalletBalance = 6500000,
                ProfileImageUrl = "/images/experts/expert7.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            },
            new Expert 
            { 
                Id = Expert8Id, 
                FirstName = "سعید", 
                LastName = "حیدری", 
                Email = "saeed.heydari@example.com", 
                PhoneNumber = "09151111112",
                CityId = MashhadCityId,
                WalletBalance = 7000000,
                ProfileImageUrl = "/images/experts/expert8.jpg",
                IsDeleted = false, 
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}
