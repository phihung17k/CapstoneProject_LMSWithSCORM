using LMS.Core.Models.RequestModels.SurveyQuestionRequestModel;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.RequestModels.SurveyRequestModel
{
    public class SurveyCreateRequestModel : SurveyRequestModel
    {
        public int TopicId { get; set; }

        /// <summary>
        /// Indicate list of question
        /// </summary>
        public List<SurveyQuestionCreateRequestModel> Elements { get; set; }
    }
}