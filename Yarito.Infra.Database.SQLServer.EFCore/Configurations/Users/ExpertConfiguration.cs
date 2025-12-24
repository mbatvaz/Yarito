using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Users;

public class ExpertConfiguration : IEntityTypeConfiguration<Expert>
{
    public void Configure(EntityTypeBuilder<Expert> builder)
    {
        builder.HasMany(e => e.ExpertCategories)
            .WithOne(ec => ec.Expert)
            .HasForeignKey(ec => ec.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Bids)
            .WithOne(b => b.Expert)
            .HasForeignKey(b => b.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Reviews)
            .WithOne(r => r.Expert)
            .HasForeignKey(r => r.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.ExpertImages)
            .WithOne(ei => ei.Expert)
            .HasForeignKey(ei => ei.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
