using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class SCORMInteractionCorrectResponseRepository : BaseRepository<SCORMInteractionCorrectResponse, int>,
        ISCORMInteractionCorrectResponseRepository
    {
        public SCORMInteractionCorrectResponseRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
