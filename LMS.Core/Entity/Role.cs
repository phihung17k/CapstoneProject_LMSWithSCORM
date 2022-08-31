using LMS.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("role")]
    public class Role : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public string Description { get; set; }

        [InverseProperty(nameof(PermissionRole.Role))]
        public virtual ICollection<PermissionRole> Permissions { get; set; }

        [InverseProperty(nameof(RoleUser.Role))]
        public virtual ICollection<RoleUser> Users { get; set; }
    }
}
