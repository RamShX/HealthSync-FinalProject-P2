﻿using HealtSync.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealtSync.Domain.Entities.Medical
{
    [Table("Specialties", Schema = "medical")]
    public class Specialties : AuditableEntity, IActivatableEntity
    {
        [Key]
        public int SpecialtyID { get; set; }
        public string? SpecialtyName { get; set; }
        public bool IsActive { get; set; }
    }
}
