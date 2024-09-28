

namespace HealtSync.Domain.Base
{
    public abstract class ActivatableEntity : AuditableEntity
    {
        public bool IsActive { get; set; }
    }
}
