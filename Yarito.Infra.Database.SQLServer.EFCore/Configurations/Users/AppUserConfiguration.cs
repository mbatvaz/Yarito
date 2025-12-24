using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Users;

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
    }
}
