using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class SurveyViewModel : SurveyViewModelWithoutQuestions
    {
        public List<SurveyQuestionViewModel> Elements { get; set; } = new List<SurveyQuestionViewModel>();
        public int? UserSurveyId { get; set; }
    }

    public class SurveyInTopicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public SurveyTrackingViewModel SurveyTracking { get; set; }
    }

    public class SurveyViewModelWithoutQuestions
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TopicId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public Guid CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
    }

    public class SurveyManagementViewModel : SurveyViewModelWithoutQuestions
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string ParentCode { get; set; }
        public string ParentName { get; set; }
        public string TopicName { get; set; }
    }
}