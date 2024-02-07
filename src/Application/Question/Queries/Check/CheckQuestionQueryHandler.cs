using Domain.Models.Enums;

namespace Application.Question.Queries.CheckAnswer;

public class CheckQuestionQueryHandler : IRequestHandler<CheckQuestionQuery, bool>
{
    private readonly IAnswerService _answerSerivce;

    public CheckQuestionQueryHandler(IAnswerService answerSerivce)
    {
        _answerSerivce = answerSerivce;
    }

    public async Task<bool> Handle(CheckQuestionQuery request, CancellationToken cancellationToken)
    {
        var category = GuessType.ToCategory(request.Category);

        var result = await _answerSerivce.IsCorrectAnswer(request.Question, request.Answer, category);

        return result;
    }
}
