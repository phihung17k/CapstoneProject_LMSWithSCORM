using LMS.Core.Common;
using LMS.Core.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("subject")]
    public class Subject : AuditableEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }
        [Required]
        public int PassScore { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(Course.Subject))]
        public ICollection<Course> Courses { get; set; }

        [InverseProperty(nameof(UserSubject.Subject))]
        public ICollection<UserSubject> Users { get; set; }
        [Required]
        public SubjectType Type { get; set; }
        [InverseProperty(nameof(QuestionBank.Subject))]
        public ICollection<QuestionBank> QuestionBanks { get; set; }

        [InverseProperty(nameof(Section.Subject))]
        public virtual ICollection<Section> Sections { get; set; }
    }
}
