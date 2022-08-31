using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class SCORMInteractionObjectiveRepository : BaseRepository<SCORMInteractionObjective, int>,
        ISCORMInteractionObjectiveRepository
    {
        public SCORMInteractionObjectiveRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
