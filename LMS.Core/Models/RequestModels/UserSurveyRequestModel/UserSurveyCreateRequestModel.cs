using System;
using System.Collections.Generic;

namespace LMS.Core.Models.RequestModels.UserSurveyRequestModel
{
    public class UserSurveyCreateRequestModel
    {
        public int SurveyId { get; set; }

        public List<UserSurveyDetailRequestModel> UserSurveyDetailList { get; set; }
    }
}
