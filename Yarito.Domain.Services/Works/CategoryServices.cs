using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Domain.Services.Works
{
    public class CategoryServices(
        ICategoryRepo categoryRepo) : ICategoryServices
    {
        public async Task<IReadOnlyList<CategoryStringDataDto>>
            GetAllCategoriesNamesAsync(CancellationToken ct)
            => await categoryRepo.GetAllCategoriesNamesAsync(ct);
    }
}
