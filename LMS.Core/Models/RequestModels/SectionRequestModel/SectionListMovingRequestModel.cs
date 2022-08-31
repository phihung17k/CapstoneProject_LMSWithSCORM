using System.Collections.Generic;

namespace LMS.Core.Models.RequestModels.SectionRequestModel
{
    public class SectionListMovingRequestModel
    {
        public List<SectionMovingRequestModel> Sections { get; set; }
    }

    public class SectionMovingRequestModel
    {
        public int SectionId { get; set; }
        public List<int> OtherLearningResourceIds { get; set; }
        public List<int> ScormIds { get; set; }
    }
}
