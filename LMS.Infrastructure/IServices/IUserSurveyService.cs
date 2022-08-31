using LMS.Core.Models.RequestModels.UserSurveyRequestModel;
using LMS.Core.Models.ViewModels;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface IUserSurveyService
    {
        Task<SubmitSurveyViewModel> SubmitSurvey(UserSurveyCreateRequestModel model);

        UserSurveyViewModel GetFilledSurvey(int userSurveyId);

        Task<UserSurveyViewModel> UpdateSurveyOfStudent(UserSurveyUpdateRequestModel model);
        
    }
}
