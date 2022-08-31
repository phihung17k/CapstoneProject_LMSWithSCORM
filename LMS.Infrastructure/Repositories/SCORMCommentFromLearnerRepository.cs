using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using static LMS.Core.Common.SCORMConstants;

namespace LMS.Infrastructure.Repositories
{
    public class SCORMCommentFromLearnerRepository : BaseRepository<SCORMCommentFromLearner, int>, 
        ISCORMCommentFromLearnerRepository
    {
        public SCORMCommentFromLearnerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void GetCommentsFromLeanerValue(ref LMSModel lms)
        {
            //cmi.comments_from_learner._children
            //cmi.comments_from_learner._count
            //cmi.comments_from_learner.n.comment
            //cmi.comments_from_learner.n.location
            //cmi.comments_from_learner.n.timestamp

            string dataItem = lms.DataItem;
            int coreId = lms.SCORMCoreId;
            if (dataItem.Equals(CmiCommentsFromLearnerChildren))
            {
                lms.ReturnValue = string.Join(",", Comment, Location, Timestamp);
                return;
            }
            if (dataItem.Equals(CmiCommentsFromLearnerCount))
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

        public async Task<LMSModel> SetCommentsFromLearner(LMSModel lms, SCORMCore scormCore)
        {
            //cmi.comments_from_learner.n.comment
            //cmi.comments_from_learner.n.location
            //cmi.comments_from_learner.n.timestamp
            string[] dataItems = lms.DataItem.Split(".");
            bool isInteger = int.TryParse(dataItems[2], out int n);
            if (!isInteger)
            {
                TrackingSCORMUtils.SetUndefinedDataModelElement(ref lms);
                return lms;
            }

            var scormCommentList = Get(sc => sc.SCORMCoreId == scormCore.Id);
            int count = scormCommentList.Count();
            if (count < n)
            {
                //Error: The data model element collection was attempted to be set out of order
                //when SCO request GetDiagnostic()
                //Ex: in db, cmi.comments_from_learner.0.comment
                //    in request cmi.comments_from_learner.2.comment
                //  correct n = 1 prior to n = 2
                TrackingSCORMUtils.SetGeneralSetFailure(ref lms);
            }
            else if (count >= n)
            {
                SCORMCommentFromLearner scormComment = scormCommentList.Where(sc => sc.N == n).FirstOrDefault();
                switch (dataItems[3])
                {
                    case Comment:
                        if (TrackingSCORMUtils.IsCMIString4000(lms.DataValue))
                        {
                            if (count == n)
                            {
                                await base.AddAsync(new SCORMCommentFromLearner
                                {
                                    N = n,
                                    SCORMCoreId = scormCore.Id,
                                    Comment = lms.DataValue
                                });
                            }
                            else
                            {
                                scormComment.Comment = lms.DataValue;
                            }
                        }
                        else
                        {
                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                        }
                        break;
                    case Location:
                        if (TrackingSCORMUtils.IsCMIString250(lms.DataValue))
                        {
                            if (count == n)
                            {
                                await base.AddAsync(new SCORMCommentFromLearner
                                {
                                    N = n,
                                    SCORMCoreId = scormCore.Id,
                                    Location = lms.DataValue
                                });
                            }
                            else
                            {
                                scormComment.Location = lms.DataValue;
                            }
                        }
                        else
                        {
                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                        }
                        break;
                    case Timestamp:
                        DateTimeOffset timestampValue;
                        bool isTimestamp = DateTimeOffset.TryParse(lms.DataValue, out timestampValue);
                        if (isTimestamp)
                        {
                            if (count == n)
                            {
                                await base.AddAsync(new SCORMCommentFromLearner
                                {
                                    N = n,
                                    SCORMCoreId = scormCore.Id,
                                    Timestamp = lms.DataValue
                                });
                            }
                            else
                            {
                                scormComment.Timestamp = lms.DataValue;
                            }
                        }
                        else
                        {
                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                        }
                        break;
                }
                if (lms.ErrorCode.Equals(ScormErrorCodes.E0))
                {
                    await applicationDbContext.SaveChangesAsync();
                }
            }
            return lms;
        }
    }
}
