using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Entities.People;
using TMS.Domain.Entities.Users;

namespace TMS.Application.DTOs.Users
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }  
        public int CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; } = string.Empty;  // Flattened from CreatedByUser.UserName
        public int PersonId { get; set; } 
        public string PersonFullName { get; set; } = string.Empty; // Useful for UI display

     }
}
