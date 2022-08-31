using System.Collections.Generic;
using LMS.Core.Models.RequestModels.OptionRequestModel;

namespace LMS.Core.Models.RequestModels.QuestionRequestModel
{
    public class QuestionCreateRequestModel : QuestionRequestModel
    {
        public int QuestionBankId { get; set; }
        public List<OptionCreateRequestModel> Options { get; set; }
    }
}