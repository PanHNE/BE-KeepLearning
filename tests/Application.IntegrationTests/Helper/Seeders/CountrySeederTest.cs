using Application.Common.Interfaces;
using Application.Common.Models.Continent;
using Application.Common.Models.Country;
using EContinent = Domain.Entities.Continent;
using ECountry = Domain.Entities.Country;

namespace Application.Helper.Seeders.IntegrationTests
{
    internal class CountrySeederTest
    {
        private readonly IKeepLearningDbContext _dbContext;

        public CountrySeederTest(IKeepLearningDbContext dbContext)
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
                        _dbContext.SaveChangesAsync(CancellationToken.None);
                    });
                }
            }
        }

        private ECountry CreateCountry(List<EContinent> continents, CountryDto countryDto)
        {
            var continent = continents.First(c => c.Name == countryDto.ContinentDto.Name);

            return new ECountry()
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
}
