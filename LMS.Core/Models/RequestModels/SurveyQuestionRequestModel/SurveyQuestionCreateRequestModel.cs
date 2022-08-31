using System.Collections.Generic;

namespace LMS.Core.Models.RequestModels.SurveyQuestionRequestModel
{
    public class SurveyQuestionCreateRequestModel : SurveyQuestionRequestModel
    {
        /// <summary>
        /// Correspond options in multiple choice question
        /// </summary>
        public List<SurveyOptionRequestModel.SurveyOptionRequestModel> Choices { get; set; }

        /// <summary>
        /// Correspond options in matrix question
        /// </summary>
        public List<SurveyOptionRequestModel.SurveyOptionRequestModel> Columns { get; set; }

        /// <summary>
        /// Correspond questions in matrix question
        /// </summary>
        public List<SurveyMatrixQuestionCreateRequestModel> Rows { get; set; }
    }

    public class SurveyMatrixQuestionCreateRequestModel
    {
        public string Content { get; set; }
    }
}