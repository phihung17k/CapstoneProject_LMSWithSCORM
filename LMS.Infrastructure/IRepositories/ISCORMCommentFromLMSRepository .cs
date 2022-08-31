using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.IRepositories
{
    public interface ISCORMCommentFromLMSRepository : IBaseRepository<SCORMCommentFromLms, int>
    {
        void GetCommentsFromLMSValue(ref LMSModel lms);
    }
}
