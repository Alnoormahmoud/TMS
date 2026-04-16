
﻿using System;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities.People;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Entities.Transactions;
using TMS.Domain.Entities.Accounts;
using TMS.Domain.Entities.Users;


namespace TMS.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Domain.Entities.Transactions.Transaction> Transactions { get; set; }
        public DbSet<TransactionEntry> TransactionEntries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}
