using Application.Common.Models.Question;

namespace Application.Question.Queries.GenerateQuestion;

public class GenerateQuestionQuery : IRequest<QuestionDto>
{
    public required string Category { get; set; }
    public required string Continent { get; set; }
}
