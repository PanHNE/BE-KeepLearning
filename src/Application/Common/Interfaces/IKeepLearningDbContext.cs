namespace Application.Common.Interfaces;

public interface IKeepLearningDbContext
{
    DbSet<Domain.Enteties.Continent> Continents { get; }
    DbSet<Domain.Enteties.Country> Countries { get; }
    DbSet<Domain.Enteties.Question> Questions { get; }
    DbSet<Domain.Enteties.Exam> Exams { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
