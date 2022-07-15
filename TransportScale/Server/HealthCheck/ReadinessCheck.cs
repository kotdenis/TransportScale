using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TransportScale.Data;

namespace TransportScale.Server.HealthCheck
{
    public class ReadinessCheck : IHealthCheck
    {
        private readonly TransportDbContext _dbContext;

        public ReadinessCheck(TransportDbContext transportDbContext) => _dbContext = transportDbContext;

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            _ = await _dbContext.Database.ExecuteSqlInterpolatedAsync($"select 1;", cancellationToken);
            return HealthCheckResult.Healthy("Database is working");
        }
    }
}
