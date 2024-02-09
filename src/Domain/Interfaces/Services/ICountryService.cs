using Domain.Entities;
using Domain.Models.Enums;

public interface ICountryService
{
  Task<Country?> GetRandom(Guid continentId);
  Task<IEnumerable<Country>> RandomCountries(IEnumerable<Guid> continentIds, int numberOfCountries);
  Task<IEnumerable<Country>> GetgCountriesByQuestionsAndCategory(List<string> questionText, GuessType.Category category);
  Task<Country?> GetCountry(string questionText, GuessType.Category category);
}