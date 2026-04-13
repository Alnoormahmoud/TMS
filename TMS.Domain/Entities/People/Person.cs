using System;
using TMS.Domain.Entities.Bases;
using TMS.Domain.Entities.Users;

namespace TMS.Domain.Entities.People
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
