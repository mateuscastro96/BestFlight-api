using BestFlight.Infrastructure.Context;
using BestFlight.Infrastructure.Repositories;
using BestFlight.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace BestFlight.Infrastructure.IoC
{
    [ExcludeFromCodeCoverage]
    public static class Infrastructure
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IFlightRoutesRepository, FlightRoutesRepository>();
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("BestFlightDb");
            if (connection == null)
                throw new ArgumentNullException($"ConnectionString is null {nameof(connection)}");

            services.AddDbContext<BestFlightContext>(options => options.UseSqlite(connection));
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BestFlight API",
                    Version = "v1",
                    Description = "API that returns the best cost-benefit trip",
                    Contact = new OpenApiContact
                    {
                        Name = "Mateus Castro"
                    }
                });
            });
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services) =>
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        public static IHost RunMigration(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<BestFlightContext>();
            context.Database.Migrate();
            return host;
        }
    }
}
