using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities.Works;

namespace Yarito.Domain.Core.Contracts.Works.Repository
{
    public interface ICategoryRepo
    {
        Task<bool> AddAsync(Category newCategory, CancellationToken ct);
        Task<bool> DeleteAsync(int categoryId, CancellationToken ct);
        Task<IReadOnlyList<CategoryStringDataDto>> GetAllCategoriesNamesAsync(CancellationToken ct);
    }
}
