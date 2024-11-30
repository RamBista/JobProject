using JobBackEnd.DAL.Context;
using JobBackEnd.DAL.Repositories.Abstracts;
using JobBackEnd.Repositories.Implementations;
using JobBackEnd.Services.Abstracts;
using JobBackEnd.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace JobBackEnd;
public static class DependencyInjections
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<JobDbContext>(options =>
        //    options.UseSqlServer(configuration.GetConnectionString("SQLConnection"),
        // b => b.MigrationsAssembly(typeof(JobDbContext).Assembly.FullName)));

        services.AddDbContext<JobDbContext>(options =>
            options.UseInMemoryDatabase("InMemoryCandidateDb"));

        // Register Repository
        services.AddTransient<ICandidateRepository, SQLCandidateRepository>();

        // Register Service
        services.AddScoped<ICandidateService, CandidateService>();

        return services;
    }
}
