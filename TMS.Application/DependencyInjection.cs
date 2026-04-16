using System;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Interfaces.Accounts;
using TMS.Application.Interfaces.People;
using TMS.Application.Services.Accounts;
using TMS.Application.Services.People;

namespace TMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}