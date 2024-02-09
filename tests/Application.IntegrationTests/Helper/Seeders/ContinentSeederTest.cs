using Application.Common.Interfaces;
using EContinent = Domain.Entities.Continent;

namespace Application.Helper.Seeders.IntegrationTests
{
    internal class ContinentSeederTest
    {
        private readonly IKeepLearningDbContext _dbContext;

        public ContinentSeederTest(IKeepLearningDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            var hasAny = _dbContext.Continents.Any();

            if (!hasAny)
            {
                var continents = GetContinentsFromFile();

                if (continents != null)
                {
                    continents.ToList().ForEach(continent =>
                    {
                        _dbContext.Continents.Add(continent);
                        _dbContext.SaveChangesAsync(CancellationToken.None);
                    });
                }
            }
        }

        private IEnumerable<EContinent> GetContinentsFromFile()
        {
            IEnumerable<EContinent> countries = new List<EContinent>();

            try
            {
                countries = File.ReadAllLines("../../../Helper/Seeders/FilesWithData/ContinentsList.csv")
                    .Skip(1)
                    .Select(name => new EContinent()
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
}
