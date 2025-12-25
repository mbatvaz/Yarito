using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Requests;

public class BidConfiguration : IEntityTypeConfiguration<Bid>
{
    public void Configure(EntityTypeBuilder<Bid> builder)
    {
        // Primary Key
        builder.HasKey(b => b.Id);
        
        // Indexes
        builder.HasIndex(b => new { b.RequestId, b.ExpertId })
            .IsUnique();
        
        builder.HasIndex(b => b.ExpertId);

        // Property Configurations
        builder.Property(b => b.Description)
            .HasMaxLength(1000)
            .IsUnicode();

        builder.Property(b => b.ProposedPrice)
            .IsRequired()
            .HasColumnType("decimal(18,0)");

        builder.Property(b => b.Status)
            .IsRequired()
            .HasDefaultValue(BidStatusEnum.Pending);

        builder.Property(b => b.ProposedVisitDateTime)
            .IsRequired();
        
        // Relationships
        builder.HasOne(b => b.Request)
            .WithMany(r => r.Bids)
            .HasForeignKey(b => b.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Expert)
            .WithMany(e => e.Bids)
            .HasForeignKey(b => b.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        // Query Filter
        builder.HasQueryFilter(x => !x.IsDeleted);

        // Seed Data
        builder.HasData(
            // Bids for Request 1 (Completed - سیم‌کشی آشپزخانه)
            new Bid
            {
                Id = SeedDataIds.Bid1Id,
                Description = "سلام، تجربه 10 ساله در سیم‌کشی دارم. کار رو با کیفیت عالی انجام میدم.",
                ProposedPrice = 2300000,
                ProposedVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(-20),
                Status = BidStatusEnum.Accepted,
                RequestId = SeedDataIds.Request1Id,
                ExpertId = SeedDataIds.Expert1Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-24)
            },
            new Bid
            {
                Id = SeedDataIds.Bid2Id,
                Description = "آماده انجام کار هستم با قیمت مناسب",
                ProposedPrice = 2600000,
                ProposedVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(-19),
                Status = BidStatusEnum.Rejected,
                RequestId = SeedDataIds.Request1Id,
                ExpertId = SeedDataIds.Expert5Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-24)
            },

            // Bids for Request 2 (Completed - تعمیر شیر آب)
            new Bid
            {
                Id = SeedDataIds.Bid3Id,
                Description = "متخصص لوله کشی با 8 سال سابقه، همراه با گارانتی",
                ProposedPrice = 480000,
                ProposedVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(-15),
                Status = BidStatusEnum.Accepted,
                RequestId = SeedDataIds.Request2Id,
                ExpertId = SeedDataIds.Expert2Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-17)
            },

            // Bids for Request 3 (Completed - نظافت منزل)
            new Bid
            {
                Id = SeedDataIds.Bid4Id,
                Description = "با ضمانت کیفیت و قیمت عالی",
                ProposedPrice = 1750000,
                ProposedVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(-10),
                Status = BidStatusEnum.Accepted,
                RequestId = SeedDataIds.Request3Id,
                ExpertId = SeedDataIds.Expert3Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-11)
            },

            // Bids for Request 4 (InProgress - رنگ اتاق)
            new Bid
            {
                Id = SeedDataIds.Bid5Id,
                Description = "نقاش حرفه‌ای با سابقه کار روی پروژه‌های بزرگ",
                ProposedPrice = 3800000,
                ProposedVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(4),
                Status = BidStatusEnum.Rejected,
                RequestId = SeedDataIds.Request4Id,
                ExpertId = SeedDataIds.Expert4Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-4)
            },

            // Bids for Request 5 (InProgress - تعمیر یخچال)
            new Bid
            {
                Id = SeedDataIds.Bid6Id,
                Description = "تعمیر فوری یخچال با بهترین قیمت",
                ProposedPrice = 1400000,
                ProposedVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(5),
                Status = BidStatusEnum.Accepted,
                RequestId = SeedDataIds.Request5Id,
                ExpertId = SeedDataIds.Expert5Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-2)
            },

            // Bids for Request 6 (Pending - نصب لوستر)
            new Bid
            {
                Id = SeedDataIds.Bid7Id,
                Description = "نصب لوستر با تجربه بالا",
                ProposedPrice = 550000,
                ProposedVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(7),
                Status = BidStatusEnum.Pending,
                RequestId = SeedDataIds.Request6Id,
                ExpertId = SeedDataIds.Expert1Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddHours(-20)
            },
            new Bid
            {
                Id = SeedDataIds.Bid8Id,
                Description = "آماده نصب در کمترین زمان",
                ProposedPrice = 650000,
                ProposedVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(8),
                Status = BidStatusEnum.Pending,
                RequestId = SeedDataIds.Request6Id,
                ExpertId = SeedDataIds.Expert5Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddHours(-18)
            },

            // Bids for Request 7 (Pending - قالیشویی)
            new Bid
            {
                Id = SeedDataIds.Bid9Id,
                Description = "قالیشویی با دستگاه‌های مدرن",
                ProposedPrice = 2800000,
                ProposedVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(10),
                Status = BidStatusEnum.Pending,
                RequestId = SeedDataIds.Request7Id,
                ExpertId = SeedDataIds.Expert3Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddHours(-10)
            },

            // Bids for Request 8 (Pending - تعمیر توالت)
            new Bid
            {
                Id = SeedDataIds.Bid10Id,
                Description = "تعمیر فوری توالت فرنگی",
                ProposedPrice = 650000,
                ProposedVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(2),
                Status = BidStatusEnum.Pending,
                RequestId = SeedDataIds.Request8Id,
                ExpertId = SeedDataIds.Expert2Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddHours(-6)
            },

            // Bid for Request 10 (Completed - تعمیر ماشین لباسشویی)
            new Bid
            {
                Id = SeedDataIds.Bid11Id,
                Description = "تعمیر ماشین لباسشویی با گارانتی کامل",
                ProposedPrice = 1150000,
                ProposedVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(-8),
                Status = BidStatusEnum.Accepted,
                RequestId = SeedDataIds.Request10Id,
                ExpertId = SeedDataIds.Expert5Id,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-13)
            }
        );
    }
}
