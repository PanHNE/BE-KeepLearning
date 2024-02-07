using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using EntityContinent = Domain.Enteties.Continent;
using EntityCountry = Domain.Enteties.Country;
using EntityExam = Domain.Enteties.Exam;
using EntityQuestion = Domain.Enteties.Question;

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
