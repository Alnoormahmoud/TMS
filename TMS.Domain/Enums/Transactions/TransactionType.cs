using System;

namespace TMS.Domain.Enums.Transactions
{
    public enum TransactionType : byte
    {
        Deposit = 1,
        Withdrawal = 2,
        Transfer = 3
    }
}
