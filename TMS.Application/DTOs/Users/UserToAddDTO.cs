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
        // now person is represented by the PersonId foreign key, so we need to take it from the dto and set it to the user entity when we create it, but we can also take the person info from the dto and create a new person entity and set it to the user entity, but for now we will just take the PersonId from the dto and set it to the user entity, later we can add the person info to the dto and create a new person entity and set it to the user entity

        public int PersonId { get; set; }  // this is the foreign key to the Person entity, we will take it from the dto and set it to the user entity when we create it, but we can also take the person info from the dto and create a new person entity and set it to the user entity, but for now we will just take the PersonId from the dto and set it to the user entity, later we can add the person info to the dto and create a new person entity and set it to the user entity

        // next Add Person Info manually to the dto, we will take the person info from the dto and create a new person entity and set it to the user entity, but for now we will just take the PersonId from the dto and set it to the user entity, later we can add the person info to the dto and create a new person entity and set it to the user entity



        // Add User Info

        public string UserName { get; set; } = string.Empty;
 
        public string Password { get; set; } = string.Empty;
 
    


    }
}
