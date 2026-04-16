using System;
using TMS.Domain.Entities.Bases;
using TMS.Domain.Entities.People;
using TMS.Domain.Entities.TransactionEntries;

namespace TMS.Domain.Entities.Accounts
{
    public class Account : BaseEntity
    {
        public string Number { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int PersonId { get; set; }
        public virtual Person Person { get; set; } = null!;

        public virtual ICollection<TransactionEntry> TransactionEntries { get; set; } = new List<TransactionEntry>();
    }
}
