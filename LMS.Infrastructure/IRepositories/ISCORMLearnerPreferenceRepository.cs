using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Data;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IRepositories
{
    public interface ISCORMLearnerPreferenceRepository : IBaseRepository<SCORMLearnerPreference, int>
    {
        void GetStudentPreference(ref LMSModel lms);
        void GetLearnerPreference(ref LMSModel lms);
        Task<LMSModel> SetStudentPreference(LMSModel lms, SCORMCore scormCore);
        Task<LMSModel> SetLearnerPreference(LMSModel lms, SCORMCore scormCore);
    }
}
