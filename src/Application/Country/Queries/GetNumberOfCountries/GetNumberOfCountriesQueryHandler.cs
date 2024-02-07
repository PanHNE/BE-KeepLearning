using Application.Common.Interfaces;

namespace Application.Country.Queries.GetNumberOfCountries;

public class GetNumberOfCountriesQueryHandler : IRequestHandler<GetNumberOfCountriesQuery, int>
{
    private readonly IKeepLearningDbContext _dbContext;

    public GetNumberOfCountriesQueryHandler(IKeepLearningDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // TODO: Add more specific validation to GetNumberOfCountriesQuery
    public async Task<int> Handle(GetNumberOfCountriesQuery request, CancellationToken cancellationToken)
    {
        var continentIds = await _dbContext.Continents
            .Where(continent => request.Continents.Contains(continent.Name))
            .Select(c => c.Id)
            .ToListAsync();

        var numberOfCountries = await _dbContext.Countries
            .Where(country => continentIds.Contains(country.ContinentId))
            .CountAsync();

        return numberOfCountries;
    }
}
