using Application.Common.Interfaces;
using Infrastructure.Data.Interceptors;
using Domain.Constants;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_KL_DB");

            Guard.Against.Null(connectionString, message: "Connection string not found.");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<KeepLearningDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IKeepLearningDbContext>(provider => provider.GetRequiredService<KeepLearningDbContext>());
            services.AddScoped<KeepLearningDbContextInitialiser>();

            services
                .AddIdentityCore<KeepLearningUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<KeepLearningDbContext>()
                .AddApiEndpoints();

            services
                .AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);

            services.AddAuthorizationBuilder();

            services.AddSingleton(TimeProvider.System);
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddScoped<IContinentRepository, ContinentRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IExamRepository, ExamRepository>();

            services.AddAuthorization(options =>
                options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

            return services;
        }
    }

    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<KeepLearningDbContextInitialiser>();

            await initialiser.InitialiseAsync();

            await initialiser.SeedAsync();
        }
    }
}
