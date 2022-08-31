using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("user_survey_detail")]
    public class UserSurveyDetail
    {
        [Key]
        public int Id { get; set; }

        //store feedback for input field question
        public string Feedback { get; set; }
        [Required]
        public int UserSurveyId { get; set; }

        [ForeignKey(nameof(UserSurveyId))]
        public UserSurvey UserSurvey { get; set; }
        [Required]
        public int SurveyQuestionId { get; set; }

        [ForeignKey(nameof(SurveyQuestionId))]
        public SurveyQuestion SurveyQuestion { get; set; }

        public int? SelectedSurveyOptionId { get; set; }

        [ForeignKey(nameof(SelectedSurveyOptionId))]
        public SurveyOption SurveyOption { get; set; }
    }
}
