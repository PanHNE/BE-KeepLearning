using Domain.Enteties;

namespace Domain.Interfaces;

public interface ICountryRepository
{
  Task<Country?> GetById(Guid id);
  Task<Country?> GetByName(string name);
  Task<Country?> GetByCapitalCity(string capitalCity);
  Task<IEnumerable<Country>> GetByNames(List<string> names);
  Task<IEnumerable<Country>> GetByCapitalCities(List<string> capitalCities);
  Task<IEnumerable<Country>> GetByContinentIds(IEnumerable<Guid> contientnIds);
}