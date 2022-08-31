using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Data;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IRepositories
{
    public interface ISCORMCommentFromLearnerRepository : IBaseRepository<SCORMCommentFromLearner, int>
    {
        Task<LMSModel> SetCommentsFromLearner(LMSModel lms, SCORMCore scormCore);
        void GetCommentsFromLeanerValue(ref LMSModel lms);
    }
}
