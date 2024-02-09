using Domain.Entities;

namespace Domain.Interfaces;

public interface IContinentRepository
{
  Task<Continent?> GetById(Guid continentId);
  Task<Continent?> GetByName(string continentName);
  Task<IEnumerable<Continent>> GetByNames(IEnumerable<string> continentNames);
}