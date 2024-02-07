using Application.UnitTests.Helper;
using Domain.Enteties;

namespace Infrastructure.Helper.Seeders.UnitTests;

internal class ContinentSeederTest
{
    private readonly KeepLearningDbContextTest _dbContext;

    public ContinentSeederTest(KeepLearningDbContextTest dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (!_dbContext.Continents.Any())
        {
            var continents = GetContinentsFromFile();

            if (continents != null)
            {
                continents.ToList().ForEach(continent =>
                {
                    _dbContext.Continents.Add(continent);
                    _dbContext.SaveChanges();
                });
            }
        }
    }

    private IEnumerable<Continent> GetContinentsFromFile()
    {
        IEnumerable<Continent> countries = new List<Continent>();

        try
        {
            countries = File.ReadAllLines("../../../Helper/Seeders/FilesWithData/ContinentsList.csv")
                .Skip(1)
                .Select(name => new Continent()
                {
                    Name = name
                });

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return countries;
    }
}
