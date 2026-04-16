using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Entities.People;
using TMS.Domain.Entities.Users;

namespace TMS.Application.DTOs.Users
{
    public class    UserToAddDTO
    {           
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Person.")]
        public int PersonId { get; set; }


    }
}
