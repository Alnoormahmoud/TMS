using System;
using TMS.Application.DTOs.Accounts;

namespace TMS.Application.Interfaces.Accounts
{
    public interface IAccountService
    {
        Task<int> AddAsync(AccountToAddDTO person);
        Task<bool> UpdateAsync(AccountToUpdateDTO person);
        Task<bool> DeleteAsync(int id);
        Task<bool> ActivateAsync(int id, bool activate);
        Task<bool> ChangePasswordAsync(int id, string newPassword, string confirmPassword);
        Task<AccountDTO?> GetByIdAsync(int id);
        Task<AccountDTO?> GetByNumberAsync(string number);
        Task<IEnumerable<AccountDTO>> GetAllAsync();
    }
}
