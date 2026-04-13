using System;
using TMS.Domain.Entities.Bases;
using TMS.Domain.Entities.People;

namespace TMS.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // TODO: الأفضل أن تضاف
        // TODO: يرث من AuditableEntity
        public int? CreatedByUserId { get; set; }
        public virtual User? CreatedByUser { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; } = null!;
    }
}
