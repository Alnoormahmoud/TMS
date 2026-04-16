using System;

namespace TMS.Application.DTOs.People
{
    public class PersonToUpdateDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
