using Microsoft.AspNetCore.Mvc;
using TMS.Application.DTOs.People;
using TMS.Application.Interfaces.People;

namespace TMS.API.Controllers
{
    [Route("api/PeopleApi")]
    [ApiController]
    public class PeopleApiController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PeopleApiController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonDTO>> AddPerson(PersonToAddDTO newPerson)
        {
            if (newPerson is null || string.IsNullOrWhiteSpace(newPerson.FirstName) || string.IsNullOrWhiteSpace(newPerson.LastName) 
                || string.IsNullOrWhiteSpace(newPerson.Email) || newPerson.Age < 18)
            {
                return BadRequest($"البيانات المدخلة غير صحيحة");
            }

            var newId = await _personService.AddAsync(newPerson);

            if (newId is not null)
            {
                return CreatedAtRoute("GetPersonById", new { id = newId }, newPerson);
            }

            return Problem("حدثت مشكلة عند الإتصال بالخادك");
        }

        [HttpPut("UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> UpdatePerson(PersonToUpdateDTO personToUpdate)
        {
            if (personToUpdate is null || string.IsNullOrWhiteSpace(personToUpdate.FirstName) || string.IsNullOrWhiteSpace(personToUpdate.LastName)
                || string.IsNullOrWhiteSpace(personToUpdate.Email) || personToUpdate.Age < 18)
            {
                return BadRequest($"البيانات المدخلة غير صحيحة");
            }

            var result = await _personService.UpdateAsync(personToUpdate);

            return result
                ? Ok("تم تعديل بيانات الشخص بنجاح")
                : Problem("حدثت مشكلة عند الإتصال بالخادك");
        }

        [HttpDelete("DeletePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> DeletePerson(int personId)
        {
            if (personId < 1)
            {
                return BadRequest($"المعرف {personId} خاطئ");
            }

            var result = await _personService.DeleteAsync(personId);

            return result
                ? Ok("تم حذف الشخص بنجاح")
                : Problem("حدثت مشكلة عند الإتصال بالخادك");
        }

        [HttpGet("GetPersonById", Name = "GetPersonById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonDTO>> GetByPersonId(int id)
        {
            if (id < 1)
            {
                return BadRequest($"المعرف {id} خاطئ");
            }

            var personDTO = await _personService.GetByIdAsync(id);

            return personDTO is not null
                ? Ok(personDTO)
                : NotFound("لم يتم العثور على الطالب");
        }

        [HttpGet("GetAllPeople")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PersonDTO>> GetAllPersons()
        {
            return Ok(_personService.GetAllAsync());
        }

    }
}
