namespace Application.Question.Queries.CheckAnswer;

public class CheckQuestionQuery : IRequest<bool>
{
    public required string Question { get; set; }
    public string Answer { get; set; } = default!;
    public required string Category { get; set; }
}
