using Application.Common.Interfaces;
using Application.Common.Models.Continent;

namespace Application.Continent.Queries.GetAllContinents;

public class GetAllContinentsQueryHandler : IRequestHandler<GetContinentsQuery, IEnumerable<ContinentDto>>
{
    private readonly IKeepLearningDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllContinentsQueryHandler(IKeepLearningDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContinentDto>> Handle(GetContinentsQuery request, CancellationToken cancellationToken)
    {
        var continents = await _dbContext.Continents.ToListAsync();

        var continentsDto = continents.Select(c => _mapper.Map<ContinentDto>(c));

        return continentsDto;
    }
}
