using Domain.Entities;
using Domain.Interfaces;
using Domain.Models.Enums;
using static Domain.Models.Enums.GuessType;

namespace Infrastructure.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<Country?> GetRandom(Guid continentId)
    {
        var countries = await RandomCountries(new List<Guid>() { continentId }, 1);

        return countries.FirstOrDefault();
    }

    public async Task<IEnumerable<Country>> RandomCountries(IEnumerable<Guid> continentIds, int numberOfCountries)
    {
        var countries = await _countryRepository.GetByContinentIds(continentIds);
        if (countries is null)
        {
            throw new NotFoundException(string.Join(",", continentIds), "Countries");
        }

        if (countries.Count() < numberOfCountries)
        {
            throw new InvalidDataException("Number of countries to choose is bigger than number of countries in continents that user chosen");
        }

        var random = new Random();

        var randomCountries = countries.OrderBy(country => random.Next()).Take(numberOfCountries);

        return randomCountries;
    }

    public async Task<IEnumerable<Domain.Entities.Country>> GetgCountriesByQuestionsAndCategory(List<string> questionText, GuessType.Category category)
    {
        if (category == GuessType.Category.CapitalCity)
        {
            return await _countryRepository.GetByNames(questionText);
        }
        else
        {
            return await _countryRepository.GetByCapitalCities(questionText);
        }
    }

    public async Task<Country?> GetCountry(string questionText, GuessType.Category category)
    {
        switch (category)
        {
            case Category.CapitalCity:
                return await _countryRepository.GetByName(questionText);

            case Category.Country:
                return await _countryRepository.GetByCapitalCity(questionText);

            default:
                throw new NotImplementedException();
        }
    }
}
