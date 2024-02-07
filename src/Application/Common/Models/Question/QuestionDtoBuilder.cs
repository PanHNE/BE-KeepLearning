using Application.Common.Models.Country;
using Application.Exam.Queries.GenerateExamCountry;
using Domain.Models.Enums;

namespace Application.Common.Models.Question;

public static class QuestionDtoBuilder
{
    public static List<QuestionDto> CreateQuestions(List<CountryDto> countries, GenerateExamCountryQuery command)
    {
        var questions = new List<QuestionDto>() { };
        var category = GuessType.ToCategory(command.Category);

        for (var i = 0; i < countries.Count(); i++)
        {
            var newQuestion = CreateQuestionByCategory(countries[i], category, i + 1);
            questions.Add(newQuestion);
        }

        return questions;
    }

    public static QuestionDto CreateQuestionByCategory(CountryDto country, GuessType.Category category, int questionNumber = 1)
    {
        switch (category)
        {
            case GuessType.Category.CapitalCity:
                return new QuestionDto(questionNumber, country.Name, country.CapitalCity);
            case GuessType.Category.Country:
                return new QuestionDto(questionNumber, country.CapitalCity, country.Name);
            default:
                throw new ArgumentException();
        }
    }
}
