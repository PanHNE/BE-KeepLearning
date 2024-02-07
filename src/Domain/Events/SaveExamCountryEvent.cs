using Domain.Enteties;

namespace Domain.Events;

public class SaveExamCountryEvent : BaseEvent
{
  public SaveExamCountryEvent(Exam exam)
  {
    Exam = exam;
  }

  public Exam Exam { get; }
}