using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.Accounts;
using TMS.Domain.Enums.TransactionEntries;

namespace TMS.Application.DTOs.TransactionEntries
{
    public class TransactionEntryDTO
    {
        public int Id { get; set; }
       
        public EntryType EntryType { get; set; }

        public decimal Amount { get; set; }

        public string AccountNumber { get; set; } = null!;
        public int TransactionID { get; set; }

       

    }
}
