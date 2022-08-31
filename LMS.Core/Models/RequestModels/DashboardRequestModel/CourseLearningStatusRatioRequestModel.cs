using LMS.Core.Enum;

namespace LMS.Core.Models.RequestModels.DashboardRequestModel
{
    public class AttendeeLearningProgressRatioRequestModel
    {
        public int CourseId { get; set; }
        public ActionTypeWithoutStudy? ActionType { get; set; }
    }
}
