using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ContinentRepository : IContinentRepository
{
  private readonly IKeepLearningDbContext _dbContext;

  public ContinentRepository(IKeepLearningDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<Continent>> GetByNames(IEnumerable<string> continentNames)
  {
    var result = await _dbContext.Continents.Where(c => continentNames.Contains(c.Name)).ToListAsync();
    return result;
  }

  public async Task<Continent?> GetById(Guid continentId)
  {
    return await _dbContext.Continents.FirstOrDefaultAsync(c => c.Id == continentId);
  }

  public async Task<Continent?> GetByName(string continentName)
  {
    return await _dbContext.Continents.FirstOrDefaultAsync(c => c.Name == continentName);
  }
}
