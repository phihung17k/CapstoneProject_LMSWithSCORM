using LMS.Core.Common;
using LMS.Core.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("survey_question")]
    public class SurveyQuestion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public string BelongTopic { get; set; }
        [Required]
        public SurveyQuestionType Type { get; set; }
        [Required]
        public int SurveyId { get; set; }

        [ForeignKey(nameof(SurveyId))]
        public Survey Survey { get; set; }

        [InverseProperty(nameof(SurveyOption.SurveyQuestion))]
        public ICollection<SurveyOption> SurveyOptions { get; set; }
    }
}
