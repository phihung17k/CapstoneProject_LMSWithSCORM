using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using System.Linq;
using static LMS.Core.Common.SCORMConstants;

namespace LMS.Infrastructure.Repositories
{
    public class SCORMNavigationRepository : BaseRepository<SCORMNavigation, int>, ISCORMNavigationRepository
    {
        public SCORMNavigationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void GetNavigation(ref LMSModel lms)
        {
            string dataItem = lms.DataItem;
            int coreId = lms.SCORMCoreId;

            var navigation = base.Get(n => n.SCORMCoreId == coreId).FirstOrDefault();
            switch (dataItem)
            {
                case ADLNavigationRequest:
                    lms.ReturnValue = navigation.Request;
                    break;
                case ADLNavRequestValidContinue:
                    lms.ReturnValue = navigation.ValidContinue;
                    break;
                case ADLNavRequestValidPrevious:
                    lms.ReturnValue = navigation.ValidPrevious;
                    break;
            }
        }
    }
}
