using System;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Interfaces.People;
using TMS.Application.Interfaces.Users;
using TMS.Application.Services.People;
using TMS.Application.Services.Users;

namespace TMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
