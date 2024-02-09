namespace Application.Common.Interfaces;

public interface IKeepLearningDbContext
{
    DbSet<Domain.Entities.Continent> Continents { get; }
    DbSet<Domain.Entities.Country> Countries { get; }
    DbSet<Domain.Entities.Question> Questions { get; }
    DbSet<Domain.Entities.Exam> Exams { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
