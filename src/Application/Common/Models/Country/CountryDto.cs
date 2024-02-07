using Application.Common.Models.Continent;

namespace Application.Common.Models.Country;

public class CountryDto
{
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
    public required string CapitalCity { get; set; }
    public required ContinentDto ContinentDto { get; set; }
}
