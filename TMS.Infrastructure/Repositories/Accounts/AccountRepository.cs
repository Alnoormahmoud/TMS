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
            if (account is null) return -1;

            await _context.Accounts
                .AddAsync(account);

            await _context
                .SaveChangesAsync();

            return account.Id;
        }

        public async Task<bool> UpdateAsync(Account account)
        {
            _context.Accounts
                .Update(account);

            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Account account)
        {
            _context.Accounts
                .Remove(account);

            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts
                .Include(a => a.Person)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts
                .Include(a => a.Person)
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

            return await _context
                .SaveChangesAsync() > 0;
        }

    }
}