using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Yarito.Infra.Database.SQLServer.Identity.Configurations;

public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUser<int>> builder)
    {
        var passwordHasher = new PasswordHasher<IdentityUser<int>>();

        // Admin
        var admin = new IdentityUser<int>
        {
            Id = 11,
            UserName = "09214507392",
            NormalizedUserName = "09214507392",
            Email = "admin@yarito.com",
            NormalizedEmail = "ADMIN@YARITO.COM",
            EmailConfirmed = true,
            PhoneNumber = "09214507392",
            PhoneNumberConfirmed = true,
            SecurityStamp = "1K2L3M4N5O6P7Q8R9S0T",
            ConcurrencyStamp = "K1L2M3N4-O5P6-Q7R8-S9T0-U1V2W3X4Y5Z6"
        };
        admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin@123");

        // Customers
        var customer1 = new IdentityUser<int>
        {
            Id = 1,
            UserName = "09121234567",
            NormalizedUserName = "09121234567",
            Email = "ali.mohammadi@example.com",
            NormalizedEmail = "ALI.MOHAMMADI@EXAMPLE.COM",
            EmailConfirmed = true,
            PhoneNumber = "09121234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "1A2B3C4D5E6F7G8H9I0J",
            ConcurrencyStamp = "A1B2C3D4-E5F6-G7H8-I9J0-K1L2M3N4O5P6"
        };
        customer1.PasswordHash = passwordHasher.HashPassword(customer1, "Customer@123");

        var customer2 = new IdentityUser<int>
        {
            Id = 2,
            UserName = "09131234567",
            NormalizedUserName = "09131234567",
            Email = "zahra.ahmadi@example.com",
            NormalizedEmail = "ZAHRA.AHMADI@EXAMPLE.COM",
            EmailConfirmed = true,
            PhoneNumber = "09131234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "2B3C4D5E6F7G8H9I0J1K",
            ConcurrencyStamp = "B2C3D4E5-F6G7-H8I9-J0K1-L2M3N4O5P6Q7"
        };
        customer2.PasswordHash = passwordHasher.HashPassword(customer2, "Customer@123");

        var customer3 = new IdentityUser<int>
        {
            Id = 3,
            UserName = "09141234567",
            NormalizedUserName = "09141234567",
            Email = "mohammad.rezaei@example.com",
            NormalizedEmail = "MOHAMMAD.REZAEI@EXAMPLE.COM",
            EmailConfirmed = true,
            PhoneNumber = "09141234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "3C4D5E6F7G8H9I0J1K2L",
            ConcurrencyStamp = "C3D4E5F6-G7H8-I9J0-K1L2-M3N4O5P6Q7R8"
        };
        customer3.PasswordHash = passwordHasher.HashPassword(customer3, "Customer@123");

        var customer4 = new IdentityUser<int>
        {
            Id = 4,
            UserName = "09151234567",
            NormalizedUserName = "09151234567",
            Email = "fatemeh.hosseini@example.com",
            NormalizedEmail = "FATEMEH.HOSSEINI@EXAMPLE.COM",
            EmailConfirmed = true,
            PhoneNumber = "09151234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "4D5E6F7G8H9I0J1K2L3M",
            ConcurrencyStamp = "D4E5F6G7-H8I9-J0K1-L2M3-N4O5P6Q7R8S9"
        };
        customer4.PasswordHash = passwordHasher.HashPassword(customer4, "Customer@123");

        var customer5 = new IdentityUser<int>
        {
            Id = 5,
            UserName = "09161234567",
            NormalizedUserName = "09161234567",
            Email = "hossein.karimi@example.com",
            NormalizedEmail = "HOSSEIN.KARIMI@EXAMPLE.COM",
            EmailConfirmed = true,
            PhoneNumber = "09161234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "5E6F7G8H9I0J1K2L3M4N",
            ConcurrencyStamp = "E5F6G7H8-I9J0-K1L2-M3N4-O5P6Q7R8S9T0"
        };
        customer5.PasswordHash = passwordHasher.HashPassword(customer5, "Customer@123");

        // Experts
        var expert1 = new IdentityUser<int>
        {
            Id = 6,
            UserName = "09171234567",
            NormalizedUserName = "09171234567",
            Email = "reza.bargkar@example.com",
            NormalizedEmail = "REZA.BARGKAR@EXAMPLE.COM",
            EmailConfirmed = true,
            PhoneNumber = "09171234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "6F7G8H9I0J1K2L3M4N5O",
            ConcurrencyStamp = "F6G7H8I9-J0K1-L2M3-N4O5-P6Q7R8S9T0U1"
        };
        expert1.PasswordHash = passwordHasher.HashPassword(expert1, "Expert@123");

        var expert2 = new IdentityUser<int>
        {
            Id = 7,
            UserName = "09181234567",
            NormalizedUserName = "09181234567",
            Email = "mehdi.loolehkesh@example.com",
            NormalizedEmail = "MEHDI.LOOLEHKESH@EXAMPLE.COM",
            EmailConfirmed = true,
            PhoneNumber = "09181234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "7G8H9I0J1K2L3M4N5O6P",
            ConcurrencyStamp = "G7H8I9J0-K1L2-M3N4-O5P6-Q7R8S9T0U1V2"
        };
        expert2.PasswordHash = passwordHasher.HashPassword(expert2, "Expert@123");

        var expert3 = new IdentityUser<int>
        {
            Id = 8,
            UserName = "09191234567",
            NormalizedUserName = "09191234567",
            Email = "sara.nezafatchi@example.com",
            NormalizedEmail = "SARA.NEZAFATCHI@EXAMPLE.COM",
            EmailConfirmed = true,
            PhoneNumber = "09191234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "8H9I0J1K2L3M4N5O6P7Q",
            ConcurrencyStamp = "H8I9J0K1-L2M3-N4O5-P6Q7-R8S9T0U1V2W3"
        };
        expert3.PasswordHash = passwordHasher.HashPassword(expert3, "Expert@123");

        var expert4 = new IdentityUser<int>
        {
            Id = 9,
            UserName = "09201234567",
            NormalizedUserName = "09201234567",
            Email = "ahmad.naghash@example.com",
            NormalizedEmail = "AHMAD.NAGHASH@EXAMPLE.COM",
            EmailConfirmed = true,
            PhoneNumber = "09201234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "9I0J1K2L3M4N5O6P7Q8R",
            ConcurrencyStamp = "I9J0K1L2-M3N4-O5P6-Q7R8-S9T0U1V2W3X4"
        };
        expert4.PasswordHash = passwordHasher.HashPassword(expert4, "Expert@123");

        var expert5 = new IdentityUser<int>
        {
            Id = 10,
            UserName = "09211234567",
            NormalizedUserName = "09211234567",
            Email = "narges.tamirkar@example.com",
            NormalizedEmail = "NARGES.TAMIRKAR@EXAMPLE.COM",
            EmailConfirmed = true,
            PhoneNumber = "09211234567",
            PhoneNumberConfirmed = true,
            SecurityStamp = "0J1K2L3M4N5O6P7Q8R9S",
            ConcurrencyStamp = "J0K1L2M3-N4O5-P6Q7-R8S9-T0U1V2W3X4Y5"
        };
        expert5.PasswordHash = passwordHasher.HashPassword(expert5, "Expert@123");


        builder.HasData(
            admin,
            customer1, customer2, customer3, customer4, customer5,
            expert1, expert2, expert3, expert4, expert5
        );
    }
}