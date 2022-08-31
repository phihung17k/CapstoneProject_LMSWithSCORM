using LMS.Core.Models.RequestModels.OptionRequestModel;
using System.Collections.Generic;

namespace LMS.Core.Models.RequestModels.QuestionRequestModel
{
    public class QuestionUpdateRequestModel : QuestionRequestModel
    {

        public bool IsActive { get; set; }

        public List<OptionUpdateRequestModel> Options { get; set; }
    }
}
