using LMS.Core.Models.RequestModels.QuizRequestModel;
using LMS.Core.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface IQuizService
    {
        Task<QuizPreviewViewModel> CreateQuizInTopic(QuizCreateRequestModel createRequestModel);
        Task<QuizPreviewViewModel> GetQuizDetail(int quizId);
        Task<QuizPreviewViewModel> UpdateQuiz(int quizId, QuizUpdateRequestModel updateRequestModel);
        Task DeleteQuiz(int quizId);
        Task<QuizInfoViewModel> GetQuizInformation(int quizId);
        Task<QuizReportViewModel> ViewOverallQuizResult(int quizId, QuizReportRequestModel requestModel);
        Task<StudentQuizResultViewModel> ReviewStudentQuizAttempt(long quizAttemptId);
        Task<List<TopicWithRestrictionViewModel>> GetTopicListForQuizRestriction(int? topicId, int? quizId);
    }
}
