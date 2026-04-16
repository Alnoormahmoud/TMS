using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.Users;
using TMS.Domain.Entities.Users;

namespace TMS.Application.Interfaces.Users
{
    public interface IUserService
    {
        Task<int> AddAsync(UserToAddDTO user);
        Task<bool> UpdateAsync(UserToUpdateDTO user);
        Task<bool> DeleteAsync(int id);
        Task<UserDTO?> GetByIdAsync(int id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<bool> ChangePasswordAsync(int id, string oldPassword, string newPassword, string confirmPassword);
        Task<UserDTO?> LogInAsync(UserToLogInDTO usertologin);
    }
}
