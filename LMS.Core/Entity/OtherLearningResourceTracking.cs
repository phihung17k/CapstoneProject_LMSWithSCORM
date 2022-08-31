using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("other_learning_resource_tracking")]
    public class OtherLearningResourceTracking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
        [Required]
        public Guid LearnerId { get; set; }
        [ForeignKey(nameof(LearnerId))]
        public User User { get; set; }
        [Required]
        public int TopicOtherLearningResourceId { get; set; }
        [ForeignKey(nameof(TopicOtherLearningResourceId))]
        public TopicOtherLearningResource TopicOtherLearningResource { get; set; }
    }
}
