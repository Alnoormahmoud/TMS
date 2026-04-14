using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.Users;

namespace TMS.Application.Interfaces.Users
{
    public interface IUserService
    {
        Task<int> AddAsync(UserToAddDTO user);
        Task<bool> UpdateAsync(UserToUpdateDTO user);
        Task<bool> DeleteAsync(int id);
        Task<UserDTO?> GetByIdAsync(int id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
    }
}
