using System;

namespace TMS.Application.DTOs.Accounts
{
    public class AccountToAddDTO
    {
        // معلومات الشخص
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }

        // معلومات الحساب
        // باقي المعلومات يتم إسنادها في الخدمة
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
