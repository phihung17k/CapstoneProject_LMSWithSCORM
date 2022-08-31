using LMS.Core.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("quiz_attempt")]
    public class QuizAttempt
    {
        [Key]
        public long Id { get; set; }
        public string BackgroundJobId { get; set; }
        [Required]
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset? FinishAt { get; set; }
        [Required]
        public DateTimeOffset EstimatedFinishTime { get; set; }
        [Required]
        public float Mark { get; set; }
        [Required]
        public int NumberOfQuestions { get; set; }
        [Required]
        public float Score { get; set; }
        [Required]
        public CompletionLevelType Status { get; set; }

        [Column(TypeName = "json")]
        public string AnswerHistory { get; set; }
        [Required]
        public long UserQuizId { get; set; }

        [ForeignKey(nameof(UserQuizId))]
        public UserQuiz UserQuiz { get; set; }
    }
}
