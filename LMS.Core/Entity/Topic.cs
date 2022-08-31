using LMS.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("topic")]
    public class Topic : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int NumberOfLearningResources { get; set; }
        [Required]
        public int NumberOfQuizzes { get; set; }
        [Required]
        public int NumberOfSurveys { get; set; }
        [Required]
        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        [InverseProperty(nameof(TopicSCORM.Topic))]
        public virtual List<TopicSCORM> TopicSCORMs { get; set; }

        [InverseProperty(nameof(TopicOtherLearningResource.Topic))]
        public virtual List<TopicOtherLearningResource> TopicOtherLearningResources { get; set; }
        [InverseProperty(nameof(Survey.Topic))]
        public virtual ICollection<Survey> Surveys { get; set; }
        [InverseProperty(nameof(Quiz.Topic))]
        public virtual ICollection<Quiz> Quizzes { get; set; }
        [InverseProperty(nameof(TopicTracking.Topic))]
        public virtual ICollection<TopicTracking> TopicTrackings { get; set; }
    }
}
