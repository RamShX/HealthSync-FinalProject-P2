using HealtSync.Domain.Base;

namespace HealtSync.Domain.Entities.Medical
{
    public class Specialties: ActivatableEntity
    {
        public int SpecialtyID { get; set; }
        public string? SpecialtyName { get; set; }
    }
}
