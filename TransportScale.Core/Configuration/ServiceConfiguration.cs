using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TransportScale.Core.Services.Implementation;
using TransportScale.Core.Services.Interfacies;
using TransportScale.Core.Validation;
using TransportScale.Dto.DtoModels;

namespace TransportScale.Core.Configuration
{
    public static class ServiceConfiguration
    {
        public static void MakeServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICacheManager, CacheManager>();
            services.AddScoped<ITransportService, TransportService>();
            services.AddScoped<IJournalService, JournalService>();
            services.AddScoped<IValidator<TransportDto>, TransportValidator>();
            services.AddScoped<IValidator<JournalDto>, JournalValidator>();
            services.AddScoped<IValidator<TransportModel>, TransportModelValidation>();
        }
    }
}
