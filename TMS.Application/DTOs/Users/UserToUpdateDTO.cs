using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Entities.People;
using TMS.Domain.Entities.Users;

namespace TMS.Application.DTOs.Users
{
    public class UserToUpdateDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;  
 
    }
}
