using Microsoft.AspNetCore.Mvc;
using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.DTOs.Works;
using Yarito.Domain.Core.Entities._Common;

namespace Yarito.Endpoint.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController(
        ICategoryAppServices categoryAppServices) : ControllerBase
    {
        [HttpGet("CategoriesWithWorkLists")]
        public async Task<PagedResult<CategoryFullDto>> GetCategoriesWithWorkLists(int pageSize, int page = 1,  CancellationToken ct = default)
        {
            var result = await categoryAppServices.GetCategoriesListAsync(new CategoryReqDto
            {
                PageSize = pageSize,
                Page = page
            }, ct);

            return result;
        }
    }
}
