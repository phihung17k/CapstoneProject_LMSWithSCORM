using LMS.Core.Enum;

namespace LMS.Core.Models.RequestModels.QuestionRequestModel
{
    public class QuestionRequestModel
    {
        public string Content { get; set; }
        public QuestionType Type { get; set; }
    }
}
