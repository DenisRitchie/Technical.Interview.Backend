namespace Technical.Interview.Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

using Technical.Interview.Backend.Common;
using Technical.Interview.Backend.Responses;
using Technical.Interview.Backend.Services;

[ApiController]
public class MarcasAutosController(IBrandService BrandService) : InterviewControllerBase
{
    [HttpGet(Name = "FetchBrands")]
    [OutputCache(Duration = 30)]
    [ProducesResponseType(typeof(PagedResponse<BrandInfo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResponse<BrandInfo>>> FetchCustomersAsync([FromQuery] BrandFilter Filter)
    {
        return await BrandService.FetchBrandsAsync(Filter);
    }
}
