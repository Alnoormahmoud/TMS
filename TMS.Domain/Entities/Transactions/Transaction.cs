using System;
using TMS.Domain.Entities.Bases;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Enums.Transactions;

namespace TMS.Domain.Entities.Transactions
{
    public class Transaction : BaseEntity
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<TransactionEntry> Entries { get; set; } = new List<TransactionEntry>();
    }
}
