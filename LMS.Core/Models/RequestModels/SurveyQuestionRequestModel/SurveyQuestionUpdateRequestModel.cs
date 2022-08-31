using System.Collections.Generic;
using LMS.Core.Enum;
using LMS.Core.Models.RequestModels.SurveyOptionRequestModel;

namespace LMS.Core.Models.RequestModels.SurveyQuestionRequestModel
{
    public class SurveyQuestionUpdateRequestModel : SurveyQuestionRequestModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Correspond options in multiple choice question
        /// </summary>
        public List<SurveyOptionUpdateRequestModel> Choices { get; set; }

        /// <summary>
        /// Correspond options in matrix question
        /// </summary>
        public List<SurveyOptionRequestModel.SurveyOptionRequestModel> Columns { get; set; }

        /// <summary>
        /// Correspond questions in matrix question
        /// </summary>
        public List<SurveyMatrixQuestionUpdateRequestModel> Rows { get; set; }
    }

    public class SurveyMatrixQuestionUpdateRequestModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}