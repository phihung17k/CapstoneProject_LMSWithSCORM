using LMS.Core.Enum;

namespace LMS.Core.Models.RequestModels.SurveyQuestionRequestModel
{
    public class SurveyQuestionRequestModel
    {
        public SurveyQuestionType Type { get; set; }

        /// <summary>
        /// Correspond topic in matrix question OR content in other question types
        /// </summary>
        public string Name { get; set; }
    }
}
