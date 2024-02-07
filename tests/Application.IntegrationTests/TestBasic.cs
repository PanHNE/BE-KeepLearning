using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Helper.Seeders.IntegrationTests;
using Application.UnitTests.Helper;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

public class TestBasics
{

  protected readonly IKeepLearningDbContext _dbContext;
  protected readonly IAnswerService _answerService;
  protected readonly IContinentRepository _continentRepository;
  protected readonly ICountryRepository _countryRepository;
  protected readonly ICountryService _countryService;
  protected readonly IExamRepository _examRepository;
  protected readonly IMapper _mapper;

  public TestBasics()
  {
    var builder = new DbContextOptionsBuilder<KeepLearningDbContextTest>();
    builder.UseInMemoryDatabase("TestKeepLearningDb-" + this.GetType());

    _dbContext = new KeepLearningDbContextTest(builder.Options);

    var continentSeederTest = new ContinentSeederTest(_dbContext);
    continentSeederTest.Seed();

    var countrySeederTest = new CountrySeederTest(_dbContext);
    countrySeederTest.Seed();

    var mappingProfiles = new List<Profile>() {
            new ContinentMappingProfile(),
            new CountryMappingProfile(),
            new QuestionMappingProfile(),
            new ExamMappingProfile(),
        };

    var configuration = new MapperConfiguration(cfg =>
        cfg.AddProfiles(mappingProfiles)
    );

    _continentRepository = new ContinentRepository(_dbContext);
    _countryRepository = new CountryRepository(_dbContext);
    _countryService = new CountryService(_countryRepository);
    _answerService = new AnswerService(_countryService);
    _examRepository = new ExamRepository(_dbContext);

    _mapper = configuration.CreateMapper();
  }
}
