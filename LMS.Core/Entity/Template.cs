using LMS.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("template")]
    public class Template : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }

        [InverseProperty(nameof(TemplateQuestion.Template))]
        public ICollection<TemplateQuestion> TemplateQuestions { get; set; }
    }
}
