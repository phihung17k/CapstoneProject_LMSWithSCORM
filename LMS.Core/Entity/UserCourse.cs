using LMS.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("user_course")]
    public class UserCourse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [Required]
        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
        [Required]
        public ActionType ActionType { get; set; }
        public DateTimeOffset? FinishTime { get; set; }
        [Required]
        public float FinalScore { get; set; }
        [Required]
        public LearningStatus LearningStatus { get; set; }
        //public int CompletedTopics { get; set; }
        //[InverseProperty(nameof(TopicTracking.UserCourse))]
        //public virtual ICollection<TopicTracking> TopicTrackings { get; set; }
    }
}
