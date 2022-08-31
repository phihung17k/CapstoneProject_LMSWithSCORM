using LMS.Core.Enum;
using LMS.Core.Models.Common.RequestModels;
using System;

namespace LMS.Core.Models.RequestModels.SurveyRequestModel
{
    public class SurveyPagingRequestModel : PagingRequestModel
    {
        public string Search { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public ActionTypeWithoutStudy? ActionType { get; set; }
    }
}
