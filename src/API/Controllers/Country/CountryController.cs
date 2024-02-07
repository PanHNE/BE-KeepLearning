using API.Extensions;
using API.Helpers;
using Application.Common.Models.Country;
using Application.Country.Queries.GetAllCountriesByContinents;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{

    private readonly IMediator _mediator;
    private readonly ILogger<CountryController> _logger;

    public CountryController(IMediator mediator, ILogger<CountryController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<CountryDto>>> GetCountriesByContinents([FromQuery] GetCountriesByContinentsQuery query)
    {
        var countries = await _mediator.Send(query);

        Response.AddPaginationHeader(new PaginationHeader(countries.CurrentPage, countries.PageSize, countries.TotalCount, countries.TotalPage));

        return Ok(countries);
    }
}
