using System;
using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities.People;

namespace TMS.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(@"Server=db47596.public.databaseasp.net,1433;Initial Catalog=db47596;Persist Security Info=False;User ID=db47596;Password=2e#MW6n-7i%Q;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

        public DbSet<Person> People { get; set; }
    }
}
