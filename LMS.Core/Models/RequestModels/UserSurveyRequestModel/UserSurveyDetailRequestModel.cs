namespace LMS.Core.Models.RequestModels.UserSurveyRequestModel
{
    public class UserSurveyDetailRequestModel
    {
        public int SurveyQuestionId { get; set; }
        public int? SelectedSurveyOptionId { get; set; }
        //for matrix option
        public int? Order { get; set; }
        public string Feedback { get; set; }
    }
}
