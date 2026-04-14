using System;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Interfaces.People;
using TMS.Application.Services.People;

namespace TMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<PersonService>();

            return services;
        }
    }
}
