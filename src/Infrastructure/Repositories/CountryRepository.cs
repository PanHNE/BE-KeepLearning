using Application.Common.Interfaces;
using Domain.Enteties;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CountryRepository : ICountryRepository
{
  private readonly IKeepLearningDbContext _dbContext;

  public CountryRepository(IKeepLearningDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<Country?> GetById(Guid id)
    => await _dbContext.Countries.FirstOrDefaultAsync(c => c.Id == id);

  public async Task<Country?> GetByName(string name)
    => await _dbContext.Countries.FirstOrDefaultAsync(c => c.Name == name);

  public async Task<IEnumerable<Country>> GetByNames(List<string> names)
    => await _dbContext.Countries.Where(c => names.Contains(c.Name)).ToListAsync();

  public async Task<IEnumerable<Country>> GetByCapitalCities(List<string> capitalCities)
    => await _dbContext.Countries.Where(c => capitalCities.Contains(c.CapitalCity)).ToListAsync();

  public async Task<Country?> GetByCapitalCity(string capitalCity)
    => await _dbContext.Countries.FirstOrDefaultAsync(c => c.CapitalCity == capitalCity);

  public async Task<IEnumerable<Country>> GetByContinentIds(IEnumerable<Guid> contientnIds)
    => await _dbContext.Countries.Where(c => contientnIds.Contains(c.ContinentId)).ToListAsync();
}
