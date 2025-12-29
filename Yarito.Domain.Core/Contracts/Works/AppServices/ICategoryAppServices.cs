using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Domain.Core.Contracts.Works.AppServices
{
    public interface ICategoryAppServices
    {
        Task<IReadOnlyList<CategoryStringDataDto>>
            GetAllCategoriesNamesAsync(CancellationToken ct);
    }
}
