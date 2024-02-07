using Application.Continent.Queries.GetAllContinents;
using Application.Country.Queries.GetNumberOfCountries;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContinentController : ControllerBase
{

    private readonly IMediator _mediator;
    private readonly ILogger<ContinentController> _logger;

    public ContinentController(IMediator mediator, ILogger<ContinentController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetContinents()
    {
        var continents = await _mediator.Send(new GetContinentsQuery());

        return Ok(continents);
    }

    [HttpGet("numberOfCountries")]
    public async Task<IActionResult> GetNumberOfCountries([FromQuery] GetNumberOfCountriesQuery query)
    {
        var numberOfCountries = await _mediator.Send(query);

        return Ok(numberOfCountries);
    }
}
