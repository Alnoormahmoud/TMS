using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.Users;
 using TMS.Application.Interfaces.Users;
using TMS.Domain.Entities.Users;

namespace TMS.Application.Services.Users
{
    public class UserService : IUserService 
    {
        private readonly IUserRepository  _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> AddAsync(UserToAddDTO dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
                PersonId = dto.PersonId,

                //add fixed current user id for now, later we will get it from the token
                CreatedByUserId = 9,

                // we dont take the crated at because we want it to be set to the current time when we create the user, we can take it from the dto but it will be ignored and overridden by the current time
                CreatedAt = DateTime.Now,

                Person = null!, // will be set by EF Core when we save changes, we just need to set the foreign key (PersonId)             
             
                CreatedByUser = null! // will be set by EF Core when we save changes, we just need to set the foreign key (CreatedByUserId)


            };

            return await _repo.AddAsync(user);
        }

        public async Task<bool> UpdateAsync(UserToUpdateDTO dto)
        {
            var user = await _repo.GetByIdAsync(dto.Id);

            if (user is null)
            {
                return false;
            }
            user.UserName = dto.UserName;
            user.Password = dto.Password; 

            return await _repo.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);

            return user is not null && await _repo.DeleteAsync(user);
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);

            return user is null ? null : MapToDTO(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var Users = await _repo.GetAllAsync();

            return Users.Select(MapToDTO);
        }

        private static UserDTO MapToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,               
                CreatedByUserId = user.CreatedByUserId,
                CreatedByUserName = user.CreatedByUser.UserName,
                PersonId = user.PersonId,
                PersonFullName = user.Person.FirstName + " " + user.Person.LastName,
                CreatedAt = user.CreatedAt
            };
        }

    }
}
