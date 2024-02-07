using Application.Common.Models.Continent;
using Application.Common.Models.Country;
using Application.Common.Models.Exam;
using Application.Common.Models.Question;
using Application.Common.Models.Exam.Country;
using Application.Common.Interfaces;
using Infrastructure.Services;
using Domain.Models.Enums;

namespace Application.Exam.Queries.GenerateExamCountry;

public class GenerateExamCountryQueryHandler : IRequestHandler<GenerateExamCountryQuery, ExamCountryDto>
{
    private readonly IKeepLearningDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICountryService _countryService;

    public GenerateExamCountryQueryHandler(IKeepLearningDbContext dbContext, IMapper mapper, ICountryService countryService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _countryService = countryService;
    }

    // TODO: Add more specific validation to GenerateExamCountryQuery
    public async Task<ExamCountryDto> Handle(GenerateExamCountryQuery request, CancellationToken cancellationToken)
    {
        var continents = await _dbContext.Continents.Where(c => request.Continents.Contains(c.Name)).ToListAsync();
        if (!continents.Any())
        {
            throw new NotFoundException(string.Join(",", request.Continents), "Not found any continents");
        }

        var continentIds = continents.Select(c => c.Id);
        var randomCountries = await _countryService.RandomCountries(continentIds, request.NumberOfQuestion);
        if (!randomCountries.Any())
        {
            throw new NotFoundException(string.Join(",", continentIds), "Not found any country for these continents");
        }

        var countriesDto = randomCountries.Select(c => _mapper.Map<CountryDto>(c)).ToList();
        var continentsDto = request.Continents.Select(c => _mapper.Map<ContinentDto>(c)).ToList();

        var category = GuessType.ToCategory(request.Category);

        var questionsDto = QuestionDtoBuilder.CreateQuestions(countriesDto, request);
        var examDto = ExamDtoBuilder.CreateExamCountry(request.Name, questionsDto, category, continentsDto);

        return examDto;
    }
}
