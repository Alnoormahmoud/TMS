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

        public async Task<int> AddAsync(Person person)
        {
            if (person is null) return -1;

            await _context.People
                .AddAsync(person);

            await _context
                .SaveChangesAsync();

            return person.Id;
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            _context.People
                .Update(person);

            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Person person)
        {
            _context.People
                .Remove(person);

            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.People
                .FindAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.People
                .ToListAsync();
        }

    }
}
