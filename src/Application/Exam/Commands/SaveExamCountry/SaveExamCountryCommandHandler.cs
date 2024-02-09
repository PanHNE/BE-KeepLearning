using Application.Exam.Commands.SaveExamCountry;
using Domain.Events;
using Domain.Interfaces;
using Domain.Models.Enums;

namespace Application.Exam.Queries.GenerateExamCountry;

public class SaveExamCountryCommandHandler : IRequestHandler<SaveExamCountryCommand, Guid>
{
    private readonly IContinentRepository _continentRepository;
    private readonly IExamRepository _examRepository;
    private readonly ICountryService _countryService;
    private readonly IMapper _mapper;

    public SaveExamCountryCommandHandler(
        IContinentRepository continentRepository,
        IExamRepository examRepository,
        ICountryService countryService,
        IMapper mapper)
    {
        _continentRepository = continentRepository;
        _examRepository = examRepository;
        _countryService = countryService;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(SaveExamCountryCommand request, CancellationToken cancellationToken)
    {
        var contients = await _continentRepository.GetByNames(request.Continents.Select(c => c.Name));
        var numberOfContinents = request.Continents.Count();

        if (contients.Count() == 0)
        {
            throw new Common.Exceptions.NotFoundException("Not found continent");
        }

        if (contients.Count() != numberOfContinents)
        {
            throw new Common.Exceptions.NotFoundException("Not found all continents from request");
        }

        var questionTexts = request.Questions.Select(q => q.QuestionText).ToList();
        var categoryExam = GuessType.ToCategory(request.Category);


        var countries = await _countryService.GetgCountriesByQuestionsAndCategory(questionTexts, categoryExam);
        if (countries.Count() != request.Questions.Count())
        {
            throw new Common.Exceptions.NotFoundException("Not found country");
        }

        if (!IsCantriesAreFromContientns(countries, contients))
        {
            throw new InvalidDataException("Countries from wrong continents");
        }

        var entity = _mapper.Map<Domain.Entities.Exam>(request);

        entity.AddDomainEvent(new SaveExamCountryEvent(entity));

        await _examRepository.Save(entity, cancellationToken);

        return entity.Id;
    }

    private bool IsCantriesAreFromContientns(IEnumerable<Domain.Entities.Country> countries, IEnumerable<Domain.Entities.Continent> continents)
    {
        return countries.Select(c => c.ContinentId).Distinct().Count() == continents.Count();
    }
}
