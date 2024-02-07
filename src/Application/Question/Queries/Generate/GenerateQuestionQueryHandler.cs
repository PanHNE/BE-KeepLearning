using Application.Common.Interfaces;
using Application.Common.Models.Country;
using Application.Common.Models.Question;
using Domain.Models.Enums;

namespace Application.Question.Queries.GenerateQuestion;

public class GenerateQuestionQueryHandler : IRequestHandler<GenerateQuestionQuery, QuestionDto>
{
    private readonly IKeepLearningDbContext _dbContext;
    private readonly ICountryService _countryService;
    private readonly IMapper _mapper;

    public GenerateQuestionQueryHandler(IKeepLearningDbContext dbContext, ICountryService countryService, IMapper mapper)
    {
        _dbContext = dbContext;
        _countryService = countryService;
        _mapper = mapper;
    }

    // TODO: Add validation to GenerateQuestionQuery
    public async Task<QuestionDto> Handle(GenerateQuestionQuery request, CancellationToken cancellationToken)
    {
        var continent = await _dbContext.Continents.Where(c => c.Name == request.Continent).FirstAsync();
        if (continent is null)
        {
            throw new NotFoundException(request.Continent, "Not found continent!");
        }

        var randomCountry = await _countryService.GetRandom(continent.Id);
        if (randomCountry is null)
        {
            throw new NotFoundException(continent.Id.ToString(), "Not found country");
        }

        var countryDto = _mapper.Map<CountryDto>(randomCountry);
        var category = GuessType.ToCategory(request.Category);

        return QuestionDtoBuilder.CreateQuestionByCategory(countryDto, category);
    }
}
