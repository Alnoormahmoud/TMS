using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.DTOs.TransactionEntries;
using TMS.Application.DTOs.Transactions;
using TMS.Application.Interfaces.TransactionEntries;
using TMS.Application.Interfaces.Transactions;

namespace TMS.API.Controllers.TransactionEntries
{
    [Route("api/TransactionEntriesApi")]
    [ApiController]
    public class TransactionEntriesApiController : ControllerBase
    {
        private readonly ITransactionEntryService _EntryService;

        public TransactionEntriesApiController(ITransactionEntryService EntryService)
        {
            _EntryService = EntryService;
        }


        [HttpGet("{id}", Name = "GetTransactionEntryById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TransactionEntryDTO>> GetTransactionEntryById(int id)
        {
            if (id < 0)
            {
                return BadRequest($"المعرف {id} خاطئ");
            }
            var TransactionDTO = await _EntryService.GetByIdAsync(id);

            return TransactionDTO is null
                 ? NotFound("لم يتم العثور على العملية")
                 : Ok(TransactionDTO);

        }
      

        [HttpGet("GetAll", Name = "GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TransactionEntryDTO>>> GetAll([FromQuery]TransactionEntriesFilterDTO dto)
        {

            var result = await _EntryService.GetAllAsync(dto);

            return Ok(result);

        }
    }
}
