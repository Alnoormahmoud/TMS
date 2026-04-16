using Microsoft.AspNetCore.Mvc;
 using TMS.Application.DTOs.Users;
using TMS.Application.Interfaces.Users;

namespace TMS.API.Controllers
{
    [Route("api/UsersApi")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UsersApiController(IUserService  UserService)
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

    }
}
