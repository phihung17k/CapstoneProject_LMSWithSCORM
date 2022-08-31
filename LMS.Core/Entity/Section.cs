using LMS.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("section")]
    public class Section : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public int SubjectId { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }

        [InverseProperty(nameof(OtherLearningResource.Section))]
        public IEnumerable<OtherLearningResource> OtherLearningResourceList { get; set; }

        [InverseProperty(nameof(SCORM.Section))]
        public IEnumerable<SCORM> SCORMList { get; set; }
    }
}
