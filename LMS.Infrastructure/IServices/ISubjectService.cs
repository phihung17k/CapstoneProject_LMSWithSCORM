using LMS.Core.Models.RequestModels;
using LMS.Core.Models.RequestModels.SubjectRequestModel;
using LMS.Core.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface ISubjectService
    {
        Task<PagingViewModel<SubjectViewModel>> GetSubjectList(SubjectPagingRequestModel requestModel, Guid? userId = null);
        Task<SubjectViewModel> GetDetailSubject(int subjectId);
        Task<SubjectWithSectionsViewModel> GetSectionsAndResourcesInCourse(int subjectId, int courseId);
        Task<SubjectWithSectionsViewModel> GetSectionsAndResourcesInTopic(int subjectId, int topicId);
        Task<SubjectViewModelWithoutSection> UpdateDescription(int subjectId, SubjectUpdateRequestModel requestModel);
        Task SyncSubject();
    }
}
