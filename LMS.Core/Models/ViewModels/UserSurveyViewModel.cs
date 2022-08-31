using LMS.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Models.ViewModels
{
    public class UserSurveyViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset SubmitTime { get; set; }
        public List<UserSurveyDetailViewModel> Elements { get; set; } = new List<UserSurveyDetailViewModel>();
    }

    public class UserSurveyDetailViewModel
    {
        //use for user survey detail has survey question type = Multiple Choice and Input Field
        public int? UserSurveyDetailId { get; set; }
        //use for user survey detail has survey question type = Multiple Choice and Input Field
        public int SurveyQuestionId { get; set; }
        public SurveyQuestionType Type { get; set; }
        public string Name { get; set; }
        public List<SurveyMultipleChoiceOptionViewModel> Choices { get; set; } =
            new List<SurveyMultipleChoiceOptionViewModel>();
        public List<SurveyMatrixOptionViewModel> Columns { get; set; } = new List<SurveyMatrixOptionViewModel>();
        public List<UserSurveyMatrixQuestionViewModel> Rows { get; set; } = new List<UserSurveyMatrixQuestionViewModel>();

        //use for type question: multiple choice
        public int? SelectedSurveyOptionId { get; set; }

        //use for type question: input field
        public string Feedback { get; set; }
    }

    public class UserSurveyMatrixQuestionViewModel
    {
        //use for user survey detail has survey question type = Matrix
        public int UserSurveyDetailId { get; set; }
        //use for user survey detail has survey question type = Matrix
        public int SurveyQuestionId { get; set; }
        public string Content { get; set; }

        //use for type question: matrix question
        public int? SelectedSurveyOptionId { get; set; }

        public int? SelectedOrder { get; set; }
    }

    public class SubmitSurveyViewModel
    {
        public UserSurveyViewModel SurveyResult { get; set; }
        public TopicTrackingViewModel TopicTracking { get; set; }
    }

    public class SurveyTrackingViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset SubmitTime { get; set; }
        public Guid UserId { get; set; }
    }
}
