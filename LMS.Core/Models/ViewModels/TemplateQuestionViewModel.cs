using System.Collections.Generic;
using LMS.Core.Enum;

namespace LMS.Core.Models.ViewModels
{
    public class TemplateQuestionViewModel
    {
        public int Id { get; set; }
        public SurveyQuestionType Type { get; set; }
        public string Name { get; set; }
        public List<TemplateMultipleChoiceOptionViewModel> Choices { get; set; } =
            new List<TemplateMultipleChoiceOptionViewModel>();
        public List<TemplateMatrixOptionViewModel> Columns { get; set; } = new List<TemplateMatrixOptionViewModel>();
        public List<TemplateMatrixQuestionViewModel> Rows { get; set; } = new List<TemplateMatrixQuestionViewModel>();
    }

    public class TemplateMatrixQuestionViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}