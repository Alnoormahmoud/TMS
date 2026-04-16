using System;

namespace TMS.Application.DTOs.Accounts
{
    public class AccountToLoginDTO
    {
        public string Number { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
