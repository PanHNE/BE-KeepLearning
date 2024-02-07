using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Seeders;

public class UserSeeder
{
    private readonly UserManager<KeepLearningUser> _userManager;

    public UserSeeder(UserManager<KeepLearningUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<KeepLearningUser> Seed(IdentityRole identityRole)
    {
        var administrator = new KeepLearningUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(identityRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { identityRole.Name });
            }
        }

        return administrator;
    }
}
