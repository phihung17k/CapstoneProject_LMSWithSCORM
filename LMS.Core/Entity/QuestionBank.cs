using LMS.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("question_bank")]
    public class QuestionBank : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int NumberOfQuestions { get; set; }
        [Required]
        public int SubjectId { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }
        [InverseProperty(nameof(Question.QuestionBank))]
        public ICollection<Question> Questions { get; set; }
    }
}
