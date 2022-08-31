using LMS.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("user_quiz")]
    public class UserQuiz
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [Required]
        public int QuizId { get; set; }

        [ForeignKey(nameof(QuizId))]
        public Quiz Quiz { get; set; }
        [Required]
        public float FinalScore { get; set; }
        [Required]
        public CompletionLevelType Status { get; set; }
        [InverseProperty(nameof(QuizAttempt.UserQuiz))]
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }
    }
}
