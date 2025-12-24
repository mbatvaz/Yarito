using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options)
    : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options);

