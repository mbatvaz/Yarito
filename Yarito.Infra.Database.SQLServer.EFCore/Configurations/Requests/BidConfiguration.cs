using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Requests;
using Yarito.Domain.Core.Enums.Requests;
using static Yarito.Infra.Database.SQLServer.EFCore.Configurations.SeedDataGuids;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Requests;

public class BidConfiguration : IEntityTypeConfiguration<Bid>
{
    public void Configure(EntityTypeBuilder<Bid> builder)
    {
        builder.HasKey(b => b.Id);
        
        builder.HasIndex(b => new { b.RequestId, b.ExpertId })
        .IsUnique();
        
        builder.HasIndex(b => b.ExpertId);
        
        builder.HasIndex(b => b.Status);

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
            .IsRequired(false);

        builder.HasOne(b => b.Request)
            .WithMany(r => r.Bids)
            .HasForeignKey(b => b.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Expert)
            .WithMany(e => e.Bids)
            .HasForeignKey(b => b.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.IsDeleted);

        var now = DateTime.UtcNow;

        // SeedData - پیشنهادات متخصصان برای درخواست‌ها
        builder.HasData(
            // پیشنهادات برای درخواست 1 (در انتظار) - تعمیر شیر آب
            new Bid { Id = Bid1Id, RequestId = Request1Id, ExpertId = Expert1Id, Description = "تعمیر شیر آب با قطعات درجه یک", ProposedPrice = 750000, ProposedVisitDateTime = now.AddDays(-4), Status = BidStatusEnum.Pending, IsDeleted = false, CreatedAt = now.AddDays(-4) },
            new Bid { Id = Bid18Id, RequestId = Request1Id, ExpertId = Expert8Id, Description = "تعویض کامل شیر با گارانتی یک ساله", ProposedPrice = 850000, ProposedVisitDateTime = now.AddDays(-3), Status = BidStatusEnum.Pending, IsDeleted = false, CreatedAt = now.AddDays(-3) },

            // پیشنهادات برای درخواست 2 (در انتظار) - نظافت منزل
            new Bid { Id = Bid19Id, RequestId = Request2Id, ExpertId = Expert3Id, Description = "نظافت کامل با مواد شوینده تخصصی", ProposedPrice = 480000, ProposedVisitDateTime = now.AddDays(-2), Status = BidStatusEnum.Pending, IsDeleted = false, CreatedAt = now.AddDays(-2) },

            // پیشنهادات برای درخواست 3 (در انتظار) - تعمیر موتور
            new Bid { Id = Bid20Id, RequestId = Request3Id, ExpertId = Expert4Id, Description = "عیب‌یابی و تعمیر موتور با تضمین", ProposedPrice = 950000, ProposedVisitDateTime = now.AddDays(-1), Status = BidStatusEnum.Pending, IsDeleted = false, CreatedAt = now.AddDays(-1) },
            new Bid { Id = Bid21Id, RequestId = Request3Id, ExpertId = Expert4Id, Description = "تعمیر سریع در محل با قطعات اصلی", ProposedPrice = 1050000, ProposedVisitDateTime = now, Status = BidStatusEnum.Pending, IsDeleted = false, CreatedAt = now },

            // پیشنهادات برای درخواست 4 (در حال انجام) - نصب لوستر
            new Bid { Id = Bid3Id, RequestId = Request4Id, ExpertId = Expert1Id, Description = "نصب لوستر با سیم‌کشی استاندارد", ProposedPrice = 550000, ProposedVisitDateTime = now.AddDays(-9), Status = BidStatusEnum.Rejected, IsDeleted = false, CreatedAt = now.AddDays(-9) },
            new Bid { Id = Bid4Id, RequestId = Request4Id, ExpertId = Expert6Id, Description = "نصب حرفه‌ای با گارانتی کار", ProposedPrice = 580000, ProposedVisitDateTime = now.AddDays(-8), Status = BidStatusEnum.Accepted, IsDeleted = false, CreatedAt = now.AddDays(-8) },

            // پیشنهادات برای درخواست 5 (در حال انجام) - آموزش زبان
            new Bid { Id = Bid9Id, RequestId = Request5Id, ExpertId = Expert5Id, Description = "آموزش تضمینی با مدرک بین‌المللی", ProposedPrice = 650000, ProposedVisitDateTime = now.AddDays(-14), Status = BidStatusEnum.Rejected, IsDeleted = false, CreatedAt = now.AddDays(-14) },
            new Bid { Id = Bid10Id, RequestId = Request5Id, ExpertId = Expert5Id, Description = "10 جلسه آموزش مکالمه و گرامر", ProposedPrice = 700000, ProposedVisitDateTime = now.AddDays(-13), Status = BidStatusEnum.Accepted, IsDeleted = false, CreatedAt = now.AddDays(-13) },

            // پیشنهادات برای درخواست 6 (تکمیل شده) - رنگ‌آمیزی
            new Bid { Id = Bid2Id, RequestId = Request6Id, ExpertId = Expert2Id, Description = "رنگ‌آمیزی با رنگ پلاستیک درجه یک", ProposedPrice = 880000, ProposedVisitDateTime = now.AddDays(-29), Status = BidStatusEnum.Accepted, IsDeleted = false, CreatedAt = now.AddDays(-29) },
            new Bid { Id = Bid5Id, RequestId = Request6Id, ExpertId = Expert2Id, Description = "رنگ‌آمیزی با رنگ پلاستیک معمولی", ProposedPrice = 820000, ProposedVisitDateTime = now.AddDays(-28), Status = BidStatusEnum.Rejected, IsDeleted = false, CreatedAt = now.AddDays(-28) },

            // پیشنهادات برای درخواست 7 (تکمیل شده) - تعمیر یخچال
            new Bid { Id = Bid6Id, RequestId = Request7Id, ExpertId = Expert3Id, Description = "تعمیر یخچال با شارژ گاز", ProposedPrice = 750000, ProposedVisitDateTime = now.AddDays(-24), Status = BidStatusEnum.Accepted, IsDeleted = false, CreatedAt = now.AddDays(-24) },
            new Bid { Id = Bid7Id, RequestId = Request7Id, ExpertId = Expert6Id, Description = "تعمیر کامل با تعویض قطعات", ProposedPrice = 850000, ProposedVisitDateTime = now.AddDays(-23), Status = BidStatusEnum.Rejected, IsDeleted = false, CreatedAt = now.AddDays(-23) },

            // پیشنهادات برای درخواست 8 (تکمیل شده) - آرایشگری
            new Bid { Id = Bid13Id, RequestId = Request8Id, ExpertId = Expert7Id, Description = "کوتاهی مدل روز و رنگ مو", ProposedPrice = 550000, ProposedVisitDateTime = now.AddDays(-21), Status = BidStatusEnum.Rejected, IsDeleted = false, CreatedAt = now.AddDays(-21) },
            new Bid { Id = Bid14Id, RequestId = Request8Id, ExpertId = Expert7Id, Description = "خدمات کامل آرایشی در منزل", ProposedPrice = 600000, ProposedVisitDateTime = now.AddDays(-20), Status = BidStatusEnum.Accepted, IsDeleted = false, CreatedAt = now.AddDays(-20) },

            // پیشنهادات برای درخواست 9 (تکمیل شده) - تعویض لوله‌ها
            new Bid { Id = Bid16Id, RequestId = Request9Id, ExpertId = Expert1Id, Description = "تعویض لوله با لوله‌های گالوانیزه", ProposedPrice = 1150000, ProposedVisitDateTime = now.AddDays(-19), Status = BidStatusEnum.Rejected, IsDeleted = false, CreatedAt = now.AddDays(-19) },
            new Bid { Id = Bid17Id, RequestId = Request9Id, ExpertId = Expert8Id, Description = "تعویض با لوله‌های پی‌وی‌سی استاندارد", ProposedPrice = 1200000, ProposedVisitDateTime = now.AddDays(-18), Status = BidStatusEnum.Accepted, IsDeleted = false, CreatedAt = now.AddDays(-18) },

            // پیشنهادات برای درخواست 10 (لغو شده) - صافکاری
            new Bid { Id = Bid8Id, RequestId = Request10Id, ExpertId = Expert4Id, Description = "صافکاری و رنگ کامل درب", ProposedPrice = 1400000, ProposedVisitDateTime = now.AddDays(-17), Status = BidStatusEnum.Rejected, IsDeleted = false, CreatedAt = now.AddDays(-17) },

            // پیشنهادات برای درخواست 11 (لغو شده) - آموزش برنامه‌نویسی
            new Bid { Id = Bid11Id, RequestId = Request11Id, ExpertId = Expert8Id, Description = "آموزش پایتون از پایه تا پیشرفته", ProposedPrice = 850000, ProposedVisitDateTime = now.AddDays(-13), Status = BidStatusEnum.Rejected, IsDeleted = false, CreatedAt = now.AddDays(-13) },
            new Bid { Id = Bid12Id, RequestId = Request11Id, ExpertId = Expert8Id, Description = "15 جلسه آموزش پروژه‌محور", ProposedPrice = 920000, ProposedVisitDateTime = now.AddDays(-12), Status = BidStatusEnum.Rejected, IsDeleted = false, CreatedAt = now.AddDays(-12) }
        );
    }
}
