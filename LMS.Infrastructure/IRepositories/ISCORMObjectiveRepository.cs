using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Data;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IRepositories
{
    public interface ISCORMObjectiveRepository : IBaseRepository<SCORMObjective, int>
    {
        void GetObjective(ref LMSModel lms, bool isSCORM12 = false);
        Task<LMSModel> SetObjectives(LMSModel lms, SCORMCore scormCore, bool isSCORM12 = false);
    }
}
