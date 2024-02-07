namespace Application.Question.Queries.GenerateQuestion.IntegrationTests;

public class GenerateQuestionQueryHandlerIntegrationTests : TestBasics
{
    public static IEnumerable<object[]> GetRandomQuestionQuerySamples()
    {
        var list = new List<GenerateQuestionQuery>()
        {
            new GenerateQuestionQuery()
            {
                Category = "Country",
                Continent = "Asia",
            },
            new GenerateQuestionQuery()
            {
                Category = "Capital City",
                Continent = "Asia",
            },
            new GenerateQuestionQuery()
            {
                Category = "Capital City",
                Continent = "Europe",
            },
            new GenerateQuestionQuery()
            {
                Category = "Country",
                Continent = "North America",
            }
        };

        return list.Select(el => new object[] { el });
    }

    [Theory()]
    [MemberData(nameof(GetRandomQuestionQuerySamples))]
    public async void Handle_WithCorrectData_ReturnQuestion(GenerateQuestionQuery generateQuestionQuery)
    {
        // arrange
        var generateQuestionQueryHandler = new GenerateQuestionQueryHandler(_dbContext, _countryService, _mapper);

        // act
        var result = await generateQuestionQueryHandler.Handle(generateQuestionQuery, CancellationToken.None);

        // assert
        result.Should().NotBeNull();
        result.QuestionText.Should().NotBeNullOrWhiteSpace();
        result.AnswerText.Should().NotBeNullOrWhiteSpace();
        result.QuestionNumber.Should().Be(1);
    }
}