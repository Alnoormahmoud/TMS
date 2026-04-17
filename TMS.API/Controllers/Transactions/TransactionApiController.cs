using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.DTOs.People;
using TMS.Application.DTOs.Transactions;
using TMS.Application.Interfaces.Transactions;
using TMS.Application.Services.Transactions;
using TMS.Domain.Entities.Transactions;

namespace TMS.API.Controllers.Transactions
{
    [Route("api/TransactionsApi")]
    [ApiController]
    public class TransactionApiController : ControllerBase
    {
        private readonly ITransactionService _TransactionService;

        public TransactionApiController(ITransactionService TransactionService)
        {
            _TransactionService = TransactionService;
        }


        [HttpGet("{id}", Name = "GetTransactionById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TransactionDTO>> GetTransactionById(int id)
        {
            if (id < 0)
            {
                return BadRequest($"المعرف {id} خاطئ");
            }
            var TransactionDTO = await _TransactionService.GetByIdAsync(id);

           return TransactionDTO is null
                ? NotFound("لم يتم العثور على العملية")
                : Ok(TransactionDTO);

        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetAllTransactions()
        {

            var result = await _TransactionService.GetAllAsync();

            return Ok(result);

        }

        [HttpPost("Deposit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TransactionDTO>> AddDeposit(DepositWithdrawDTO dto)
        {
            

            int? NewId = await _TransactionService.DepositAsync(dto);
            if (NewId is null)
            {
                return BadRequest($"البيانات المدخلة غير صحيحة");
            }

            var Created = await _TransactionService.GetByIdAsync((int)NewId);

            return Created is null
                 ? Problem("حدثت مشكلة عند الإتصال بالخادم")
                : CreatedAtRoute("GetTransactionById", new { id = NewId }, Created);

        }

        [HttpPost("Withdraw")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TransactionDTO>> AddWithdraw(DepositWithdrawDTO dto)
        {
            int? NewId = await _TransactionService.WithdrawAsync(dto);
            if (NewId is null)
            {
                return BadRequest($"البيانات المدخلة غير صحيحة");
            }

            var Created = await _TransactionService.GetByIdAsync((int)NewId);

            return Created is null
                 ? Problem("حدثت مشكلة عند الإتصال بالخادم")
                : CreatedAtRoute("GetTransactionById", new { id = NewId }, Created);
        }

        [HttpPost("Transfer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TransactionDTO>> AddTransfer(TransferDTO dto)
        {
            int? NewId = await _TransactionService.TransferAsync(dto);
            if (NewId is null)
            {
                return BadRequest($"البيانات المدخلة غير صحيحة");
            }

            var Created = await _TransactionService.GetByIdAsync((int)NewId);

            return Created is null
                 ? Problem("حدثت مشكلة عند الإتصال بالخادم")
                : CreatedAtRoute("GetTransactionById", new { id = NewId }, Created);
        }
    }
}
