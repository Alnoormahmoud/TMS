using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.Transactions;

namespace TMS.Application.Interfaces.Transactions
{
    public interface ITransactionService
    {
        public Task<IEnumerable<TransactionDTO>> GetAllAsync();
        public Task<TransactionDTO?> GetByIdAsync(int Id);

        public Task<int?> WithdrawAsync(DepositWithdrawDTO dto);

        public Task<int?> DepositAsync(DepositWithdrawDTO dto);

        public Task<int?> TransferAsync(TransferDTO dto);
    }
}
