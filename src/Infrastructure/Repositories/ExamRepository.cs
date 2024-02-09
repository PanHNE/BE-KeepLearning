using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ExamRepository : IExamRepository
{
  private readonly IKeepLearningDbContext _dbContext;

  public ExamRepository(IKeepLearningDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task Delete(Exam exam, CancellationToken cancellationToken)
  {
    _dbContext.Exams.Remove(exam);
    await _dbContext.SaveChangesAsync(cancellationToken);
  }

  public async Task<Exam?> Get(Guid examId)
  {
    return await _dbContext.Exams.FirstOrDefaultAsync(e => e.Id == examId);
  }

  public async Task<Guid> Save(Exam exam, CancellationToken cancellationToken)
  {
    _dbContext.Exams.Add(exam);
    await _dbContext.SaveChangesAsync(cancellationToken);

    return exam.Id;
  }

  public async Task<Exam> Update(Exam exam, CancellationToken cancellationToken)
  {
    _dbContext.Exams.Update(exam);
    await _dbContext.SaveChangesAsync(cancellationToken);

    return exam;
  }
}
