using Application.Common.Interfaces;
using Domain.Enteties;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;

public class KeepLearningDbContext : IdentityDbContext<KeepLearningUser>, IKeepLearningDbContext
{
    public KeepLearningDbContext(DbContextOptions<KeepLearningDbContext> options) : base(options) { }

    public DbSet<Continent> Continents => Set<Continent>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Exam> Exams => Set<Exam>();
    public DbSet<Question> Questions => Set<Question>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>()
            .HasOne(country => country.Continent);

        modelBuilder.Entity<Exam>()
            .HasMany(e => e.Questions)
            .WithOne(q => q.Exam)
            .HasForeignKey(q => q.ExamId);
    }
}
