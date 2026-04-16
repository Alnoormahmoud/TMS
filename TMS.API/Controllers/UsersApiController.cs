using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
 using TMS.Application.DTOs.Users;
using TMS.Application.Interfaces.Users;
using TMS.Domain.Entities.Users;

namespace TMS.API.Controllers
{
    [Route("api/UsersApi")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UsersApiController(IUserService UserService)
        {
            _UserService = UserService;
        }

        [HttpPost("AddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> AddUser(UserToAddDTO userToAdd)
        {
            if (userToAdd is null || string.IsNullOrWhiteSpace(userToAdd.UserName)
                || string.IsNullOrWhiteSpace(userToAdd.Password))

            {
                return BadRequest($"البيانات المدخلة غير صحيحة");
            }

            var newId = await _UserService.AddAsync(userToAdd);
            var created = await _UserService.GetByIdAsync(newId);

            return created is null ? Problem("حدثت مشكلة عند الإتصال بالخادم")
                : CreatedAtRoute("GetUserById", new { id = newId }, created);
        }

        [HttpPut("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> UpdateUser(UserToUpdateDTO usertoupdate)
        {
            if (usertoupdate is null || string.IsNullOrWhiteSpace(usertoupdate.UserName) || string.IsNullOrWhiteSpace(usertoupdate.Password))
            {
                return BadRequest($"البيانات المدخلة غير صحيحة");
            }

            var result = await _UserService.UpdateAsync(usertoupdate);

            return result ? Ok("تم تعديل بيانات المستخدم بنجاح") : Problem("حدثت مشكلة عند الإتصال بالخادك");
        }

        [HttpDelete("DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> DeleteUser(int userid)
        {
            if (userid < 1)
            {
                return BadRequest($"المعرف {userid} خاطئ");
            }

            var result = await _UserService.DeleteAsync(userid);

            return result ? Ok("تم حذف المستخدم بنجاح") : Problem("حدثت مشكلة عند الإتصال بالخادك");
        }

        [HttpGet("GetUserById", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetByUserId(int id)
        {
            if (id < 1)
            {
                return BadRequest($"المعرف {id} خاطئ");
            }

            var userDTO = await _UserService.GetByIdAsync(id);

            return userDTO is null ? NotFound("لم يتم العثور على المستخدم")
                : Ok(userDTO);
        }

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var result = await _UserService.GetAllAsync();
            return Ok(result);
        }

        [HttpPut("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangePassword(UserToChangePasswordDTO changePasswordDto)
        {
            if (changePasswordDto is null || string.IsNullOrWhiteSpace(changePasswordDto.NewPassword) || string.IsNullOrWhiteSpace(changePasswordDto.ConfirmPassword))
            {
                return BadRequest("يجب تعبئة جميع الحقول المطلوبة");
            }

            if (changePasswordDto.NewPassword.Length < 3)
            {
                return BadRequest("يجب أن تتكون كلمة المرور الجديدة من 3 خانات على الأقل");
            }

            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
            {
                return BadRequest("كلمات المرور الجديدة غير متطابقة");
            }

            if (changePasswordDto.Id < 1)
            {
                return BadRequest("معرف المستخدم غير صالح");
            }

            if (string.IsNullOrWhiteSpace(changePasswordDto.OldPassword))
            {
                return BadRequest("كلمة المرور القديمة مطلوبة");
            }

            if (changePasswordDto.OldPassword == changePasswordDto.NewPassword)
            {
                return BadRequest("كلمة المرور الجديدة لا يمكن أن تكون نفسها القديمة");
            }

            var result = await _UserService.ChangePasswordAsync(changePasswordDto.Id,changePasswordDto.OldPassword, changePasswordDto.NewPassword, changePasswordDto.ConfirmPassword);

            return result ? Ok("تم تغيير كلمة المرور بنجاح") : Problem("فشل تغيير كلمة المرور، قد يكون الحساب غير موجود أو كلمة المرور القديمة غير صحيحة");
        }
        [HttpPost("LogInUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<User>> LogInUser(UserToLogInDTO loginDto)
        {
            if (loginDto is null || string.IsNullOrWhiteSpace(loginDto.UserName)|| string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest("يجب تعبئة جميع الحقول المطلوبة");
            }

      
            var userDTO = await _UserService.LogInAsync(loginDto);

            return userDTO is null ? Unauthorized(new { message = "اسم المستخدم أو كلمة المرور غير صحيحة" }): Ok(new { message = "تم تسجيل الدخول بنجاح", data = userDTO });

         }
    }
}

