using System.Collections.Generic;
using LMS.Core.Enum;

namespace LMS.Core.Models.ViewModels
{
    public class SurveyQuestionViewModel
    {
        public int Id { get; set; }
        public SurveyQuestionType Type { get; set; }
        public string Name { get; set; }
        public List<SurveyMultipleChoiceOptionViewModel> Choices { get; set; } = 
            new List<SurveyMultipleChoiceOptionViewModel>();
        public List<SurveyMatrixOptionViewModel> Columns { get; set; } = new List<SurveyMatrixOptionViewModel>();
        public List<SurveyMatrixQuestionViewModel> Rows { get; set; } = new List<SurveyMatrixQuestionViewModel>();
    }

    public class SurveyMatrixQuestionViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}