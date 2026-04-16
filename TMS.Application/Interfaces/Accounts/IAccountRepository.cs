using System;
using TMS.Domain.Entities.Accounts;

namespace TMS.Application.Interfaces.Accounts
{
    public interface IAccountRepository
    {
        Task<int> AddAsync(Account account);
        Task<bool> UpdateAsync(Account account);
        Task<bool> DeleteAsync(Account account);
        Task<bool> ActivateAsync(Account account, bool activate);
        Task<bool> ChangePasswordAsync(Account account, string newPassword);
        Task<Account?> GetByIdAsync(int id);
        Task<Account?> GetByNumberAsync(string number);
        Task<IEnumerable<Account>> GetAllAsync();
    }
}
