using LMS.Core.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("role_user")]
    public class RoleUser : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
