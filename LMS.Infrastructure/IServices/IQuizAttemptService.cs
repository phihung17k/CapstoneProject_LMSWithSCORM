using LMS.Core.Entity;
using LMS.Core.Models.RequestModels.QuizAttemptRequestModel;
using LMS.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface IQuizAttemptService
    {
        Task<QuizAttemptViewModel> CreateQuizAttempt(QuizAttemptRequestModel requestModel);
        Task<SubmitQuizViewModel> UpdateQuizAttemptResult(long quizAttemptId, QuizSubmitRequestModel requestModel, bool isAutoSubmit = false);
        Task<QuizResultViewModel> ReviewOwnQuizAttempt(long quizAttemptId);
        Task CreateUserQuizWhenQuizEnd(List<Guid> studentIds, Quiz quiz, int courseId);
    }
}
