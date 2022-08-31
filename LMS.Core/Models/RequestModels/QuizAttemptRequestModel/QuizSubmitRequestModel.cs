using System.Collections.Generic;

namespace LMS.Core.Models.RequestModels.QuizAttemptRequestModel
{
    public class QuizSubmitRequestModel
    {
        public List<QuestionSubmitRequestModel> Questions { get; set; }
    }
    public class QuestionSubmitRequestModel
    {
        public int QuestionId { get; set; }
        public List<OptionSelectedRequestModel> SelectedOptions { get; set; }
    }
    public class OptionSelectedRequestModel
    {
        public int OptionId { get; set; }
    }
}
