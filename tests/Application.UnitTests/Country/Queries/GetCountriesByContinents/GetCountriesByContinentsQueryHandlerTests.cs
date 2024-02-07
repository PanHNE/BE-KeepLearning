using Application.Common.Mappings;
using Application.Country.Queries.GetAllCountriesByContinents;
using Application.UnitTests.Helper;
using AutoMapper;
using Infrastructure.Helper.Seeders.UnitTests;
using Microsoft.EntityFrameworkCore;

namespace Application.Country.Queries.GetCountriesByContinents.UnitTest;

public class GetCountriesByContinentsQueryHandlerTests
{
    private readonly KeepLearningDbContextTest _dbContext;
    private readonly IMapper _mapper;

    public GetCountriesByContinentsQueryHandlerTests()
    {
        var builder = new DbContextOptionsBuilder<KeepLearningDbContextTest>();
        builder.UseInMemoryDatabase("TestKeepLearningDb-GetCountriesByContinentsQueryHandlerTests");

        _dbContext = new KeepLearningDbContextTest(builder.Options);

        var continentSeederTest = new ContinentSeederTest(_dbContext);
        continentSeederTest.Seed();

        var countrySeederTest = new CountrySeederTest(_dbContext);
        countrySeederTest.Seed();

        var mappingProfiles = new List<Profile>() {
            new CountryMappingProfile(),
            new ContinentMappingProfile()
            };

        var configuration = new MapperConfiguration(cfg =>
               cfg.AddProfiles(mappingProfiles));

        _mapper = configuration.CreateMapper();
    }

    public record QueryWithExpectedResult(GetCountriesByContinentsQuery getCountriesByContinentsQuery, int result) { }

    public static IEnumerable<object[]> GetQueryWithExpectedResult()
    {
        var list = new List<QueryWithExpectedResult>()
        {
            new QueryWithExpectedResult(
                new GetCountriesByContinentsQuery(){
                    Continents = new List<String>() { "Asia" },
                    PageSize = 100,
                    PageNumber = 1
                },
                48
            ),
            new QueryWithExpectedResult(
                new GetCountriesByContinentsQuery(){
                    Continents = new List<String>() { "Asia", "Europe" },
                    PageSize = 100,
                    PageNumber = 1
                },
                92
            ),
            new QueryWithExpectedResult(
                new GetCountriesByContinentsQuery(){
                    Continents = new List<String>() { "Europe" },
                    PageSize = 100,
                    PageNumber = 1
                },
                44
            ),
            new QueryWithExpectedResult(
                new GetCountriesByContinentsQuery(){
                    Continents = new List<String>() { "Europe" },
                    PageSize = 20,
                    PageNumber = 1
                },
                20
            ),
            new QueryWithExpectedResult(
                new GetCountriesByContinentsQuery(){
                    Continents = new List<String>() { "Europe" },
                    PageSize = 40,
                    PageNumber = 1
                },
                40
            ),
            new QueryWithExpectedResult(
                new GetCountriesByContinentsQuery(){
                    Continents = new List<String>() { "Europe" },
                    PageSize = 40,
                    PageNumber = 2
                },
                4
            ),
        };

        return list.Select(el => new object[] { el });
    }

    [Theory()]
    [MemberData(nameof(GetQueryWithExpectedResult))]
    public async void Handle_WithNotEmptyListOfContinents_ReturnCountries(QueryWithExpectedResult queryWithExpectedResult)
    {
        // arrange
        var handler = new GetCountriesByContinentsQueryHandler(_dbContext, _mapper);

        // act
        var result = await handler.Handle(queryWithExpectedResult.getCountriesByContinentsQuery, CancellationToken.None);

        // assert
        result.Count().Should().Be(queryWithExpectedResult.result);
    }

    [Fact()]
    public async void Handle_WithEmptyListOfContinents_ReturnAllCountries()
    {
        // arrange
        var getAllCountriesByContinentsQuery = new GetCountriesByContinentsQuery() {
            PageNumber = 1,
            PageSize = 200
        };
        var handler = new GetCountriesByContinentsQueryHandler(_dbContext, _mapper);
        var numerOfAllCountries = 195;

        // act
        var result = await handler.Handle(getAllCountriesByContinentsQuery, CancellationToken.None);

        // assert
        result.Count().Should().Be(numerOfAllCountries);
    }
}
