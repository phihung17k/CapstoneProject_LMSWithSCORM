using LMS.Core.Common;
using LMS.Core.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("question")]
    public class Question : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public QuestionType Type { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public int QuestionBankId { get; set; }

        [ForeignKey(nameof(QuestionBankId))]
        public QuestionBank QuestionBank { get; set; }

        [InverseProperty(nameof(Option.Question))]
        public ICollection<Option> Options { get; set; }
        [InverseProperty(nameof(QuizQuestion.Question))]
        public ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
