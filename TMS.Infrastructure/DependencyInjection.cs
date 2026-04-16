using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Interfaces.Accounts;
using TMS.Application.Interfaces.People;
using TMS.Infrastructure.Persistence;
using TMS.Infrastructure.Repositories.Accounts;
using TMS.Infrastructure.Repositories.People;

namespace TMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connection)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}