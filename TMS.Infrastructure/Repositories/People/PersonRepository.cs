using System;
using Microsoft.EntityFrameworkCore;
using TMS.Application.Interfaces.People;
using TMS.Domain.Entities.People;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories.People
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<int?> AddAsync(Person person)
        {
            _context.People.Add(person);

            if (await _context.SaveChangesAsync() > 0)
            {
                return _context.People.Max(person => person.Id);
            }

            return null;
        }

        public async Task<bool> DeleteAsync(Person person)
        {
            _context.People.Remove(person);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return _context.People
                .Where(x => x.Id > 0)
                .ToList();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            _context.People.Update(person);

            return await _context.SaveChangesAsync() > 0;
        }

    }
}
