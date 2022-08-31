using LMS.Core.Common;
using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.Utils;
using System.Linq;
using System.Threading.Tasks;
using static LMS.Core.Common.SCORMConstants;

namespace LMS.Infrastructure.Repositories
{
    public class SCORMObjectiveRepository : BaseRepository<SCORMObjective, int>, ISCORMObjectiveRepository
    {
        public SCORMObjectiveRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void GetObjective(ref LMSModel lms, bool isSCORM12 = false)
        {
            //cmi.objectives.n.score._children
            string dataItem = lms.DataItem;
            int coreId = lms.SCORMCoreId;
            if (dataItem.Equals(CmiObjectivesChildren))
            {
                if (isSCORM12)
                {
                    lms.ReturnValue = string.Join(",", Id, Score, Status);
                }
                else
                {
                    lms.ReturnValue = string.Join(",", Id, Score, SCORMConstants.SuccessStatus,
                        SCORMConstants.CompletionStatus, Description);
                }
                return;
            }
            if (dataItem.Equals(CmiObjectivesCount))
            {
                lms.ReturnValue = base.Get(i => i.SCORMCoreId == coreId).Count().ToString();
                return;
            }
            string sObjective = "", sSubObjective = "";
            string DataItem = lms.DataItem;
            string delimStr = ".";
            char[] delimiter = delimStr.ToCharArray();
            string[] sDataItem = DataItem.Split(delimiter, 4);
            string FirstIndex = sDataItem[2]; // get first "n" from the string
            sObjective = sDataItem[3]; // get type of objective from the string
            if (sDataItem.Length >= 5)
            {
                sSubObjective = sDataItem[4];
            }
            if (int.TryParse(FirstIndex, out int n1)) // purpose of this is to guarantee that "n" is an integer
            {
                var objective = base.Get(i => i.SCORMCoreId == coreId && i.N == n1).FirstOrDefault();
                if (objective == null)
                {
                    lms.ErrorCode = ScormErrorCodes.E301;
                    lms.ErrorString = ScormErrorStrings.OutOfRange;
                    lms.ReturnValue = ScormReturnValues.Empty;
                    return;
                }
                switch (sObjective)
                {
                    case Id:
                        lms.ReturnValue = objective.Nid;
                        break;
                    case ScoreChildren: //cmi.objectives.n.score._children
                        lms.ReturnValue = isSCORM12 ? string.Join(",", Raw, Min, Max)
                                                    : string.Join(",", Scaled, Raw, Min, Max);
                        break;
                    case ScoreScaled:
                        lms.ReturnValue = objective.ScoreScaled;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case ScoreRaw:
                        lms.ReturnValue = objective.ScoreRaw;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case ScoreMin:
                        lms.ReturnValue = objective.ScoreMin;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case ScoreMax:
                        lms.ReturnValue = objective.ScoreMax;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case SCORMConstants.SuccessStatus:
                        lms.ReturnValue = objective.SuccessStatus;
                        break;
                    case SCORMConstants.CompletionStatus:
                        lms.ReturnValue = objective.CompletionStatus;
                        break;
                    case ProgressMeasure:
                        lms.ReturnValue = objective.ProgressMeasure;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case Description:
                        lms.ReturnValue = objective.Description;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case Status:    //cmi.objective.n.status in SCORM 1.2
                        lms.ReturnValue = objective.Status12;
                        break;
                }
            }
            else
            {
                TrackingSCORMUtils.SetArgumentError(ref lms);
            }
        }

        public async Task<LMSModel> SetObjectives(LMSModel lms, SCORMCore scormCore, bool isSCORM12 = false)
        {
            //cmi.objectives.n.id
            //cmi.objectives.n.score.scaled
            //cmi.objectives.n.score.raw
            //cmi.objectives.n.score.min
            //cmi.objectives.n.score.max
            //cmi.objectives.n.progress_measure
            //cmi.objectives.n.success_status
            //cmi.objectives.n.completion_status
            //cmi.objectives.n.description

            //cmi.objective.n.status in SCORM 1.2
            string[] dataItems = lms.DataItem.Split(".");
            bool isInteger = int.TryParse(dataItems[2], out int n);
            if (isInteger)
            {
                //list of objectives is existed when scorm core init
                //if SCO set n is out of range [0; count-1], return error 351
                var scormObjectiveList = Get(sc => sc.SCORMCoreId == scormCore.Id);
                int count = scormObjectiveList.Count();
                if (count <= n)
                {
                    TrackingSCORMUtils.SetGeneralSetFailure(ref lms);
                }
                else
                {
                    SCORMObjective scormObjective = scormObjectiveList.Where(so => so.N == n).FirstOrDefault();
                    switch (dataItems[3])
                    {
                        case Id:
                            if (TrackingSCORMUtils.IsCMIString4000(lms.DataValue))
                            {
                                scormObjective.Nid = lms.DataValue;
                            }
                            else
                            {
                                if (isSCORM12)
                                {
                                    TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                                }
                                else
                                {
                                    TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                                }
                            }
                            break;
                        case Score:
                            bool isRealNumber = false;
                            //for score.scaled, score.raw, score.min, score.max
                            switch (dataItems[4])
                            {
                                case Scaled:
                                    if (scormObjective == null || scormObjective.Nid == null)
                                    {
                                        TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                                    }
                                    else
                                    {
                                        isRealNumber = float.TryParse(lms.DataValue, out float scoreScaled);
                                        if (isRealNumber)
                                        {
                                            if (scoreScaled >= -1 && scoreScaled <= 1)
                                            {
                                                scormObjective.ScoreScaled = lms.DataValue;
                                            }
                                            else
                                            {
                                                TrackingSCORMUtils.SetDataModelElementValueOutOfRange(ref lms);
                                            }
                                        }
                                        else
                                        {
                                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                                        }
                                    }
                                    break;
                                case Raw:
                                    if (scormObjective == null || scormObjective.Nid == null)
                                    {
                                        if (!isSCORM12)
                                        {
                                            TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                                        }
                                    }
                                    else
                                    {
                                        isRealNumber = float.TryParse(lms.DataValue, out float scoreRaw);
                                        if (isRealNumber)
                                        {
                                            if (isSCORM12)
                                            {
                                                if (scoreRaw >= 0 && scoreRaw <= 100)
                                                {
                                                    scormObjective.ScoreRaw = lms.DataValue;
                                                }
                                                else
                                                {
                                                    TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                                                }
                                            }
                                            else
                                            {
                                                scormObjective.ScoreRaw = lms.DataValue;
                                            }
                                        }
                                        else
                                        {
                                            if (isSCORM12)
                                            {
                                                TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                                            }
                                            else
                                            {
                                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                                            }
                                        }
                                    }
                                    break;
                                case Min:
                                    if (scormObjective == null || scormObjective.Nid == null)
                                    {
                                        if (!isSCORM12)
                                        {
                                            TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                                        }
                                    }
                                    else
                                    {
                                        isRealNumber = float.TryParse(lms.DataValue, out float scoreMin);
                                        if (isRealNumber)
                                        {
                                            if (isSCORM12)
                                            {
                                                if (scoreMin >= 0 && scoreMin <= 100)
                                                {
                                                    scormObjective.ScoreMin = lms.DataValue;
                                                }
                                                else
                                                {
                                                    TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                                                }
                                            }
                                            else
                                            {
                                                scormObjective.ScoreMin = lms.DataValue;
                                            }
                                        }
                                        else
                                        {
                                            if (isSCORM12)
                                            {
                                                TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                                            }
                                            else
                                            {
                                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                                            }
                                        }
                                    }
                                    break;
                                case Max:
                                    if (scormObjective == null || scormObjective.Nid == null)
                                    {
                                        if (!isSCORM12)
                                        {
                                            TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                                        }
                                    }
                                    else
                                    {
                                        isRealNumber = float.TryParse(lms.DataValue, out float scoreMax);
                                        if (isRealNumber)
                                        {
                                            if (isSCORM12)
                                            {
                                                if (scoreMax >= 0 && scoreMax <= 100)
                                                {
                                                    scormObjective.ScoreMax = lms.DataValue;
                                                }
                                                else
                                                {
                                                    TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                                                }
                                            }
                                            else
                                            {
                                                scormObjective.ScoreMax = lms.DataValue;
                                            }
                                        }
                                        else
                                        {
                                            if (isSCORM12)
                                            {
                                                TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                                            }
                                            else
                                            {
                                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                                            }
                                        }
                                    }
                                    break;
                            }
                            await applicationDbContext.SaveChangesAsync();
                            break;
                        case ProgressMeasure:
                            isRealNumber = float.TryParse(lms.DataValue, out float progressMeasure);
                            if (isRealNumber)
                            {
                                if (progressMeasure >= 0 && progressMeasure <= 1)
                                {
                                    scormObjective.ProgressMeasure = lms.DataValue;
                                }
                                else
                                {
                                    TrackingSCORMUtils.SetDataModelElementValueOutOfRange(ref lms);
                                }
                            }
                            else
                            {
                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                            }
                            break;
                        case SCORMConstants.SuccessStatus:
                            if (TrackingSCORMUtils.IsCMIVocabulary(CmiSuccessStatus, lms.DataValue))
                            {
                                if (scormObjective == null || scormObjective.Nid == null)
                                {
                                    TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                                }
                                else
                                {
                                    scormObjective.SuccessStatus = lms.DataValue;
                                }
                            }
                            else
                            {
                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                            }
                            break;
                        case SCORMConstants.CompletionStatus:
                            if (TrackingSCORMUtils.IsCMIVocabulary(CmiCompletionStatus, lms.DataValue))
                            {
                                if (scormObjective == null || scormObjective.Nid == null)
                                {
                                    TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                                }
                                else
                                {
                                    scormObjective.CompletionStatus = lms.DataValue;
                                }
                            }
                            else
                            {
                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                            }
                            break;
                        case Description:
                            if (TrackingSCORMUtils.IsCMIString250(lms.DataValue))
                            {
                                if (scormObjective == null || scormObjective.Nid == null)
                                {
                                    TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                                }
                                else
                                {
                                    scormObjective.Description = lms.DataValue;
                                }
                            }
                            else
                            {
                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                            }
                            break;
                        case Status:
                            //space value is the same lesson status
                            if (TrackingSCORMUtils.IsCMIVocabulary(CmiCoreLessonStatus, lms.DataValue))
                            {
                                scormObjective.Status12 = lms.DataValue;
                            }
                            else
                            {
                                TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                            }
                            break;
                    }
                    if (lms.ErrorCode.Equals(ScormErrorCodes.E0))
                    {
                        await applicationDbContext.SaveChangesAsync();
                    }
                }
            }
            return lms;
        }
    }
}
