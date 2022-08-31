using System.Threading.Tasks;
using LMS.Core.Models.RequestModels.TemplateRequestModel;
using LMS.Core.Models.ViewModels;

namespace LMS.Infrastructure.IServices
{
    public interface ITemplateService
    {
        Task<TemplateViewModel> CreateTemplate(TemplateRequestModel templateRequestModel);
        Task<TemplateViewModel> Get(int templateId);
        Task<PagingViewModel<TemplateViewModelWithoutQuestions>> Search(TemplatePagingRequestModel templatePagingRequestModel);
        Task Delete(int templateId);
        Task<TemplateViewModel> Update(int templateId, TemplateRequestModel templateRequestModel);
        Task<TemplateViewModelWithoutQuestions> UpdateStatus(int templateId, bool isActive);
    }
}