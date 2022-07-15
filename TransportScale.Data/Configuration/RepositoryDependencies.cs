using Microsoft.Extensions.DependencyInjection;
using TransportScale.Data.Repositries.Implementation;
using TransportScale.Data.Repositries.Interfacies;

namespace TransportScale.Data.Configuration
{
    public static class RepositoryDependencies
    {
        public static void MakeRepositoryDependencies(this IServiceCollection services)
        {
            services.AddScoped<IJournalRepository, JournalRepository>();
            services.AddScoped<ITransportRepository, TransportRepository>();
            services.AddScoped<ITransportQuantityRepository, TransportQuantityRepository>();
        }
    }
}
