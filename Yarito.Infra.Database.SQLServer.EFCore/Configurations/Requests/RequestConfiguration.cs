using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Requests;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.HasKey(r => r.Id);
        
        builder.HasIndex(r => r.CustomerId);

        builder.HasIndex(r => r.CategoryId);

        builder.HasIndex(r => r.Status);

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(r => r.Description)
            .HasMaxLength(1000)
            .IsUnicode();

        builder.Property(r => r.ProposedPrice)
            .HasColumnType("decimal(18,0)");

        builder.Property(r => r.Address)
            .IsRequired()
            .HasMaxLength(250)
            .IsUnicode();

        builder.Property(r => r.Status)
            .HasDefaultValue(RequestStatusEnum.Pending);

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Requests)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Category)
            .WithMany(c => c.Requests)
            .HasForeignKey(r => r.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.AcceptedBid)
            .WithOne()
            .HasForeignKey<Request>(r => r.AcceptedBidId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.Bids)
            .WithOne(b => b.Request)
            .HasForeignKey(b => b.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.RequestImages)
            .WithOne(ri => ri.Request)
            .HasForeignKey(ri => ri.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
