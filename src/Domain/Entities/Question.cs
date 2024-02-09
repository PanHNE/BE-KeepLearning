namespace Domain.Entities;

public class Question : BaseAuditableEntity
{
    public required int QuestionNumber { get; set; }
    public required string QuestionText { get; set; } = default!;
    public required string AnswerText { get; set; } = default!;
    public DateTime ExpiredAt { get; set; } = DateTime.Now.AddMinutes(70);
    public Guid? ExamId { get; set; }
    public Exam? Exam { get; set; }
}
