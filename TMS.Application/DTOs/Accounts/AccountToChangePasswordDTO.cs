using System;

namespace TMS.Application.DTOs.Accounts
{
    public class AccountToChangePasswordDTO
    {
        public int Id { get; set; }
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
