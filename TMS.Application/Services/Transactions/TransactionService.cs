using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TMS.Application.DTOs.Transactions;
using TMS.Application.Interfaces.Transactions;
using TMS.Domain.Entities.Accounts;
using TMS.Domain.Entities.Transactions;
using TMS.Domain.Enums.TransactionEntries;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;
        public TransactionService(ITransactionRepository repo)
        {
            _repo = repo;
        }

       
        public async Task<IEnumerable<TransactionDTO>> GetAllAsync()
        {
            List<TransactionDTO> DTOList = new List<TransactionDTO>();
            var transactions = await _repo.GetAllAsync();

            foreach (var transaction in transactions)
            {
                DTOList.Add(MapToDTO(transaction));
            }

            return DTOList;
        }

        public async Task<TransactionDTO?> GetByIdAsync(int Id)
        {
            var transaction = await _repo.GetByIdAsync(Id);
            return transaction is null
                ? null : MapToDTO(transaction);
        }


        public async Task<int?> TransferAsync(TransferDTO dto)
        {

            using var transaction = await _Context.Database.BeginTransactionAsync();
            int? NewTransactionId = null;
            try
            {
                var FromAccount = await _GetAccountAsync(dto.FromAccountNumber);
                if (FromAccount is null)
                {
                    await transaction.RollbackAsync();
                    return null;// Error invalid FromAccountNumber
                }

                var ToAccount = await _GetAccountAsync(dto.ToAccountNumber);
                if (ToAccount is null)
                {
                    await transaction.RollbackAsync();
                    return null;// Error invalid ToAccountNumber
                }


                NewTransactionId = await _TransferHelper(FromAccount, ToAccount, dto.Amount);


                await _Context.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return null;
            }

            return NewTransactionId;
        }

        public async Task<int?> WithdrawAsync(DepositWithdrawDTO dto)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            int? NewTransactionId = null;
            try
            {

                var Account = await _GetAccountAsync(dto.AccountNumber);
                if (Account is null)
                {
                    return null;// Error invalid accountNumber
                }

                NewTransactionId = await _WithdrawalHelperAsync(Account, dto.Amount);
                if (NewTransactionId is null)
                {
                    return null;// Error insufficient Balance or Amount < 0
                }


                transaction.Complete();

            }
            catch (Exception)
            {
                return null;
            }

            return NewTransactionId;
        }

        public async Task<int?> DepositAsync(DepositWithdrawDTO dto)
        {
            using var transaction = await _Context.Database.BeginTransactionAsync();
            int? NewTransactionId = null;

            try
            {
                var Account = await _GetAccountAsync(dto.AccountNumber);
                if (Account is null)
                {
                    await transaction.RollbackAsync();
                    return null;
                }

                NewTransactionId = await _DepositHelperAsync(Account, dto.Amount);
                if (NewTransactionId is null)
                {
                    await transaction.RollbackAsync();
                    return null;// Error Amount < 0
                }

                await _Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return null;
            }

            return NewTransactionId;
        }


        private async Task<int?> _DepositHelperAsync(Account Account, decimal Amount)
        {
            if (Amount < 0)
                return null;

            Account.Balance += Amount;

            int NewTransactionId = await _AddTransactionAsync(TransactionType.Deposit, Amount);

            await _AddEntryAsync(EntryType.In, NewTransactionId, Account.Id);

            return NewTransactionId;
        }

        private async Task<int?> _WithdrawalHelperAsync(Account Account, decimal Amount)
        {
            if (Account.Balance < Amount || Amount < 0)
                return null;

            Account.Balance -= Amount;

            int NewTransactionId = await _AddTransactionAsync(TransactionType.Withdrawal, Account.Id);

            await _AddEntryAsync(EntryType.Out, NewTransactionId, Account.Id);

            return NewTransactionId;
        }

        private async Task<int?> _TransferHelper(Account FromAccount, Account ToAccount, decimal Amount)
        {
            if (Amount < FromAccount.Balance || Amount < 0)
                return null;

            FromAccount.Balance -= Amount;
            ToAccount.Balance += Amount;

            int NewTransactionId = await _AddTransactionAsync(TransactionType.Transfer, Amount);
            await _AddEntryAsync(EntryType.Out, NewTransactionId, FromAccount.Id);
            await _AddEntryAsync(EntryType.In, NewTransactionId, ToAccount.Id);

            return NewTransactionId;

        }

        //TODO: we should replace this func with the one exists in AccountRepo
        private async Task<Account?> _GetAccountAsync(string AccountNumber)
        {
            var Account = await _Context.Set<Account>().SingleOrDefaultAsync(x => x.Number == AccountNumber);

            return Account;
        }

        public static TransactionDTO MapToDTO(Transaction transaction)
        {
            return new TransactionDTO()
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Type = transaction.Type,
                Entries = transaction.Entries
            };
        }
    }
}
