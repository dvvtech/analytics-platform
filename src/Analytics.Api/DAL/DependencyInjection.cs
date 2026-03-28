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
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                if (logger != null)
                {
                    logger.LogInformation("connection string: " + dbOptions.ConnectionString);
                }
                options.UseNpgsql(dbOptions.ConnectionString);
            });

            return services;
        }
    }
}
