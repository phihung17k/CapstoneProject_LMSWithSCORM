using System.Threading.Tasks;
using LMS.Core.Models.RequestModels.QuestionRequestModel;
using LMS.Core.Models.ViewModels;

namespace LMS.Infrastructure.IServices
{
    public interface IQuestionService
    {
        Task<QuestionViewModel> CreateQuestion(QuestionCreateRequestModel questionCreateRequestModel);
        Task<QuestionViewModel> UpdateQuestion(int id, QuestionUpdateRequestModel requestModel);
        Task<PagingViewModel<QuestionViewModelWithoutOptions>> Search(QuestionPagingRequestModel questionPagingRequestModel);
        Task<QuestionViewModel> GetDetail(int questionId);
        Task Delete(int questionId);
        Task<QuestionViewModelWithoutOptions> UpdateStatus(int id, bool isActive);
    }
}