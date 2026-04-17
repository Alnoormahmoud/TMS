using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TMS.Application.DTOs.TransactionEntries;
using TMS.Application.Interfaces.TransactionEntries;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Enums.TransactionEntries;
using TMS.Domain.Enums.Transactions;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories.TransactionEntries
{
    public class TransactionEntryRepository :ITransactionEntryRepository
    {
        private readonly AppDbContext _Context;
        public TransactionEntryRepository(AppDbContext Context)
        {
            _Context = Context;
        }

        public async Task<IEnumerable<TransactionEntry>> GetAllAsync(TransactionEntriesFilterDTO dto)
        {
            var Query = _Context.TransactionEntries.Where((x) => true);

            if (dto.AccountNumber != null)
            {
                Query = Query
                    .Where(x => (x.Account.Number == dto.AccountNumber));
            }

            if (dto.TransactionType.HasValue)
            {
                Query = Query
                    .Where(x => (x.Transaction.Type == dto.TransactionType));
            }

            return await Query
                .Include(x=>x.Transaction)
                .Include(x=>x.Account)
                .ToListAsync();

        }

        public async Task<TransactionEntry?> GetByIdAsync(int Id)
        {
            return await _Context.TransactionEntries
                .Include(x=>x.Account)
                .Include(x=>x.Transaction)
                .FirstOrDefaultAsync(x=>x.Id == Id);
        }

        public async Task<int> AddEntryAsync(EntryType Type, int TransactionId, int AccountId)
        {
            var NewEntry = new TransactionEntry()
            {
                EntryType = Type,
                TransactionId = TransactionId,
                AccountId = AccountId
            };
            await _Context.TransactionEntries.AddAsync(NewEntry);

            _Context.SaveChanges();

            return NewEntry.Id;
        }
    }
}
