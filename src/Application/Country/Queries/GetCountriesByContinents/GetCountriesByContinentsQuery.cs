using Application.Common.Models.Country;
using Application.Helpers;

namespace Application.Country.Queries.GetAllCountriesByContinents;

public class GetCountriesByContinentsQuery : UserParams, IRequest<PagedList<CountryDto>>
{
  public IEnumerable<string> Continents { get; set; } = new List<string>();
}
