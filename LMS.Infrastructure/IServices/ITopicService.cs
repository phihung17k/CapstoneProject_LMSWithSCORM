using System.Threading.Tasks;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.RequestModels.LearningResourceRequestModel;
using LMS.Core.Models.ViewModels;

namespace LMS.Infrastructure.IServices
{
    public interface ITopicService
    {
        Task<TopicViewModelWithoutResource> CreateTopic(TopicCreateRequestModel topicRequestModel);
        Task<TopicViewModelWithoutResource> Update(int topicId, TopicUpdateRequestModel topicRequestModel);
        Task Delete(int topicId);
        Task<TopicViewModelWithResource> MoveLearningResourcesToTopic(int topicId, 
            LearningResourceListMovingRequestModel requestModel);
    }
}