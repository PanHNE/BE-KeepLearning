using Application.Common.Models.Answer;

namespace Application.Common.Models.Exam;

public record ExamResultDto(IEnumerable<AnswerResultDto> AnswerResults, int NumberOfGoodAnswers, int NumberOfBadAnswers) { }
