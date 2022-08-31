using LMS.Core.Common;
using LMS.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("quiz")]
    public class Quiz : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? NumberOfAllowedAttempts { get; set; }
        [Required]
        public int NumberOfQuestions { get; set; }
        [Required]
        public int NumberOfActiveQuestions { get; set; }
        [Required]
        public int QuestionsPerPage { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        [Required]
        public DateTimeOffset StartTime { get; set; }
        [Required]
        public DateTimeOffset EndTime { get; set; }
        [Required]
        public GradingMethodType GradingMethod { get; set; }
        [Required]
        public float PassedScore { get; set; }
        [Required]
        public bool ShuffledQuestion { get; set; }
        [Required]
        public bool ShuffledOption { get; set; }
        [Required]
        public int Credit { get; set; }
        [Column(TypeName = "json")]
        public string Restriction { get; set; }
        [Required]
        public int TopicId { get; set; }
        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }
        [InverseProperty(nameof(QuizQuestion.Quiz))]
        public virtual ICollection<QuizQuestion> Questions { get; set; }
        [InverseProperty(nameof(UserQuiz.Quiz))]
        public virtual ICollection<UserQuiz> UserQuizzes { get; set; }
    }
}
