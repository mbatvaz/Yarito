using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Core.DTOs.Works;

namespace Yarito.Domain.AppServices.Works
{
    public class CategoryAppServices(
        ICategoryServices categoryServices) : ICategoryAppServices
    {
        public async Task<IReadOnlyList<CategoryStringDataDto>>
            GetAllCategoriesNamesAsync(CancellationToken ct)
            => await categoryServices.GetAllCategoriesNamesAsync(ct);
        
    }
}
