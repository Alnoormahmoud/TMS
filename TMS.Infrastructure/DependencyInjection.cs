using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Interfaces.Accounts;
using TMS.Application.Interfaces.People;
using TMS.Application.Interfaces.TransactionEntries;
using TMS.Application.Interfaces.Transactions;
using TMS.Application.Interfaces.Users;
using TMS.Infrastructure.Persistence;
using TMS.Infrastructure.Repositories.Accounts;
using TMS.Infrastructure.Repositories.People;
using TMS.Infrastructure.Repositories.TransactionEntries;
using TMS.Infrastructure.Repositories.Transactions;
using TMS.Infrastructure.Repositories.Users;


namespace TMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connection)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<ITransactionEntryRepository, TransactionEntryRepository>();

            return services;
        }
    }
}