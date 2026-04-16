using System;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Interfaces.Accounts;
using TMS.Application.Interfaces.People;
using TMS.Application.Interfaces.TransactionEntries;
using TMS.Application.Interfaces.Transactions;
using TMS.Application.Services.People;
using TMS.Application.Services.TransactionEntries;
using TMS.Application.Services.Transactions;
using TMS.Application.Interfaces.Users;
using TMS.Application.Services.Accounts;
using TMS.Application.Services.People;
using TMS.Application.Services.Users;


namespace TMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();

            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionEntryService, TransactionEntryService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();


            return services;
        }
    }
}