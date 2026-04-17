using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.TransactionEntries;
using TMS.Application.DTOs.Transactions;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.Interfaces.TransactionEntries
{
    public interface ITransactionEntryService
    {
        public Task<TransactionEntryDTO?> GetByIdAsync(int Id);

        public Task<IEnumerable<TransactionEntryDTO>> GetAllAsync(TransactionEntriesFilterDTO dto);
    }
}
