using System;

namespace LMS.Core.Models.RequestModels.SurveyRequestModel
{
    public class SurveyRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
