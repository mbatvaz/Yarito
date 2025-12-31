using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Users;
using Yarito.Domain.Core.Enums.Users;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Users;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUsers");

        // Table Per Hierarchy (TPH) Configuration
        builder.HasDiscriminator<UserTypeEnum>("UserType")
            .HasValue<Customer>(UserTypeEnum.Customer)
            .HasValue<Expert>(UserTypeEnum.Expert);

        builder.HasKey(u => u.Id);

        // Indexes
        builder.HasIndex(u => u.PhoneNumber)
            .IsUnique();

        // Property Configurations
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

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
            .HasMaxLength(11)
            .IsFixedLength();

        builder.Property(u => u.WalletBalance)
            .HasColumnType("decimal(18,0)")
            .HasDefaultValue(0);

        builder.Property(u => u.ProfileImgPath)
            .HasMaxLength(100);


        // Relationships
        builder.HasOne(au => au.City)
            .WithMany(ci => ci.Users)
            .HasForeignKey(au => au.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        // Query Filter
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
