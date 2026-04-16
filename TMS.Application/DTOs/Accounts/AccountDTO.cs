using System;

namespace TMS.Application.DTOs.Accounts
{
    public class AccountDTO
    {
        // معلومات الشخص
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }

        // معلومات الحساب
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
