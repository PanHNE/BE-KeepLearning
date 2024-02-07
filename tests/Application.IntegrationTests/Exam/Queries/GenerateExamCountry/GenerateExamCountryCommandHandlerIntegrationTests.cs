using Application.Exam.Queries.GenerateExamCountry;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.CreateExamCountry.IntegrationTests;

public class GenerateExamCountryCommandHandlerIntegrationTests : TestBasics
{
    public static IEnumerable<object[]> GetCommands()
    {
        var list = new List<GenerateExamCountryQuery>()
        {
            new GenerateExamCountryQuery(){
                Name = "Exam 1",
                NumberOfQuestion = 5,
                Category = "Country",
                Continents = new List<string>() { "Europe", "Asia" }
            },
            new GenerateExamCountryQuery(){
                Name = "Exam 2",
                NumberOfQuestion = 10,
                Category = "Country",
                Continents = new List<string>() { "Europe", "Asia" }
            },
            new GenerateExamCountryQuery(){
                Name = "Exam 3",
                NumberOfQuestion = 25,
                Category = "Country",
                Continents = new List<string>() { "Europe", "Asia" }
            },
            new GenerateExamCountryQuery(){
                Name = "",
                NumberOfQuestion = 50,
                Category = "Country",
                Continents = new List<string>() { "Europe", "Asia" }
            },
        };

        return list.Select(el => new object[] { el });
    }

    [Theory()]
    [MemberData(nameof(GetCommands))]
    public async void Handle_CreateExamWithAllValidData_ReturnExam(GenerateExamCountryQuery createExamCountryCommand)
    {
        // arrange
        var createExamCountryCommandHandler = new GenerateExamCountryQueryHandler(_dbContext, _mapper, _countryService);
        var continents = createExamCountryCommand.Continents.Select(c => c);
        var expectedCategory = createExamCountryCommand.Category;

        // act
        var result = await createExamCountryCommandHandler.Handle(createExamCountryCommand, CancellationToken.None);

        // assert
        result.Category.Should().Be(expectedCategory);
        result.Name.Should().Be(createExamCountryCommand.Name);
        result.Continents.Select(c => c.Name).All(createExamCountryCommand.Continents.Contains);
        result.Questions.Count().Should().Be(createExamCountryCommand.NumberOfQuestion);
    }

    public static IEnumerable<object[]> GetInvalidCommands()
    {
        var list = new List<GenerateExamCountryQuery>()
        {
            new GenerateExamCountryQuery(){
                Name = "Exam 1",
                NumberOfQuestion = 105,
                Category = "Country",
                Continents = new List<string>() { "Europe"}
            }
        };

        return list.Select(el => new object[] { el });
    }

    [Theory()]
    [MemberData(nameof(GetCommands))]
    public void Handle_CreateExamWithNumberOfQuestionIsBiggerThanCountriesInContinent_ReturnExam(GenerateExamCountryQuery createExamCountryCommand)
    {
        // arrange
        var createExamCountryCommandHandler = new GenerateExamCountryQueryHandler(_dbContext, _mapper, _countryService);
        var continents = createExamCountryCommand.Continents.Select(c => c);

        // act
        var action = () => createExamCountryCommandHandler.Handle(createExamCountryCommand, CancellationToken.None);

        // assert
        action.Invoking(action => action.Invoke())
            .Should().ThrowAsync<NotFoundException>();
    }
}