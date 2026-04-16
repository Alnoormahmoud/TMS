using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.Users;
using TMS.Domain.Entities.Accounts;
using TMS.Domain.Entities.People;
using TMS.Domain.Entities.Users;

namespace TMS.Application.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<int> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> ChangePasswordAsync(User user, string newPassword);
        Task<User> LogInAsync(UserToLogInDTO usertologin);


    }
}
