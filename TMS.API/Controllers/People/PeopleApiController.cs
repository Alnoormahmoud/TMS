using Microsoft.AspNetCore.Mvc;
using TMS.Application.DTOs.People;
using TMS.Application.Interfaces.People;

namespace TMS.API.Controllers.People
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
        public async Task<ActionResult<PersonDTO>> AddPerson(PersonToAddDTO personToAdd)
        {
            if (personToAdd is null || string.IsNullOrWhiteSpace(personToAdd.FirstName) || string.IsNullOrWhiteSpace(personToAdd.LastName) 
                || string.IsNullOrWhiteSpace(personToAdd.Email))
            {
                return BadRequest($"البيانات المدخلة غير صحيحة");
            }

            var newId = await _personService.AddAsync(personToAdd);
            var created = await _personService.GetByIdAsync(newId);

            return created is null
                ? Problem("حدثت مشكلة عند الإتصال بالخادك")
                : CreatedAtRoute("GetPersonById", new { id = newId }, created);
        }

        [HttpPut("UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonDTO>> UpdatePerson(PersonToUpdateDTO personToUpdate)
        {
            if (personToUpdate is null || string.IsNullOrWhiteSpace(personToUpdate.FirstName) || string.IsNullOrWhiteSpace(personToUpdate.LastName)
                || string.IsNullOrWhiteSpace(personToUpdate.Email))
            {
                return BadRequest($"البيانات المدخلة غير صحيحة");
            }

            var result = await _personService.UpdateAsync(personToUpdate);

            return result
                ? Ok("تم تعديل بيانات الشخص بنجاح")
                : Problem("حدثت مشكلة عند الإتصال بالخادك");
        }

        [HttpDelete("DeletePerson/{id}")]
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

        [HttpGet("GetPersonById/{id}", Name = "GetPersonById")]
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

            return personDTO is null
                ? NotFound("لم يتم العثور على الشخص")
                : Ok(personDTO);
        }

        [HttpGet("GetAllPeople")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetAllPersons()
        {
            var result = await _personService.GetAllAsync();
            return Ok(result);
        }

    }
}
