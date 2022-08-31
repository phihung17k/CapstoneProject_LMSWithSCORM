using LMS.Core.Common;
using LMS.Core.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("template_question")]
    public class TemplateQuestion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public string BelongTopic { get; set; }
        [Required]
        public SurveyQuestionType Type { get; set; }
        [Required]
        public int TemplateId { get; set; }

        [ForeignKey(nameof(TemplateId))]
        public Template Template { get; set; }

        [InverseProperty(nameof(TemplateOption.TemplateQuestion))]
        public ICollection<TemplateOption> TemplateOptions { get; set; }
    }
}
