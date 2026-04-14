using System;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Interfaces.People;
using TMS.Infrastructure.Persistence;
using TMS.Infrastructure.Repositories.People;

namespace TMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            return services;
        }
    }
}