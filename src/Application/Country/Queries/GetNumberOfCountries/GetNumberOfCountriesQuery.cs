namespace Application.Country.Queries.GetNumberOfCountries;

public class GetNumberOfCountriesQuery : IRequest<int>
{
  public IEnumerable<string> Continents { get; set; } = new List<string>();
}
