using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface ITrackingOtherLearningResourseService
    {
        Task<OtherLearningResourceTrackingViewModel> Tracking(OtherLearningResourceTrackingRequestModel trackingRequestModel);
        Task<OtherLearningResourceUpdateProgressViewModel> UpdateLearningProgress(int OLRTrackingId, LearningProgressUpdateRequestModel updateRequestModel);
    }
}
