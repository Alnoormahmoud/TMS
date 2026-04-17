using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.DTOs.TransactionEntries;
using TMS.Application.Interfaces.TransactionEntries;
using TMS.Application.Services.Transactions;
using TMS.Domain.Entities.TransactionEntries;
using TMS.Domain.Enums.Transactions;

namespace TMS.Application.Services.TransactionEntries
{
    public class TransactionEntryService : ITransactionEntryService
    {
        private readonly ITransactionEntryRepository _repo;
        public TransactionEntryService(ITransactionEntryRepository repo)
        {
            _repo = repo;
        }
     
        public async Task<IEnumerable<TransactionEntryDTO>> GetAllAsync(TransactionEntriesFilterDTO dto)
        {

           var Entries = await _repo.GetAllAsync(dto);
           return MapToDTOs(Entries);
        }


        public async Task<TransactionEntryDTO?> GetByIdAsync(int Id)
        {
            var Entry = await _repo.GetByIdAsync(Id);

            return Entry is null
                ? null
                :_MapToDTO(Entry);
        }

        private static TransactionEntryDTO _MapToDTO(TransactionEntry Entry)
        {
            return new TransactionEntryDTO()
            {
                Id = Entry.Id,
                AccountNumber = Entry.Account.Number,
                TransactionID = Entry.TransactionId,
                Amount = Entry.Transaction.Amount,
                EntryType = Entry.EntryType
            };

        }

        public static IEnumerable<TransactionEntryDTO> MapToDTOs(IEnumerable<TransactionEntry> Entries)
        {
            List<TransactionEntryDTO> DTOList = new List<TransactionEntryDTO>();

            foreach (var Entry in Entries)
            {
                DTOList.Add(_MapToDTO(Entry));
            }
            return DTOList;

        }
    }
}
