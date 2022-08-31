using LMS.Core.Common;
using LMS.Core.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("other_learning_resource")]
    public class OtherLearningResource : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string PathToFile { get; set; }
        [Required]
        public LearningResourceType Type { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public int? SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public Section Section { get; set; }


        [InverseProperty(nameof(TopicOtherLearningResource.OtherLearningResource))]
        public virtual ICollection<TopicOtherLearningResource> TopicOtherLearningResources { get; set; }
    }
}
