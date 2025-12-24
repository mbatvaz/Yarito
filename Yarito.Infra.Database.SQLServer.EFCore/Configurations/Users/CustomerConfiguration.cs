using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Users;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(c => c.Address)
            .HasMaxLength(500)
            .IsUnicode();

        builder.HasMany(c => c.Requests)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Reviews)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
