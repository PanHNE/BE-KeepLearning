using Application.Common.Models.Continent;
using Application.Common.Models.Country;
using Application.UnitTests.Helper;
using Domain.Enteties;

namespace Infrastructure.Helper.Seeders.UnitTests;

internal class CountrySeederTest
{
    private readonly KeepLearningDbContextTest _dbContext;

    public CountrySeederTest(KeepLearningDbContextTest dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (!_dbContext.Countries.Any())
        {
            var continents = _dbContext.Continents.ToList();

            var countries = GetCountriesFromFile();

            if (countries != null)
            {
                countries.ToList().ForEach(countryDto =>
                {
                    var newCountry = CreateCountry(continents, countryDto);

                    _dbContext.Countries.Add(newCountry);
                    _dbContext.SaveChanges();
                });
            }
        }
    }

    private Country CreateCountry(List<Continent> continents, CountryDto countryDto)
    {
        var continent = continents.First(c => c.Name == countryDto.ContinentDto.Name);

        return new Country()
        {
            Name = countryDto.Name,
            Abbreviation = countryDto.Abbreviation,
            CapitalCity = countryDto.CapitalCity,
            ContinentId = continent.Id
        };
    }

    private IEnumerable<CountryDto> GetCountriesFromFile()
    {
        IEnumerable<CountryDto> countries = new List<CountryDto>();

        try
        {
            countries = File.ReadAllLines("../../../Helper/Seeders/FilesWithData/WorldCountriesList.csv")
                .Skip(1)
                .Select(c => c.Split(','))
                .Select(c => new CountryDto()
                {
                    Name = c[0],
                    Abbreviation = c[1],
                    CapitalCity = c[2],
                    ContinentDto = new ContinentDto(c[3])
                });

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return countries;
    }
}
