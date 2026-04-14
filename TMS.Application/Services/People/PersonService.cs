using System;
using TMS.Application.DTOs.People;
using TMS.Application.Interfaces.People;
using TMS.Domain.Entities.People;

namespace TMS.Application.Services.People
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repo;

        public PersonService(IPersonRepository repo)
        {
            _repo = repo;
        }

        public async Task<int?> AddAsync(PersonToAddDTO dto)
        {
            var person = new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                DateOfBirth = dto.DateOfBirth
            };

            return await _repo.AddAsync(person);
        }

        public async Task<bool> UpdateAsync(PersonToUpdateDTO dto)
        {
            var person = await _repo.GetByIdAsync(dto.Id);

            if (person is null)
            {
                throw new Exception("لم يتم العثور على الشخص");
            }

            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            person.Email = dto.Email;
            person.Phone = dto.Phone;
            person.DateOfBirth = dto.DateOfBirth;

            return await _repo.UpdateAsync(person);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var person = await _repo.GetByIdAsync(id);

            if (person is null)
            {
                throw new Exception("لم يتم العثور على الشخص");
            }

            return await _repo.DeleteAsync(person);
        }

        public async Task<PersonDTO?> GetByIdAsync(int id)
        {
            var person = await _repo.GetByIdAsync(id);

            return person is not null 
                ? MapToDTO(person) 
                : null;
        }

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            return _repo.GetAllAsync()
                .Result
                .Select(person => MapToDTO(person))
                .ToList();
        }

        internal PersonDTO MapToDTO(Person person)
        {
            return new PersonDTO
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                Phone = person.Phone,
                DateOfBirth = person.DateOfBirth
            };
        }

    }
}