using System.Collections.Generic;
using LMS.Core.Models.RequestModels.SurveyQuestionRequestModel;

namespace LMS.Core.Models.RequestModels.SurveyRequestModel
{
    public class SurveyUpdateRequestModel : SurveyRequestModel
    {
        /// <summary>
        /// Indicate list of question
        /// </summary>
        public List<SurveyQuestionUpdateRequestModel> Elements { get; set; }
    }
}