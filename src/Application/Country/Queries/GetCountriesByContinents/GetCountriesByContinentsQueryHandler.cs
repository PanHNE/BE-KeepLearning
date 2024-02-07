using Application.Common.Interfaces;
using Application.Common.Models.Country;

namespace Application.Country.Queries.GetAllCountriesByContinents;

public class GetCountriesByContinentsQueryHandler : IRequestHandler<GetCountriesByContinentsQuery, PagedList<CountryDto>>
{
    private readonly IKeepLearningDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCountriesByContinentsQueryHandler(IKeepLearningDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // TODO: Add more specific validation to GetCountriesByContinentsQuery
    public async Task<PagedList<CountryDto>> Handle(GetCountriesByContinentsQuery request, CancellationToken cancellationToken)
    {

        var continents = await _dbContext.Continents
            .Where(continent => request.Continents.Contains(continent.Name))
            .ToListAsync();

        IQueryable<CountryDto> queryCountryDto = GetQueryableCountries(continents);

        var result = PagedList<CountryDto>.CreateAsync(queryCountryDto, request.PageNumber, request.PageSize);

        return await result;
    }

    private IQueryable<CountryDto> GetQueryableCountries(List<Domain.Enteties.Continent> continents)
    {

        if (continents.Count() == 0)
        {
            return _dbContext.Countries
                .Include(continent => continent.Continent)
                .Select(c => _mapper.Map<CountryDto>(c));
        }
        else
        {
            var continentIds = continents.Select(c => c.Id);

            return _dbContext.Countries
                .Include(continent => continent.Continent)
                .Where(country => continentIds.Contains(country.ContinentId))
                .Select(c => _mapper.Map<CountryDto>(c));
        }
    }
}
