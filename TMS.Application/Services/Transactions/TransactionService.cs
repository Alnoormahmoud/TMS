using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TMS.Application.DTOs.Accounts;
using TMS.Application.DTOs.TransactionEntries;
using TMS.Application.DTOs.Transactions;
using TMS.Application.Interfaces.Accounts;
using TMS.Application.Interfaces.TransactionEntries;
using TMS.Application.Interfaces.Transactions;
using TMS.Application.Services.Accounts;
using TMS.Application.Services.TransactionEntries;
using TMS.Domain.Entities.Accounts;
using TMS.Domain.Entities.Transactions;
using TMS.Domain.Enums.TransactionEntries;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _TransactionRepo;

        private readonly ITransactionEntryRepository _EntryRepo;

        private readonly IAccountService _AccountService;
        public TransactionService(ITransactionRepository TransactionRepo, ITransactionEntryRepository EntryRepo
            , IAccountService AccountService )
        {
            _TransactionRepo = TransactionRepo;
            _EntryRepo = EntryRepo;
            _AccountService = AccountService;
        }

       
        public async Task<IEnumerable<TransactionDTO>> GetAllAsync()
        {
            List<TransactionDTO> DTOList = new List<TransactionDTO>();
            var transactions = await _TransactionRepo.GetAllAsync();

            foreach (var transaction in transactions)
            {
                DTOList.Add(MapToDTO(transaction));
            }

            return DTOList;
        }

        public async Task<TransactionDTO?> GetByIdAsync(int Id)
        {
            var transaction = await _TransactionRepo.GetByIdAsync(Id);
            return transaction is null
                ? null : MapToDTO(transaction);
        }


        public async Task<int?> TransferAsync(TransferDTO dto)
        {

            int? NewTransactionId = null;

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    var FromAccountDTO = await _AccountService.GetByNumberAsync(dto.FromAccountNumber);
                    if (FromAccountDTO is null)
                    {
                        return null;// Error invalid accountNumber
                    }

                    var ToAccountDTO = await _AccountService.GetByNumberAsync(dto.ToAccountNumber);
                    if (ToAccountDTO is null)
                    {
                        return null;// Error invalid accountNumber
                    }


                    NewTransactionId = await _TransferHelper(FromAccountDTO, ToAccountDTO, dto.Amount);


                    transaction.Complete();

                }
                catch (Exception)
                {
                    return null;
                }
            }

           

            return NewTransactionId;
        }

        public async Task<int?> WithdrawAsync(DepositWithdrawDTO dto)
        {
            int? NewTransactionId = null;

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    var AccountDTO = await _AccountService.GetByNumberAsync(dto.AccountNumber);
                    if (AccountDTO is null)
                    {
                        return null;// Error invalid accountNumber
                    }

                    NewTransactionId = await _WithdrawHelperAsync(AccountDTO, dto.Amount);
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
            }
                
            return NewTransactionId;
        }

        public async Task<int?> DepositAsync(DepositWithdrawDTO dto)
        {
            int? NewTransactionId = null;

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    var AccountDTO = await _AccountService.GetByNumberAsync(dto.AccountNumber);
                    if (AccountDTO is null)
                    {
                        return null;// Error invalid accountNumber
                    }

                    NewTransactionId = await _DepositHelperAsync(AccountDTO, dto.Amount);

                    if (NewTransactionId is null)
                    {
                        return null;// Error Amount < 0
                    }
                    transaction.Complete();

                }
                catch (Exception)
                {
                    return null;
                }
            }

           

            return NewTransactionId;
        }


        private async Task<int?> _DepositHelperAsync(AccountDTO Account, decimal Amount)
        {
            if (Amount < 0)
                return null;

            Account.Balance += Amount;
            //TODO: update Balance
            //await _AccountService.UpdateAsync()

            int NewTransactionId = await _TransactionRepo.AddAsync(TransactionType.Deposit, Amount);

            await _EntryRepo.AddEntryAsync(EntryType.In, NewTransactionId, Account.Id);

            return NewTransactionId;
        }

        private async Task<int?> _WithdrawHelperAsync(AccountDTO Account, decimal Amount)
        {
            if (Account.Balance < Amount || Amount < 0)
                return null;

            Account.Balance -= Amount;
            //TODO: update Balance
           //await _AccountService.UpdateAsync()

            int NewTransactionId = await _TransactionRepo.AddAsync(TransactionType.Withdrawal, Amount);

            await _EntryRepo.AddEntryAsync(EntryType.Out, NewTransactionId, Account.Id);

            return NewTransactionId;
        }

        private async Task<int?> _TransferHelper(AccountDTO FromAccount, AccountDTO ToAccount, decimal Amount)
        {
            if (Amount > FromAccount.Balance || Amount < 0)
                return null;

            FromAccount.Balance -= Amount;
            //TODO: update Balance
            //await _AccountService.UpdateAsync()
            ToAccount.Balance += Amount;
            //TODO: update Balance
            //await _AccountService.UpdateAsync()

            int NewTransactionId = await _TransactionRepo.AddAsync(TransactionType.Transfer, Amount);
            await _EntryRepo.AddEntryAsync(EntryType.Out, NewTransactionId, FromAccount.Id);
            await _EntryRepo.AddEntryAsync(EntryType.In, NewTransactionId, ToAccount.Id);

            return NewTransactionId;

        }

      
        public static TransactionDTO MapToDTO(Domain.Entities.Transactions.Transaction transaction)
        {

            return new TransactionDTO()
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Type = transaction.Type,
                Entries = TransactionEntryService.MapToDTOs(transaction.Entries.AsEnumerable())
            };
        }
    }
}
