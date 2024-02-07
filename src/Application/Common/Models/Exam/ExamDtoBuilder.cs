using Application.Common.Models.Continent;
using Application.Common.Models.Exam.Country;
using Application.Common.Models.Question;
using Domain.Models.Enums;
using static Domain.Models.Enums.GuessType;

namespace Application.Common.Models.Exam;

public static class ExamDtoBuilder
{
    public static ExamDto CreateExam(string? name, List<QuestionDto> questions)
    {
        var examDto = new ExamDto()
        {
            Name = name,
            Questions = questions
        };

        return examDto;
    }

    public static ExamCountryDto CreateExamCountry(string? name, List<QuestionDto> questions, Category category, List<ContinentDto> continents)
    {
        var examDto = new ExamCountryDto()
        {
            Name = name,
            Questions = questions,
            Category = GuessType.ToString(category),
            Continents = continents
        };

        return examDto;
    }
}
