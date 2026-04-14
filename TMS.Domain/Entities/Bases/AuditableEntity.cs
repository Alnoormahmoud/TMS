using System;
using TMS.Domain.Entities.Users;

namespace TMS.Domain.Entities.Bases
{
    public class AuditableEntity : BaseEntity
    {
        public int CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
