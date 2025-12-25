using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Yarito.Infra.Database.SQLServer.Identity.Configurations;
public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
    {
        builder.HasData(
            // Customers
            new IdentityUserRole<int> { UserId = 1, RoleId = 2 },
            new IdentityUserRole<int> { UserId = 2, RoleId = 2 },
            new IdentityUserRole<int> { UserId = 3, RoleId = 2 },
            new IdentityUserRole<int> { UserId = 4, RoleId = 2 },
            new IdentityUserRole<int> { UserId = 5, RoleId = 2 },

            // Experts
            new IdentityUserRole<int> { UserId = 6, RoleId = 3 },
            new IdentityUserRole<int> { UserId = 7, RoleId = 3 },
            new IdentityUserRole<int> { UserId = 8, RoleId = 3 },
            new IdentityUserRole<int> { UserId = 9, RoleId = 3 },
            new IdentityUserRole<int> { UserId = 10, RoleId = 3 }
        );
    }
}