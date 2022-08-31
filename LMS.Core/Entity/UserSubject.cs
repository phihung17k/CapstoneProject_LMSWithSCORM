using LMS.Core.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("user_subject")]
    public class UserSubject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [Required]
        public int SubjectId { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }
    }
}
