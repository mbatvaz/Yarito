using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yarito.Domain.Core.Entities.Works;

namespace Yarito.Infra.Database.SQLServer.EFCore.Configurations.Works;

public class ExpertWorkConfiguration : IEntityTypeConfiguration<ExpertWork>
{
    public void Configure(EntityTypeBuilder<ExpertWork> builder)
    {
        // Primary Key
        builder.HasKey(ew => new { ew.ExpertId, ew.WorkId });

        // Relationships
        builder.HasOne(ew => ew.Expert)
            .WithMany(e => e.ExpertWorks)
            .HasForeignKey(ew => ew.ExpertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ew => ew.Work)
            .WithMany(w => w.ExpertWorks)
            .HasForeignKey(ew => ew.WorkId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.Expert.IsDeleted);

        // Seed Data
        builder.HasData(
            // Expert1 (رضا برقکار) - متخصص برق
            new ExpertWork { ExpertId = SeedDataIds.Expert1Id, WorkId = SeedDataIds.HomeWiringWorkId },
            new ExpertWork { ExpertId = SeedDataIds.Expert1Id, WorkId = SeedDataIds.LightInstallationWorkId },
            new ExpertWork { ExpertId = SeedDataIds.Expert1Id, WorkId = SeedDataIds.SwitchRepairWorkId },

            // Expert2 (مهدی لوله‌کش) - متخصص لوله‌کشی
            new ExpertWork { ExpertId = SeedDataIds.Expert2Id, WorkId = SeedDataIds.PipeRepairWorkId },
            new ExpertWork { ExpertId = SeedDataIds.Expert2Id, WorkId = SeedDataIds.FaucetInstallationWorkId },
            new ExpertWork { ExpertId = SeedDataIds.Expert2Id, WorkId = SeedDataIds.ToiletRepairWorkId },

            // Expert3 (سارا نظافتچی) - متخصص نظافت
            new ExpertWork { ExpertId = SeedDataIds.Expert3Id, WorkId = SeedDataIds.HomeCleaningWorkId },
            new ExpertWork { ExpertId = SeedDataIds.Expert3Id, WorkId = SeedDataIds.WindowCleaningWorkId },
            new ExpertWork { ExpertId = SeedDataIds.Expert3Id, WorkId = SeedDataIds.CarpetCleaningWorkId },

            // Expert4 (احمد نقاش) - متخصص نقاشی
            new ExpertWork { ExpertId = SeedDataIds.Expert4Id, WorkId = SeedDataIds.InteriorPaintingWorkId },
            new ExpertWork { ExpertId = SeedDataIds.Expert4Id, WorkId = SeedDataIds.ExteriorPaintingWorkId },
            new ExpertWork { ExpertId = SeedDataIds.Expert4Id, WorkId = SeedDataIds.WallPaperingWorkId },

            // Expert5 (نرگس تعمیرکار) - متخصص تعمیر لوازم خانگی + برخی برق
            new ExpertWork { ExpertId = SeedDataIds.Expert5Id, WorkId = SeedDataIds.WashingMachineRepairWorkId },
            new ExpertWork { ExpertId = SeedDataIds.Expert5Id, WorkId = SeedDataIds.RefrigeratorRepairWorkId },
            new ExpertWork { ExpertId = SeedDataIds.Expert5Id, WorkId = SeedDataIds.ACRepairWorkId },
            new ExpertWork { ExpertId = SeedDataIds.Expert5Id, WorkId = SeedDataIds.LightInstallationWorkId }
        );
    }
}
