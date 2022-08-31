using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("topic_tracking")]
    public class TopicTracking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
        [Required]
        public int CompletedLearningResourses { get; set; }
        [Required]
        public int CompletedQuizzes { get; set; }
        [Required]
        public int CompletedSurveys { get; set; }
        //public int UserCourseId { get; set; }
        //[ForeignKey(nameof(UserCourseId))]
        //public UserCourse UserCourse { get; set; }
        [Required]
        public int TopicId { get; set; }
        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
