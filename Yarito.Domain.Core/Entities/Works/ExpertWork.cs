using Yarito.Domain.Core.Entities.Users;

namespace Yarito.Domain.Core.Entities.Works;

/// <summary>
/// جدول واسط برای رابطه چند-به-چند بین متخصص و خدمات.
/// </summary>
public class ExpertWork
{
    // Foreign Keys
    public int ExpertId { get; set; }
    public int WorkId { get; set; }

    // Navigation Properties
    public Expert Expert { get; set; } = null!;
    public Work Work { get; set; } = null!;
}
