using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TransportScale.Server.HealthCheck
{
    public class LivenessCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy("Healthy"));
        }
    }
}
