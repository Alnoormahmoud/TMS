using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.TransactionEntries;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.DTOs.Transactions
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<TransactionEntryDTO> Entries { get; set; } = new List<TransactionEntryDTO>();
    }
}
