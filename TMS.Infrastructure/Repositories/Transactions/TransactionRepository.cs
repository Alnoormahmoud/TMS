using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TMS.Application.DTOs.Transactions;
using TMS.Application.Interfaces.Transactions;
using TMS.Domain.Entities.Accounts;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Entities.Transactions;
using TMS.Domain.Enums.TransactionEntries;
using TMS.Domain.Enums.Transactions;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _Context;
        public TransactionRepository(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _Context.Transactions.ToListAsync();
        }


        public async Task<Transaction?> GetByIdAsync(int Id)
        {
            return await _Context.Transactions.FindAsync(Id);

        }
        
        public async Task<int> AddAsync(TransactionType Type, decimal Amount)
        {
            var NewTransaction = new Transaction()
            {
                Type = Type,
                Amount = Amount,
                Date = DateTime.Now
            };
            await _Context.Transactions.AddAsync(NewTransaction);
            _Context.SaveChanges();

            return NewTransaction.Id;
        }

    }

}
