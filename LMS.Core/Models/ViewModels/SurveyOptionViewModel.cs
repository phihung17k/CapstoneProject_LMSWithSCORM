namespace LMS.Core.Models.ViewModels
{
    public class SurveyOptionViewModel
    {
        public string Content { get; set; }
    }

    public class SurveyMultipleChoiceOptionViewModel : SurveyOptionViewModel
    {
        public int Id { get; set; }
    }

    public class SurveyMatrixOptionViewModel : SurveyOptionViewModel
    {
        public int Order { get; set; }
    }
}