using LMS.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("topic_other_learning_resource")]
    public class TopicOtherLearningResource : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public int OtherLearningResourceId { get; set; }
        [Required]
        public string OtherLearningResourceName { get; set; }
        [Required]
        public float CompletionThreshold { get; set; }

        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }

        [ForeignKey(nameof(OtherLearningResourceId))]
        public OtherLearningResource OtherLearningResource { get; set; }
        [InverseProperty(nameof(OtherLearningResourceTracking.TopicOtherLearningResource))]
        public virtual ICollection<OtherLearningResourceTracking> OLRTrackings { get; set; }
    }
}
