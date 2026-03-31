using Analytics.Api.DAL;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Analytics.Api.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly AnalyticsDbContext _dbContext;

        public DatabaseHealthCheck(AnalyticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var canConnect = await _dbContext.Database
                    .CanConnectAsync(cancellationToken);

                if (canConnect)
                {
                    return HealthCheckResult.Healthy("Database connection is OK");
                }

                return HealthCheckResult.Unhealthy("Database connection failed");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(
                    "Database connection threw exception",
                    ex);
            }
        }
    }
}
