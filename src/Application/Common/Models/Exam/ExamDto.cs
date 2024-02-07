using Application.Common.Models.Question;

namespace Application.Common.Models.Exam;

public class ExamDto
{
    public string? Name { get; set; }
    public IEnumerable<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
}
