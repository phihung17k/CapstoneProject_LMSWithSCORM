using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.Utils;
using System.Linq;
using static LMS.Core.Common.SCORMConstants;

namespace LMS.Infrastructure.Repositories
{
    public class SCORMCommentFromLMSRepository : BaseRepository<SCORMCommentFromLms, int>, ISCORMCommentFromLMSRepository
    {
        public SCORMCommentFromLMSRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void GetCommentsFromLMSValue(ref LMSModel lms)
        {
            //cmi.comments_from_lms._children
            //cmi.comments_from_lms._count
            //cmi.comments_from_lms.n.comment
            //cmi.comments_from_lms.n.location
            //cmi.comments_from_lms.n.timestamp

            string dataItem = lms.DataItem;
            int coreId = lms.SCORMCoreId;
            if (dataItem.Equals(CmiCommentsFromLmsChildren))
            {
                lms.ReturnValue = string.Join(",", Comment, Location, Timestamp);
                return;
            }
            if (dataItem.Equals(CmiCommentsFromLmsCount))
            {
                lms.ReturnValue = base.Get(cmt => cmt.SCORMCoreId == coreId).Count().ToString();
                return;
            }
            string delimStr = ".";
            string[] sDataItem = dataItem.Split(delimStr);
            string Index = sDataItem[2]; // get "n" from the string
            string DataItem = sDataItem[3]; // get desired field from the string
            if (int.TryParse(Index, out int n)) // purpose of this is to guarantee that "n" is an integer
            {
                var comment = base.Get(cmt => cmt.SCORMCoreId == coreId && cmt.N == n).FirstOrDefault();
                if (comment == null)
                {
                    lms.ErrorCode = ScormErrorCodes.E301;
                    lms.ErrorString = ScormErrorStrings.OutOfRange;
                    lms.ReturnValue = ScormReturnValues.Empty;
                    return;
                }
                switch (DataItem)
                {
                    case Comment:
                        lms.ReturnValue = comment.Comment;
                        break;
                    case Location:
                        lms.ReturnValue = comment.Location;
                        break;
                    case Timestamp:
                        lms.ReturnValue = comment.Timestamp;
                        break;
                }
            }
            else
            {
                TrackingSCORMUtils.SetArgumentError(ref lms);
            }
        }
    }
}
