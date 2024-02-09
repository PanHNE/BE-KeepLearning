using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using EntityContinent = Domain.Entities.Continent;
using EntityCountry = Domain.Entities.Country;
using EntityExam = Domain.Entities.Exam;
using EntityQuestion = Domain.Entities.Question;

namespace Application.UnitTests.Helper;

internal class KeepLearningDbContextTest: DbContext, IKeepLearningDbContext
{
    public KeepLearningDbContextTest(DbContextOptions<KeepLearningDbContextTest> options) : base(options) { }

    public DbSet<EntityContinent> Continents => Set<EntityContinent>();
    public DbSet<EntityCountry> Countries => Set<EntityCountry>();
    public DbSet<EntityExam> Exams => Set<EntityExam>();
    public DbSet<EntityQuestion> Questions => Set<EntityQuestion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EntityCountry>()
            .HasOne(country => country.Continent);

        modelBuilder.Entity<EntityExam>()
            .HasMany(e => e.Questions)
            .WithOne(q => q.Exam)
            .HasForeignKey(q => q.ExamId);
    }
}
