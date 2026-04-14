using System;
using TMS.Domain.Entities.People;

namespace TMS.Application.Interfaces.People
{
    public interface IPersonRepository
    {
        Task<int?> AddAsync(Person person);
        Task<bool> UpdateAsync(Person person);
        Task<bool> DeleteAsync(Person person);
        Task<Person?> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
    }
}
