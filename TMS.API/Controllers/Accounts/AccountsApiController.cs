using Microsoft.AspNetCore.Mvc;
using TMS.Application.DTOs.Accounts;
using TMS.Application.Interfaces.Accounts;

namespace TMS.API.Controllers.Accounts
{
    [Route("api/AccountsApi")]
    [ApiController]
    public class AccountsApiController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsApiController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("AddAccount")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountDTO>> AddAccount(AccountToAddDTO accountToAdd)
        {
            if (accountToAdd is null 
                || string.IsNullOrWhiteSpace(accountToAdd.FirstName) 
                || string.IsNullOrWhiteSpace(accountToAdd.LastName) 
                || string.IsNullOrWhiteSpace(accountToAdd.Email)
                || string.IsNullOrWhiteSpace(accountToAdd.Password))
            {
                return BadRequest("يجب تعبئة جميع الحقول المطلوبة");
            }

            if (!string.IsNullOrWhiteSpace(accountToAdd.Phone) && !accountToAdd.Phone.All(ch => char.IsDigit(ch)))
            {
                return BadRequest("يجب أن يحتوي رقم الهاتف على أرقام فقط");
            }

            if (accountToAdd.Password.Length < 8
                || !accountToAdd.Password.Any(ch => char.IsLower(ch))
                || !accountToAdd.Password.Any(ch => char.IsUpper(ch)))
            {
                return BadRequest("يجب أن تتكون كلمة المرور الجديدة من 8 خانات على الأقل و حرف صغير و حرف كبير");
            }

            if (accountToAdd.Password != accountToAdd.ConfirmPassword)
            {
                return BadRequest("كلمات المرور غير متطابقة");
            }

            var newId = await _accountService
                .AddAsync(accountToAdd);

            var createdAccount = await _accountService
                .GetByIdAsync(newId);

            return createdAccount is null
                ? Problem("حدثت مشكلة عند الإتصال بالخادم أثناء إنشاء الحساب")
                : CreatedAtRoute("GetAccountById", new { id = newId }, createdAccount);
        }

        [HttpPut("UpdateAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAccount(AccountToUpdateDTO accountToUpdate)
        {
            if (accountToUpdate is null
                || string.IsNullOrWhiteSpace(accountToUpdate.FirstName)
                || string.IsNullOrWhiteSpace(accountToUpdate.LastName)
                || string.IsNullOrWhiteSpace(accountToUpdate.Email))
            {
                return BadRequest("يجب تعبئة جميع الحقول المطلوبة");
            }

            if (accountToUpdate.Id < 1)
            {
                return BadRequest($"المعرف {accountToUpdate.Id} غير صحيح");
            }

            if (!string.IsNullOrWhiteSpace(accountToUpdate.Phone) && !accountToUpdate.Phone.All(ch => char.IsDigit(ch)))
            {
                return BadRequest("يجب أن يحتوي رقم الهاتف على أرقام فقط");
            }

            var result = await _accountService
                .UpdateAsync(accountToUpdate);

            return result
                ? Ok("تم تعديل بيانات الحساب بنجاح")
                : Problem("فشل تعديل الحساب، قد يكون الحساب غير موجود أو مرتبط بعمليات أخرى");
        }

        [HttpDelete("DeleteAccount/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            if (id < 1)
            {
                return BadRequest($"المعرف {id} غير صحيح");
            }

            var result = await _accountService
                .DeleteAsync(id);

            return result
                ? Ok("تم حذف الحساب بنجاح")
                : Problem("فشل حذف الحساب، قد يكون الحساب غير موجود");
        }

        [HttpGet("GetAccountById/{id}", Name = "GetAccountById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountDTO>> GetAccountById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"المعرف {id} غير صحيح");
            }

            var account = await _accountService
                .GetByIdAsync(id);

            return account is null
                ? NotFound("لم يتم العثور على الحساب المطلوب")
                : Ok(account);
        }

        [HttpGet("GetAllAccounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAllAccounts()
        {
            var accounts = await _accountService
                .GetAllAsync();

            return Ok(accounts);
        }

        [HttpPut("ActivateAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ActivateAccount(int id, bool activate)
        {
            if (id < 1)
            {
                return BadRequest($"المعرف {id} غير صحيح");
            }

            var result = await _accountService
                .ActivateAsync(id, activate);

            return result
                ? Ok(activate ? "تم تنشيط الحساب" : "تم إلغاء تنشيط الحساب")
                : Problem("فشل تغيير حالة الحساب، قد يكون الحساب غير موجود");
        }

        [HttpPut("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangePassword(AccountToChangePasswordDTO changePasswordDto)
        {
            if (changePasswordDto is null 
                || string.IsNullOrWhiteSpace(changePasswordDto.NewPassword) 
                || string.IsNullOrWhiteSpace(changePasswordDto.ConfirmPassword))
            {
                return BadRequest("يجب تعبئة جميع الحقول المطلوبة");
            }

            if (changePasswordDto.NewPassword.Length < 8 
                || !changePasswordDto.NewPassword.Any(ch => char.IsLower(ch))
                || !changePasswordDto.NewPassword.Any(ch => char.IsUpper(ch)))
            {
                return BadRequest("يجب أن تتكون كلمة المرور الجديدة من 8 خانات على الأقل و حرف صغير و حرف كبير");
            }

            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
            {
                return BadRequest("كلمات المرور الجديدة غير متطابقة");
            }

            var result = await _accountService.ChangePasswordAsync(
                changePasswordDto.Id,
                changePasswordDto.NewPassword,
                changePasswordDto.ConfirmPassword
            );

            return result
                ? Ok("تم تغيير كلمة المرور بنجاح")
                : Problem("فشل تغيير كلمة المرور، قد يكون الحساب غير موجود");
        }

    }
}