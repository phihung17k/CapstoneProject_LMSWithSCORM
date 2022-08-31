using LMS.Core.Models.RequestModels.QuestionBankRequestModel;
using LMS.Core.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface IQuestionBankService
    {
        Task<List<QuestionBankBySubjectViewModel>> GetQuestionBankBySubject(QuestionBankRequestModel requestModel);
        Task<QuestionBankViewModel> GetQuestionBankDetail(int questionBankId);
        Task<QuestionBankViewModel> CreateQuestionBank(QuestionBankCreateRequestModel createRequestModel);
        Task<QuestionBankViewModel> UpdateQuestionBank(int questionBankId, QuestionBankUpdateRequestModel updateRequestModel);
        //Task<QuestionBankViewModel> UpdateQuestionBankStatus(int questionBankId, bool isActive);
        Task<bool> DeleteQuestionBank(int questionBankId);
    }
}