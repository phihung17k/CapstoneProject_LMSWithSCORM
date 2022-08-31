using LMS.Core.Common;
using LMS.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("course")]
    public class Course : AuditableEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string ParentCode { get; set; }
        [Required]
        public string ParentName { get; set; }
        [Required]
        public CourseType Type { get; set; }
        [Required]
        public int NumberOfTrainee { get; set; }

        public string Description { get; set; }
        [Required]
        public DateTimeOffset StartTime { get; set; }
        [Required]
        public DateTimeOffset EndTime { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public int SubjectId { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }

        [InverseProperty(nameof(Topic.Course))]
        public virtual ICollection<Topic> Topics { get; set; }

        [InverseProperty(nameof(UserCourse.Course))]
        public ICollection<UserCourse> Users { get; set; }

    }
}
