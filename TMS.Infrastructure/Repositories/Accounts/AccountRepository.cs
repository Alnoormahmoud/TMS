using System;
using Microsoft.EntityFrameworkCore;
using TMS.Application.Interfaces.Accounts;
using TMS.Domain.Entities.Accounts;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Account account)
        {
            if (account is null || account.Person is null) return -1;

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                await _context.People
                    .AddAsync(account.Person);

                account.PersonId = account.Person.Id;

                await _context.Accounts
                    .AddAsync(account);

                await _context
                    .SaveChangesAsync();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback(); 
            }

            return account.Id;
        }

        public async Task<bool> UpdateAsync(Account account)
        {
            if (account is null || account.Person is null) return false;

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.People
                    .Update(account.Person);

                _context.Accounts
                    .Update(account);

                var result = await _context
                    .SaveChangesAsync() > 0;

                transaction.Commit();
                return result;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Account account)
        {
            if (account is null || account.Person is null) return false;

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.People
                    .Remove(account.Person);

                _context.Accounts
                    .Remove(account);

                var result = await _context
                    .SaveChangesAsync() > 0;

                transaction.Commit();
                return result;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts
                .Include(a => a.Person)
                .Include(a => a.TransactionEntries)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts
                .Include(a => a.Person)
                .Include(a => a.TransactionEntries)
                .ToListAsync();
        }

        public async Task<bool> ActivateAsync(Account account, bool activate)
        {
            if (account is null) return false;

            account.IsActive = activate;

            _context.Accounts
                .Update(account);

            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangePasswordAsync(Account account, string newPassword)
        {
            if (account is null) return false;

            account.Password = newPassword;

            _context.Accounts
                .Update(account);

            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<Account?> GetByNumberAsync(string number)
        {
            return await _context.Accounts
                .Include(a => a.Person)
                .Include(a => a.TransactionEntries)
                .FirstOrDefaultAsync(a => a.Number == number);
        }

    }
}