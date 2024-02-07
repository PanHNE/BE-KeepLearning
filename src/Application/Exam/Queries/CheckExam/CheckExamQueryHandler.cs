using Application.Common.Models.Answer;
using Application.Common.Models.Exam;
using Domain.Models.Enums;
using Infrastructure.Services;

namespace Application.Exam.Queries.CheckExam;

public class CheckExamQueryHandler : IRequestHandler<CheckExamQuery, ExamResultDto>
{
    private readonly IAnswerService _answerService;

    public CheckExamQueryHandler(IAnswerService answerService)
    {
        _answerService = answerService;
    }

    // TODO: Add validation to CheckExamQuery
    public async Task<ExamResultDto> Handle(CheckExamQuery request, CancellationToken cancellationToken)
    {
        var answers = new List<AnswerResultDto>();
        var numberOfCorrectAnswers = 0;
        var numberOfIncorrectAnswers = 0;
        var category = GuessType.ToCategory(request.Category);

        foreach (var answer in request.Answers)
        {
            var correctAnswer = await _answerService.GetCorrectAnswer(answer.QuestionText, category);

            if (answer.AnswerText is not null && answer.AnswerText?.ToLower() == correctAnswer.ToLower())
            {
                numberOfCorrectAnswers++;
            }
            else
            {
                numberOfIncorrectAnswers++;
            }
            answers.Add(new AnswerResultDto(answer.NumberOfQuestion, answer.AnswerText, correctAnswer));
        }

        var examResultDto = new ExamResultDto(answers, numberOfCorrectAnswers, numberOfIncorrectAnswers);

        return examResultDto;
    }
}
