using Domain.Enteties;

namespace Infrastructure.Data.Seeders;

public class ContinentSeeder
{
    private readonly KeepLearningDbContext _dbContext;

    public ContinentSeeder(KeepLearningDbContext dbContext)
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
        IEnumerable<Continent> continents = new List<Continent>();

        try
        {

            continents = File.ReadAllLines("../Infrastructure/Data/Seeders/FilesWithData/ContinentsList.csv")
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

        return continents;
    }
}
