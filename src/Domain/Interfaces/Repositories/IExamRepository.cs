using Domain.Entities;

namespace Domain.Interfaces;

public interface IExamRepository
{
  Task<Guid> Save(Exam exam, CancellationToken cancellationToken);
  Task<Exam?> Get(Guid examId);
  Task Delete(Exam exam, CancellationToken cancellationToken);
  Task<Exam> Update(Exam exam, CancellationToken cancellationToken);
}