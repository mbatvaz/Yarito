using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Images;
using static Yarito.Infra.Database.SQLServer.EFCore.Configurations.SeedDataGuids;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Images;
public class BidImageConfiguration : IEntityTypeConfiguration<BidImage>
{
    public void Configure(EntityTypeBuilder<BidImage> builder)
    {
        builder.HasKey(x => new { x.BidId, x.ExpertImageId });

        builder.HasOne(x => x.Bid)
            .WithMany(b => b.BidImage)
            .HasForeignKey(x => x.BidId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ExpertImage)
            .WithMany() 
            .HasForeignKey(x => x.ExpertImageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ExpertImageId);

        // SeedData - ارتباط تصاویر متخصصان با پیشنهادات
        builder.HasData(
            // پیشنهادات رضا نوری (لوله‌کش و برق‌کار)
            new BidImage { BidId = Bid1Id, ExpertImageId = Expert1Image1Id },
            new BidImage { BidId = Bid1Id, ExpertImageId = Expert1Image2Id },
            new BidImage { BidId = Bid3Id, ExpertImageId = Expert1Image1Id },
            new BidImage { BidId = Bid3Id, ExpertImageId = Expert1Image3Id },
            new BidImage { BidId = Bid16Id, ExpertImageId = Expert1Image2Id },

            // پیشنهادات محمد صادقی (نقاش و کاشی‌کار)
            new BidImage { BidId = Bid2Id, ExpertImageId = Expert2Image1Id },
            new BidImage { BidId = Bid2Id, ExpertImageId = Expert2Image2Id },
            new BidImage { BidId = Bid5Id, ExpertImageId = Expert2Image3Id },

            // پیشنهادات فاطمه موسوی (نظافت و تعمیرکار)
            new BidImage { BidId = Bid6Id, ExpertImageId = Expert3Image1Id },
            new BidImage { BidId = Bid7Id, ExpertImageId = Expert3Image2Id },
            new BidImage { BidId = Bid19Id, ExpertImageId = Expert3Image1Id },

            // پیشنهادات امیر قاسمی (مکانیک و صافکار)
            new BidImage { BidId = Bid8Id, ExpertImageId = Expert4Image1Id },
            new BidImage { BidId = Bid8Id, ExpertImageId = Expert4Image2Id },
            new BidImage { BidId = Bid20Id, ExpertImageId = Expert4Image3Id },
            new BidImage { BidId = Bid21Id, ExpertImageId = Expert4Image1Id },

            // پیشنهادات نرگس کاظمی (آموزش)
            new BidImage { BidId = Bid9Id, ExpertImageId = Expert5Image1Id },
            new BidImage { BidId = Bid10Id, ExpertImageId = Expert5Image2Id },

            // پیشنهادات احمد رحیمی (برق‌کار و تعمیرکار)
            new BidImage { BidId = Bid4Id, ExpertImageId = Expert6Image1Id },
            new BidImage { BidId = Bid4Id, ExpertImageId = Expert6Image2Id },

            // پیشنهادات مریم یزدانی (آرایشگر)
            new BidImage { BidId = Bid13Id, ExpertImageId = Expert7Image1Id },
            new BidImage { BidId = Bid14Id, ExpertImageId = Expert7Image2Id },

            // پیشنهادات سعید حیدری (مدرس کامپیوتر و لوله‌کش)
            new BidImage { BidId = Bid11Id, ExpertImageId = Expert8Image1Id },
            new BidImage { BidId = Bid12Id, ExpertImageId = Expert8Image2Id },
            new BidImage { BidId = Bid17Id, ExpertImageId = Expert8Image1Id },
            new BidImage { BidId = Bid18Id, ExpertImageId = Expert8Image2Id }
        );
    }
}
