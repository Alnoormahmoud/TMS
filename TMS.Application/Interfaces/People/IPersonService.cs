using System;
using TMS.Application.DTOs.People;

namespace TMS.Application.Interfaces.People
{
    public interface IPersonService
    {
        Task<int?> AddAsync(PersonToAddDTO person);
        Task<bool> UpdateAsync(PersonToUpdateDTO person);
        Task<bool> DeleteAsync(int id);
        Task<PersonDTO?> GetByIdAsync(int id);
        Task<IEnumerable<PersonDTO>> GetAllAsync();
    }
}
