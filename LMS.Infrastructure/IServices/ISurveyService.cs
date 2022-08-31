using System;
using System.Threading.Tasks;
using LMS.Core.Models.RequestModels.SurveyRequestModel;
using LMS.Core.Models.ViewModels;

namespace LMS.Infrastructure.IServices
{
    public interface ISurveyService
    {
        Task<SurveyViewModel> CreateSurveyInTopic(SurveyCreateRequestModel surveyCreateRequestModel);
        Task<SurveyViewModel> Get(int surveyId);
        Task Delete(int surveyId);
        Task<SurveyViewModel> Update(int surveyId, SurveyUpdateRequestModel surveyUpdateRequestModel);
        SurveyAggregationViewModel GetSurveyAggregation(int surveyId);
        Task<PagingViewModel<SurveyManagementViewModel>> GetSurveyList(SurveyPagingRequestModel requestModel, Guid? userId = null);
    }
}