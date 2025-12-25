using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Requests;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        // Primary Key
        builder.HasKey(r => r.Id); 

        builder.HasIndex(r => r.CustomerId);

        // Property Configurations
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

        // Relationships
        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Requests)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Work)
            .WithMany(w => w.Requests)
            .HasForeignKey(r => r.WorkId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.AcceptedBid)
            .WithOne()
            .HasForeignKey<Request>(r => r.AcceptedBidId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasMany(r => r.Bids)
            .WithOne(b => b.Request)
            .HasForeignKey(b => b.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.RequestImages)
            .WithOne(ri => ri.Request)
            .HasForeignKey(ri => ri.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        // Query Filter
        builder.HasQueryFilter(x => !x.IsDeleted);

        // Seed Data
        builder.HasData(
            // Request 1 - Completed
            new Request
            {
                Id = SeedDataIds.Request1Id,
                Title = "سیم‌کشی آشپزخانه",
                Description = "نیاز به سیم‌کشی کامل آشپزخانه برای نصب هود و کابینت",
                ProposedPrice = 2500000,
                Address = "تهران، خیابان ولیعصر، پلاک 123، واحد 5",
                PreferredVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(-20),
                Status = RequestStatusEnum.Completed,
                CustomerId = SeedDataIds.Customer1Id,
                WorkId = SeedDataIds.HomeWiringWorkId,
                AcceptedBidId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-25)
            },

            // Request 2 - Completed
            new Request
            {
                Id = SeedDataIds.Request2Id,
                Title = "تعمیر شیر آب آشپزخانه",
                Description = "شیر آب آشپزخانه نشتی دارد و نیاز به تعویض دارد",
                ProposedPrice = 500000,
                Address = "اصفهان، خیابان چهارباغ، پلاک 45، طبقه سوم",
                PreferredVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(-15),
                Status = RequestStatusEnum.Completed,
                CustomerId = SeedDataIds.Customer2Id,
                WorkId = SeedDataIds.FaucetInstallationWorkId,
                AcceptedBidId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-18)
            },

            // Request 3 - Completed
            new Request
            {
                Id = SeedDataIds.Request3Id,
                Title = "نظافت کامل منزل",
                Description = "نظافت کامل منزل 120 متری قبل از نوروز",
                ProposedPrice = 1800000,
                Address = "شیراز، خیابان زند، پلاک 78، واحد 12",
                PreferredVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(-10),
                Status = RequestStatusEnum.Completed,
                CustomerId = SeedDataIds.Customer3Id,
                WorkId = SeedDataIds.HomeCleaningWorkId,
                AcceptedBidId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-12)
            },

            // Request 4 - InProgress
            new Request
            {
                Id = SeedDataIds.Request4Id,
                Title = "رنگ اتاق خواب",
                Description = "نقاشی و رنگ‌آمیزی دو اتاق خواب",
                ProposedPrice = 3500000,
                Address = "کرج، میدان آزادگان، پلاک 56، واحد 8",
                PreferredVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(3),
                Status = RequestStatusEnum.InProgress,
                CustomerId = SeedDataIds.Customer4Id,
                WorkId = SeedDataIds.InteriorPaintingWorkId,
                AcceptedBidId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-5)
            },

            // Request 5 - InProgress
            new Request
            {
                Id = SeedDataIds.Request5Id,
                Title = "تعمیر یخچال سامسونگ",
                Description = "یخچال یخ نمی‌زند و نیاز به بررسی دارد",
                ProposedPrice = 1500000,
                Address = "کاشان، خیابان کمال الملک، پلاک 90، طبقه دوم",
                PreferredVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(5),
                Status = RequestStatusEnum.InProgress,
                CustomerId = SeedDataIds.Customer5Id,
                WorkId = SeedDataIds.RefrigeratorRepairWorkId,
                AcceptedBidId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-3)
            },

            // Request 6 - Pending
            new Request
            {
                Id = SeedDataIds.Request6Id,
                Title = "نصب لوستر سالن",
                Description = "نصب یک لوستر سنگین در سالن پذیرایی",
                ProposedPrice = 600000,
                Address = "تهران، خیابان انقلاب، پلاک 200، واحد 3",
                PreferredVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(7),
                Status = RequestStatusEnum.Pending,
                CustomerId = SeedDataIds.Customer1Id,
                WorkId = SeedDataIds.LightInstallationWorkId,
                AcceptedBidId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-1)
            },

            // Request 7 - Pending
            new Request
            {
                Id = SeedDataIds.Request7Id,
                Title = "شستشوی فرش ایرانی",
                Description = "قالیشویی یک فرش دستباف 12 متری",
                ProposedPrice = 3000000,
                Address = "اصفهان، خیابان سپاهان، پلاک 150، واحد 6",
                PreferredVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(10),
                Status = RequestStatusEnum.Pending,
                CustomerId = SeedDataIds.Customer2Id,
                WorkId = SeedDataIds.CarpetCleaningWorkId,
                AcceptedBidId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddHours(-12)
            },

            // Request 8 - Pending
            new Request
            {
                Id = SeedDataIds.Request8Id,
                Title = "تعمیر توالت فرنگی",
                Description = "توالت فرنگی به درستی آب نمی‌کشد",
                ProposedPrice = 700000,
                Address = "شیراز، خیابان مطهری، پلاک 85، طبقه اول",
                PreferredVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(2),
                Status = RequestStatusEnum.Pending,
                CustomerId = SeedDataIds.Customer3Id,
                WorkId = SeedDataIds.ToiletRepairWorkId,
                AcceptedBidId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddHours(-8)
            },

            // Request 9 - Cancelled
            new Request
            {
                Id = SeedDataIds.Request9Id,
                Title = "نقاشی نمای ساختمان",
                Description = "رنگ‌آمیزی نمای یک ساختمان سه طبقه",
                ProposedPrice = 8000000,
                Address = "کرج، خیابان فردوسی، پلاک 320",
                PreferredVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(-5),
                Status = RequestStatusEnum.Cancelled,
                CustomerId = SeedDataIds.Customer4Id,
                WorkId = SeedDataIds.ExteriorPaintingWorkId,
                AcceptedBidId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-10)
            },

            // Request 10 - Completed
            new Request
            {
                Id = SeedDataIds.Request10Id,
                Title = "تعمیر ماشین لباسشویی",
                Description = "ماشین لباسشویی در حین کار خاموش می‌شود",
                ProposedPrice = 1200000,
                Address = "تهران، خیابان آزادی، پلاک 450، واحد 15",
                PreferredVisitDateTime = SeedDataIds.SeedDataBaseDate.AddDays(-8),
                Status = RequestStatusEnum.Completed,
                CustomerId = SeedDataIds.Customer5Id,
                WorkId = SeedDataIds.WashingMachineRepairWorkId,
                AcceptedBidId = null,
                IsDeleted = false,
                CreatedAt = SeedDataIds.SeedDataBaseDate.AddDays(-14)
            }
        );
    }
}
