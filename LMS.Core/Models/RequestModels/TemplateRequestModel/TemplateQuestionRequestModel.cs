using LMS.Core.Enum;
using System.Collections.Generic;

namespace LMS.Core.Models.RequestModels.TemplateRequestModel
{
    public class TemplateQuestionRequestModel
    {
        public SurveyQuestionType Type { get; set; }

        /// <summary>
        /// Correspond topic in matrix question OR content in other question types
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Correspond options in multiple choice question
        /// </summary>
        public List<TemplateOptionRequestModel> Choices { get; set; }

        /// <summary>
        /// Correspond options in matrix question
        /// </summary>
        public List<TemplateOptionRequestModel> Columns { get; set; }

        /// <summary>
        /// Correspond questions in matrix question
        /// </summary>
        public List<TemplateMatrixQuestionCreateRequestModel> Rows { get; set; }
    }

    public class TemplateMatrixQuestionCreateRequestModel
    {
        public string Content { get; set; }
    }
}
