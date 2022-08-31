using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("template_option")]
    public class TemplateOption
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public int TemplateQuestionId { get; set; }

        [ForeignKey(nameof(TemplateQuestionId))]
        public TemplateQuestion TemplateQuestion { get; set; }
    }
}
