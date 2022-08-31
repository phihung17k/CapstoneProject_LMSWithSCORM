using LMS.Core.Models.RequestModels;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface ITrackingScormService
    {
        Task<LMSModel> InitializeSession(LMSModel lms);
        LMSModel GetValue(LMSModel lms);
        Task<LMSModel> SetValue(LMSModel lms);
        Task<LMSModel> Terminate(LMSModel lms);
        LMSModel Commit(LMSModel lms);
    }
}
