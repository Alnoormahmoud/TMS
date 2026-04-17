using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Application.DTOs.Transactions
{
    public class DepositWithdrawDTO
    {
        public required string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
