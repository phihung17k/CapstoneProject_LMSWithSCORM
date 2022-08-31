using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("quiz_question")]
    public class QuizQuestion
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public int QuizId { get; set; }

        [ForeignKey(nameof(QuizId))]
        public Quiz Quiz { get; set; }
        [Required]
        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }

    }
}
