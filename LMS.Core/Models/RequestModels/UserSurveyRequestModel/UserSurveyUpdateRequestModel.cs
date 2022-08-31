using System.Collections.Generic;

namespace LMS.Core.Models.RequestModels.UserSurveyRequestModel
{
    public class UserSurveyUpdateRequestModel
    {
        public int UserSurveyId { get; set; }
        public List<UserSurveyDetailRequestModel> UserSurveyDetailList { get; set; }
    }
}
