using System.Collections.Generic;

namespace LMS.Core.Models.RequestModels.LearningResourceRequestModel
{
    public class LearningResourceListMovingRequestModel
    {
        public List<LearningResourceMovingRequestModel> Resources { get; set; }
    }

    public class LearningResourceMovingRequestModel
    {
        public int ResourceId { get; set; }
        public bool IsSCORM { get; set; }
    }
}
