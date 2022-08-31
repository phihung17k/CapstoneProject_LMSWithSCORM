using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("permission_role")]
    public class PermissionRole
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PermissionId { get; set; }
        [Required]
        public int RoleId { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public virtual Permission Permission { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }

    }
}
