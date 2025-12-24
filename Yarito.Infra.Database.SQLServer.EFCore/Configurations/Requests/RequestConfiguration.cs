using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;
using static Yarito.Infra.Database.SQLServer.EFCore.Configurations.SeedDataGuids;

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

        var now = DateTime.UtcNow;

        // SeedData - درخواست‌های مشتریان با وضعیت‌های مختلف
        builder.HasData(
            // درخواست‌های در انتظار (Pending)
            new Request 
            { 
                Id = Request1Id, 
                CustomerId = Customer1Id, 
                CategoryId = PlumbingId, 
                Title = "تعمیر شیر آب آشپزخانه", 
                Description = "شیر آب آشپزخانه نشتی دارد و نیاز به تعویض یا تعمیر دارد",
                ProposedPrice = 800000,
                Address = "تهران، میدان ولیعصر، خیابان کریمخان، پلاک 45",
                Status = RequestStatusEnum.Pending,
                AcceptedBidId = null,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-5)
            },
            new Request 
            { 
                Id = Request2Id, 
                CustomerId = Customer2Id, 
                CategoryId = CleaningId, 
                Title = "نظافت منزل", 
                Description = "نیاز به نظافت کامل منزل مسکونی 100 متری دارم",
                ProposedPrice = 500000,
                Address = "تهران، میدان آزادی، خیابان انقلاب، پلاک 123",
                Status = RequestStatusEnum.Pending,
                AcceptedBidId = null,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-3)
            },
            new Request 
            { 
                Id = Request3Id, 
                CustomerId = Customer3Id, 
                CategoryId = MechanicId, 
                Title = "تعمیر موتور خودرو", 
                Description = "موتور پژو 206 صدای غیرعادی می‌دهد",
                ProposedPrice = 1000000,
                Address = "اصفهان، میدان نقش جهان، چهارباغ عباسی، پلاک 67",
                Status = RequestStatusEnum.Pending,
                AcceptedBidId = null,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-2)
            },

            // درخواست‌های در حال انجام (InProgress) - با AcceptedBidId
            new Request 
            { 
                Id = Request4Id, 
                CustomerId = Customer1Id, 
                CategoryId = ElectricalId, 
                Title = "نصب لوستر سقفی", 
                Description = "نیاز به نصب لوستر 5 شاخه در سالن پذیرایی دارم",
                ProposedPrice = 600000,
                Address = "تهران، میدان ولیعصر، خیابان کریمخان، پلاک 45",
                Status = RequestStatusEnum.InProgress,
                AcceptedBidId = Bid4Id,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-10)
            },
            new Request 
            { 
                Id = Request5Id, 
                CustomerId = Customer4Id, 
                CategoryId = LanguageId, 
                Title = "آموزش خصوصی زبان انگلیسی", 
                Description = "نیاز به آموزش زبان انگلیسی سطح مقدماتی دارم",
                ProposedPrice = 700000,
                Address = "مشهد، بلوار کوهسنگی، خیابان امام رضا، پلاک 89",
                Status = RequestStatusEnum.InProgress,
                AcceptedBidId = Bid10Id,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-15)
            },

            // درخواست‌های تکمیل شده (Completed) - با AcceptedBidId
            new Request 
            { 
                Id = Request6Id, 
                CustomerId = Customer2Id, 
                CategoryId = PaintingId, 
                Title = "رنگ‌آمیزی اتاق خواب", 
                Description = "نیاز به رنگ‌آمیزی یک اتاق خواب 15 متری دارم",
                ProposedPrice = 900000,
                Address = "تهران، میدان آزادی، خیابان انقلاب، پلاک 123",
                Status = RequestStatusEnum.Completed,
                AcceptedBidId = Bid2Id,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-30)
            },
            new Request 
            { 
                Id = Request7Id, 
                CustomerId = Customer3Id, 
                CategoryId = ApplianceRepairId, 
                Title = "تعمیر یخچال فریزر", 
                Description = "یخچال فریزر سامسونگ یخ نمی‌زند",
                ProposedPrice = 800000,
                Address = "اصفهان، میدان نقش جهان، چهارباغ عباسی، پلاک 67",
                Status = RequestStatusEnum.Completed,
                AcceptedBidId = Bid6Id,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-25)
            },
            new Request 
            { 
                Id = Request8Id, 
                CustomerId = Customer5Id, 
                CategoryId = WomenHairdresserId, 
                Title = "خدمات آرایشگری در منزل", 
                Description = "نیاز به خدمات کوتاهی و رنگ مو دارم",
                ProposedPrice = 600000,
                Address = "شیراز، میدان شهدا، خیابان زند، پلاک 34",
                Status = RequestStatusEnum.Completed,
                AcceptedBidId = Bid14Id,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-22)
            },
            new Request 
            { 
                Id = Request9Id, 
                CustomerId = Customer4Id, 
                CategoryId = PlumbingId, 
                Title = "تعویض لوله‌های حمام", 
                Description = "لوله‌های حمام فرسوده شده و نیاز به تعویض دارند",
                ProposedPrice = 1200000,
                Address = "مشهد، بلوار کوهسنگی، خیابان امام رضا، پلاک 89",
                Status = RequestStatusEnum.Completed,
                AcceptedBidId = Bid17Id,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-20)
            },

            // درخواست‌های لغو شده (Cancelled)
            new Request 
            { 
                Id = Request10Id, 
                CustomerId = Customer1Id, 
                CategoryId = MechanicId, 
                Title = "صافکاری خودرو", 
                Description = "نیاز به صافکاری درب جلو خودرو دارم",
                ProposedPrice = 1500000,
                Address = "تهران، میدان ولیعصر، خیابان کریمخان، پلاک 45",
                Status = RequestStatusEnum.Cancelled,
                AcceptedBidId = null,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-18)
            },
            new Request 
            { 
                Id = Request11Id, 
                CustomerId = Customer5Id, 
                CategoryId = ComputerId, 
                Title = "آموزش برنامه‌نویسی پایتون", 
                Description = "نیاز به آموزش پایتون از صفر تا صد دارم",
                ProposedPrice = 900000,
                Address = "شیراز، میدان شهدا، خیابان زند، پلاک 34",
                Status = RequestStatusEnum.Cancelled,
                AcceptedBidId = null,
                IsDeleted = false, 
                CreatedAt = now.AddDays(-14)
            }
        );
    }
}
