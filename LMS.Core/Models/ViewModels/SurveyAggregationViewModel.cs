using LMS.Core.Enum;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class SurveyAggregationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        //numer of attendee response in this survey
        public int NumberOfResponses { get; set; }
        public int TotalAttendees { get; set; }
        public List<SurveyQuestionAggregationViewModel> Questions { get; set; }
    }

    public class SurveyQuestionAggregationViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string BelongTopic { get; set; }
        public SurveyQuestionType Type { get; set; }
        public int NumberOfResponses { get; set; }
        public List<SurveyOptionAggregationViewModel> Options { get; set; }
        public List<string> ListOfFeedback { get; set; }
    }

    public class SurveyOptionAggregationViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int NumberOfResponses { get; set; }
    }
}
