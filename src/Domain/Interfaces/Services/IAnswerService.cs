using static Domain.Models.Enums.GuessType;

public interface IAnswerService
{
  Task<string> GetCorrectAnswer(string questionText, Category category);
  Task<bool> IsCorrectAnswer(string questionText, string answerText, Category category);
}