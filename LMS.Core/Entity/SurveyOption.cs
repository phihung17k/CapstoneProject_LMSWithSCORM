using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("survey_option")]
    public class SurveyOption
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public int SurveyQuestionId { get; set; }

        [ForeignKey(nameof(SurveyQuestionId))]
        public SurveyQuestion SurveyQuestion { get; set; }
    }
}
