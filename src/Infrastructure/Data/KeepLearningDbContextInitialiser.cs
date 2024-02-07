using Infrastructure.Data.Seeders;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class KeepLearningDbContextInitialiser
{
    private readonly ILogger<KeepLearningDbContextInitialiser> _logger;
    private readonly KeepLearningDbContext _dbContext;
    private readonly UserManager<KeepLearningUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public KeepLearningDbContextInitialiser(ILogger<KeepLearningDbContextInitialiser> logger, KeepLearningDbContext context, UserManager<KeepLearningUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _dbContext = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var roleSeeder = new RoleSeeder(_roleManager);
        var administratorRole = await roleSeeder.Seed();

        // Default users
        var userSeeder = new UserSeeder(_userManager);
        await userSeeder.Seed(administratorRole);

        // contientns
        var continentSeeder = new ContinentSeeder(_dbContext);
        continentSeeder.Seed();

        // countries
        var countrySeeder = new CountrySeeder(_dbContext);
        await countrySeeder.Seed();
    }
}
