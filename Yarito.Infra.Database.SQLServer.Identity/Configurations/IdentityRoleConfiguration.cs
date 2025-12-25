using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Yarito.Infra.Database.SQLServer.Identity.Configurations;

public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
    {
        builder.HasData(
            new IdentityRole<int>
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "A1B2C3D4-E5F6-4789-A1B2-C3D4E5F6A7B8"
            },
            new IdentityRole<int>
            {
                Id = 2,
                Name = "Customer",
                NormalizedName = "CUSTOMER",
                ConcurrencyStamp = "B2C3D4E5-F6A7-4890-B2C3-D4E5F6A7B8C9"
            },
            new IdentityRole<int>
            {
                Id = 3,
                Name = "Expert",
                NormalizedName = "EXPERT",
                ConcurrencyStamp = "C3D4E5F6-A7B8-4901-C3D4-E5F6A7B8C9D0"
            }
        );
    }
}
