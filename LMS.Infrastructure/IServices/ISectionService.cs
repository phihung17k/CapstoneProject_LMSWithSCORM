using LMS.Core.Models.RequestModels.SectionRequestModel;
using LMS.Core.Models.ViewModels;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface ISectionService
    {
        Task<SectionViewModel> CreateSection(SectionCreateRequestModel requestModel);
        Task<SectionViewModel> UpdateSection(int sectionId, SectionUpdateRequestModel requestModel);
        Task Delete(int sectionId);
    }
}
