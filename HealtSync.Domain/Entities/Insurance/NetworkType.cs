using HealtSync.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HealtSync.Domain.Entities.Insurance
{
    [Table("NetworkType", Schema = "Insurance")]
    public sealed class NetworkType : AuditableEntity, IActivatableEntity
    {
        [Key]
        public int NetworkTypeld { get; set; }// Primarary ID
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get;  set; }
    }
}
