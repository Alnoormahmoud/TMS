using Microsoft.EntityFrameworkCore;
using System;
using TMS.Domain.Entities.People;
using TMS.Domain.Entities.Users;

namespace TMS.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
