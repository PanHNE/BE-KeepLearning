using Application.Common.Models.Answer;
using Application.Common.Models.Exam;

namespace Application.Exam.Queries.CheckExam;

public class CheckExamQuery : IRequest<ExamResultDto>
{
    public required string Category { get; set; }
    public required IEnumerable<AnswerDto> Answers { get; set; }
}
