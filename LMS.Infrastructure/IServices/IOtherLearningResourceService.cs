using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface IOtherLearningResourceService
    {
        Task<OtherLearningResourceViewModel> UploadOtherLearningResourceInSection(int sectionId, IFormFile resource);
        Task<TopicOLRWithoutTrackingViewModel> UploadOtherLearningResourceInTopic(int topicId, IFormFile resource);
        //Task<TopicOLRListViewModel> MoveOtherLearningResourcesToTopic(
        //    OtherLearningResourceListMovingRequestModel requestModel);
        Task DeleteOtherLearningResourceInTopic(int topicOLRId);
        Task DeleteOtherLearningResourceInSection(int OLRId);
        Task<OtherLearningResourceViewContentModel> ViewContent(int topicOtherLearningResourceId);
        Task<TopicOLRWithoutTrackingViewModel> UpdateOtherLearningResourceInTopic(int topicOtherLearningResourceId, TopicOLRUpdateRequestModel requestModel);
    }
}
