using BestFlight.Application.Services;
using BestFlight.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BestFlight.Application.IoC
{
    [ExcludeFromCodeCoverage]
    public static class ServiceIoc
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IFlightRoutesService, FlightRoutesService>();
            return services;
        }
    }
}
