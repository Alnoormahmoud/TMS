using System;
using TMS.Domain.Entities.Accounts;
using TMS.Domain.Entities.Bases;
using TMS.Domain.Entities.Transactions;
using TMS.Domain.Enums.TransactionEntries;

namespace TMS.Domain.Entities.TransactionEntries
{
    public class TransactionEntry : BaseEntity
    {
        public int TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; } = null!;

        public int AccountId { get; set; }
        public virtual Account Account { get; set; } = null!;

        public EntryType EntryType { get; set; }
    }
}
