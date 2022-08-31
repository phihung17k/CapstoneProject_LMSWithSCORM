using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface ISCORMService
    {
        Task<SCORMViewModel> UploadSCORMInSection(int sectionId, IFormFile resource);
        Task<TopicSCORMWithoutCoreViewModel> UploadSCORMInTopic(int topicId, IFormFile resource);
        //Task<TopicSCORMWithoutCoreViewModel> MoveSCORMToTopic(int topicId, int resourceId);
        Task DeleteSCORMInTopic(int topicSCORMId);
        Task DeleteSCORMInSection(int scormId);
        Task<SCORMViewContentModel> ViewContent(int topicSCORMId);
        Task<TopicSCORMWithoutCoreViewModel> UpdateSCORMInTopic(int topicSCORMId, TopicScormUpdateRequestModel requestModel);
    }
}
