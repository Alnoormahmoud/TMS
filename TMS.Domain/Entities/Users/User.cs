using System;
using TMS.Domain.Entities.Bases;
using TMS.Domain.Entities.People;

namespace TMS.Domain.Entities.Users
{
    public class User : AuditableEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        // TODO: الأفضل أن تضاف
        // TODO: يرث من AuditableEntity
        // inherted from AuditableEntity which has CreatedByUserId and CreatedByUser and UserId and CreatedAt
        // public int? CreatedByUserId { get; set; }
        // public virtual User? CreatedByUser { get; set; }
        // public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int PersonId { get; set; }
        public virtual Person Person { get; set; } = null!;
    }
}
