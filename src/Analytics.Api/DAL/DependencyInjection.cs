using Analytics.Api.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Api.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));            

            services.AddDbContextFactory<AnalyticsDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                options.UseNpgsql("User ID=postgres;Password=curMal7Hof!;Host=176.108.255.113;Port=5432;Database=web-analytics");
            });

            return services;
        }
    }
}
