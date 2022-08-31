using LMS.Core.Common;
using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using static LMS.Core.Common.SCORMConstants;
using static LMS.Core.Common.SCORMConstants.CMIVocabularyTokens;

namespace LMS.Infrastructure.Repositories
{
    public class SCORMInteractionRepository : BaseRepository<SCORMInteraction, int>, ISCORMInteractionRepository
    {
        public SCORMInteractionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void GetInteractions(ref LMSModel lms, bool isSCORMVersion12 = false)
        {
            string dataItem = lms.DataItem;
            int coreId = lms.SCORMCoreId;
            if (dataItem.Equals(CmiInteractionsChildren))
            {
                if (isSCORMVersion12)
                {
                    lms.ReturnValue = string.Join(",", Id, Objectives, SCORMConstants.Type, Time, CorrectResponses,
                    Weighting, StudentResponse, Result, Latency);
                }
                else
                {
                    lms.ReturnValue = string.Join(",", Id, SCORMConstants.Type, Objectives, Timestamp, CorrectResponses,
                    Weighting, LearnerResponse, Result, Latency, Description);
                }
                return;
            }
            if (dataItem.Equals(CmiInteractionsCount))
            {
                lms.ReturnValue = base.Get(i => i.SCORMCoreId == coreId).Count().ToString();
                return;
            }
            string sInteraction = "", sSubInteraction = "";
            string DataItem = lms.DataItem;
            string delimStr = ".";
            char[] delimiter = delimStr.ToCharArray();
            string[] sDataItem = DataItem.Split(delimiter, 6);
            string FirstIndex = sDataItem[2]; // get first "n" from the string
            sInteraction = sDataItem[3]; // get type of interaction from the string
            if (sDataItem.Length >= 5)
            {
                sSubInteraction = sDataItem[4];
            }
            if (int.TryParse(FirstIndex, out int n1)) // purpose of this is to guarantee that "n" is an integer
            {
                var interaction = base.Get(i => i.SCORMCoreId == coreId && i.N == n1,
                    i => i.CorrectResonses, i => i.Objectives).AsSplitQuery().FirstOrDefault();
                if (interaction == null)
                {
                    lms.ErrorCode = ScormErrorCodes.E301;
                    lms.ErrorString = ScormErrorStrings.OutOfRange;
                    lms.ReturnValue = ScormReturnValues.Empty;
                    return;
                }
                switch (sInteraction)
                {
                    case Id:
                        //cmi.interactions.n.id in SCORM 1.2 is write only
                        if (isSCORMVersion12)

                        {
                            TrackingSCORMUtils.SetElementIsWriteOnlyError12(ref lms);
                        }
                        else
                        {
                            lms.ReturnValue = interaction.NId;
                        }
                        break;
                    case SCORMConstants.Type:
                        lms.ReturnValue = interaction.Type;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case Objectives:
                        if (sSubInteraction.Equals(Count))
                        {
                            lms.ReturnValue = interaction.Objectives.Count.ToString();
                        }
                        else
                        {
                            string SecondIndex = sInteraction;
                            if (int.TryParse(SecondIndex, out int n2))
                            {
                                var objective = interaction.Objectives.Where(o => o.N == n2).FirstOrDefault();
                                if (objective == null)
                                {
                                    lms.ErrorCode = ScormErrorCodes.E301;
                                    lms.ErrorString = ScormErrorStrings.OutOfRange;
                                    lms.ReturnValue = ScormReturnValues.Empty;
                                    return;
                                }
                                lms.ReturnValue = objective.NId;
                            }
                            else
                            {
                                TrackingSCORMUtils.SetArgumentError(ref lms);
                            }
                        }
                        break;
                    case Timestamp:
                        lms.ReturnValue = interaction.Timestamp;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case CorrectResponses:
                        if (sSubInteraction.Equals(Count))
                        {
                            lms.ReturnValue = interaction.CorrectResonses.Count.ToString();
                        }
                        else
                        {
                            string SecondIndex = sInteraction;
                            if (int.TryParse(SecondIndex, out int n2))
                            {
                                var correctResponse = interaction.CorrectResonses.Where(o => o.N == n2).FirstOrDefault();
                                if (correctResponse == null)
                                {
                                    lms.ErrorCode = ScormErrorCodes.E301;
                                    lms.ErrorString = ScormErrorStrings.OutOfRange;
                                    lms.ReturnValue = ScormReturnValues.Empty;
                                    return;
                                }
                                lms.ReturnValue = correctResponse.Pattern;
                            }
                            else
                            {
                                TrackingSCORMUtils.SetArgumentError(ref lms);
                            }
                        }
                        break;
                    case Weighting:
                        lms.ReturnValue = interaction.Weighting;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case LearnerResponse:
                        lms.ReturnValue = interaction.LearnerResponse;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case Result:
                        lms.ReturnValue = interaction.Result;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case Latency:
                        lms.ReturnValue = interaction.Latency;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case Description:
                        lms.ReturnValue = interaction.Description;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                }
            }
            else
            {
                TrackingSCORMUtils.SetArgumentError(ref lms);
            }
        }

        public async Task<LMSModel> SetInteractions(LMSModel lms, SCORMCore scormCore, bool isSCORMVersion12 = false)
        {
            //cmi.interactions.n.id
            //cmi.interactions.n.type
            //cmi.interactions.n.weighting
            //cmi.interactions.n.learner_response
            //cmi.interactions.n.result
            //cmi.interactions.n.latency
            //cmi.interactions.n.description
            //cmi.interfactions.n.timestamp
            //cmi.interactions.n.objectives.n.id
            //cmi.interactions.n.correct_response.n.pattern

            //cmi.interfactions.n.time in SCORM 1.2
            string[] dataItems = lms.DataItem.Split(".");
            bool isInteger = int.TryParse(dataItems[2], out int n);
            if (!isInteger)
            {
                TrackingSCORMUtils.SetUndefinedDataModelElement(ref lms);
                return lms;
            }

            var interactionList = base.Get(sc => sc.SCORMCoreId == scormCore.Id);
            int count = interactionList.Count();
            if (count < n)
            {
                TrackingSCORMUtils.SetGeneralSetFailure(ref lms);
            }
            else if (count >= n)
            {
                var scormInteraction = interactionList.Where(i => i.N == n).FirstOrDefault();
                switch (dataItems[3])
                {
                    case Id:
                        if (TrackingSCORMUtils.IsCMIString4000(lms.DataValue))
                        {
                            if (count > n)
                            {
                                scormInteraction.NId = lms.DataValue;
                            }
                            else
                            {
                                await base.AddAsync(new SCORMInteraction
                                {
                                    N = n,
                                    NId = lms.DataValue,
                                    SCORMCoreId = scormCore.Id
                                });
                            }
                        }
                        else
                        {
                            if (isSCORMVersion12)
                            {
                                TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                            }
                            else
                            {
                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                            }
                        }
                        break;
                    case SCORMConstants.Type:
                        if (TrackingSCORMUtils.IsCMIVocabulary(CmiInteractions, lms.DataValue))
                        {
                            if (count > n)
                            {
                                scormInteraction.Type = lms.DataValue;
                            }
                            else
                            {
                                await base.AddAsync(new SCORMInteraction
                                {
                                    N = n,
                                    Type = lms.DataValue,
                                    SCORMCoreId = scormCore.Id
                                });
                            }
                        }
                        else
                        {
                            if (isSCORMVersion12)
                            {
                                TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                            }
                            else
                            {
                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                            }
                        }
                        break;
                    case Weighting:
                        bool isRealNumber = false;
                        if (scormInteraction == null || scormInteraction.NId == null)
                        {
                            TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                        }
                        else
                        {
                            isRealNumber = float.TryParse(lms.DataValue, out float weight);
                            if (isRealNumber)
                            {
                                if (count > n)
                                {
                                    scormInteraction.Weighting = lms.DataValue;
                                }
                                else
                                {
                                    await base.AddAsync(new SCORMInteraction
                                    {
                                        N = n,
                                        Weighting = lms.DataValue,
                                        SCORMCoreId = scormCore.Id
                                    });
                                }
                            }
                            else
                            {
                                if (isSCORMVersion12)
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
                    case LearnerResponse:
                        if (scormInteraction == null || scormInteraction.NId == null || scormInteraction.Type == null)
                        {
                            TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                        }
                        else
                        {
                            //if not passed condition meaning false, don't save
                            bool flag = false;
                            switch (scormInteraction.Type)
                            {
                                case InteractionType.TrueFalse:
                                    if (TrackingSCORMUtils.IsCMIVocabulary(LearnerResponse, lms.DataValue))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Choice:
                                    if (TrackingSCORMUtils.CheckValidInLearnerResponse(lms.DataValue, 36))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.FillIn:
                                    if (TrackingSCORMUtils.CheckValidInLearnerResponse(lms.DataValue, 10))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.LongFillIn:
                                    string temp = TrackingSCORMUtils.RemoveLocalizedString(lms.DataValue);
                                    if (TrackingSCORMUtils.IsCMIString4000(temp))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Likert:
                                    if (TrackingSCORMUtils.IsCMIString250(lms.DataValue))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Matching:
                                    if (TrackingSCORMUtils.CheckValidMatchingInLearnerResponse(lms.DataValue))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Performance:
                                    if (TrackingSCORMUtils.CheckValidPerformanceInLearnerResponse(lms.DataValue))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Sequencing:
                                    if (TrackingSCORMUtils.CheckValidInLearnerResponse(lms.DataValue, 36,
                                        isOnceElementInArray: true))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Numeric:
                                    isRealNumber = float.TryParse(lms.DataValue, out float realNumber);
                                    if (isRealNumber)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Other:
                                    if (TrackingSCORMUtils.IsCMIString4000(lms.DataValue))
                                    {
                                        flag = true;
                                    }
                                    break;
                            }
                            if (flag)
                            {
                                if (count > n)
                                {
                                    scormInteraction.LearnerResponse = lms.DataValue;
                                }
                                else
                                {
                                    await base.AddAsync(new SCORMInteraction
                                    {
                                        N = n,
                                        LearnerResponse = lms.DataValue,
                                        SCORMCoreId = scormCore.Id
                                    });
                                }
                            }
                            else
                            {
                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                            }
                        }
                        break;
                    case Result:
                        if (scormInteraction == null || scormInteraction.NId == null)
                        {
                            TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                        }
                        else
                        {
                            isRealNumber = float.TryParse(lms.DataValue, out float result);
                            if (isRealNumber || TrackingSCORMUtils.IsCMIVocabulary(Result, lms.DataValue))
                            {
                                if (count > n)
                                {
                                    scormInteraction.Result = lms.DataValue;
                                }
                                else
                                {
                                    await base.AddAsync(new SCORMInteraction
                                    {
                                        N = n,
                                        Result = lms.DataValue,
                                        SCORMCoreId = scormCore.Id
                                    });
                                }
                            }
                            else
                            {
                                if (isSCORMVersion12)
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
                    case Latency:
                        if (scormInteraction == null || scormInteraction.NId == null)
                        {
                            TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                        }
                        else
                        {
                            try
                            {
                                if (isSCORMVersion12)
                                {
                                    TimeSpan.Parse(lms.DataValue);
                                }
                                else
                                {
                                    XmlConvert.ToTimeSpan(lms.DataValue);
                                }

                                if (count > n)
                                {
                                    scormInteraction.Latency = lms.DataValue;
                                }
                                else
                                {
                                    await base.AddAsync(new SCORMInteraction
                                    {
                                        N = n,
                                        Latency = lms.DataValue,
                                        SCORMCoreId = scormCore.Id
                                    });
                                }
                            }
                            catch (FormatException)
                            {
                                if (isSCORMVersion12)
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
                    case Description:
                        if (scormInteraction == null || scormInteraction.NId == null)
                        {
                            TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                        }
                        else
                        {
                            string temp = TrackingSCORMUtils.RemoveLocalizedString(lms.DataValue);
                            if (TrackingSCORMUtils.IsCMIString250(temp))
                            {
                                if (count > n)
                                {
                                    scormInteraction.Description = lms.DataValue;
                                }
                                else
                                {
                                    await base.AddAsync(new SCORMInteraction
                                    {
                                        N = n,
                                        Description = lms.DataValue,
                                        SCORMCoreId = scormCore.Id
                                    });
                                }
                            }
                            else
                            {
                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                            }
                        }
                        break;
                    case Timestamp:
                        if (scormInteraction == null || scormInteraction.NId == null)
                        {
                            TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                        }
                        else
                        {
                            DateTimeOffset timestampValue;
                            bool isTimestamp = DateTimeOffset.TryParse(lms.DataValue, out timestampValue);
                            if (isTimestamp)
                            {
                                if (count > n)
                                {
                                    scormInteraction.Timestamp = lms.DataValue;
                                }
                                else
                                {
                                    await base.AddAsync(new SCORMInteraction
                                    {
                                        N = n,
                                        Timestamp = lms.DataValue,
                                        SCORMCoreId = scormCore.Id
                                    });
                                }
                            }
                            else
                            {
                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                            }
                        }
                        break;
                    case Objectives:    //for objectives.n.
                        if (scormInteraction == null || scormInteraction.NId == null)
                        {
                            TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                        }
                        else
                        {
                            var scormInteractionObjectiveList = applicationDbContext.SCORMInteractionObjectives
                                .Where(sio => sio.InteractionId == scormInteraction.Id);
                            int tempCount = scormInteractionObjectiveList.Count();
                            int.TryParse(dataItems[4], out int nObjective);
                            if (tempCount < nObjective)
                            {
                                TrackingSCORMUtils.SetGeneralSetFailure(ref lms);
                            }
                            else if (tempCount >= nObjective)
                            {
                                //create
                                if (TrackingSCORMUtils.IsCMIString4000(lms.DataValue))
                                {
                                    if (tempCount > nObjective)
                                    {
                                        var scormInteractionObjective = scormInteractionObjectiveList
                                                .Where(sio => sio.N == nObjective).FirstOrDefault();
                                        scormInteractionObjective.NId = lms.DataValue;
                                    }
                                    else
                                    {
                                        await applicationDbContext.SCORMInteractionObjectives.AddAsync(
                                            new SCORMInteractionObjective
                                        {
                                            N = nObjective,
                                            NId = lms.DataValue,
                                            InteractionId = scormInteraction.Id
                                        });
                                    }
                                }
                                else
                                {
                                    if (isSCORMVersion12)
                                    {
                                        TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                                    }
                                    else
                                    {
                                        TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                                    }
                                }
                            }
                        }
                        break;
                    case CorrectResponses:
                        if (scormInteraction == null || scormInteraction.NId == null || scormInteraction.Type == null)
                        {
                            TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                        }
                        else
                        {
                            var scormInteractionCorrectResponseList = applicationDbContext.SCORMInteractionCorrectResponses
                                    .Where(sicr => sicr.InteractionId == scormInteraction.Id);
                            int tempCount = scormInteractionCorrectResponseList.Count();
                            int.TryParse(dataItems[4], out int nCorrectResponse);
                            if (tempCount < nCorrectResponse)
                            {
                                TrackingSCORMUtils.SetGeneralSetFailure(ref lms);
                            }
                            else if (tempCount >= nCorrectResponse)
                            {
                                var scormInteractionCorrect = scormInteractionCorrectResponseList
                                                                .Where(i => i.N == nCorrectResponse).FirstOrDefault();
                                bool flag = false;
                                switch (scormInteraction.Type)
                                {
                                    case InteractionType.TrueFalse:
                                        if (TrackingSCORMUtils.IsCMIVocabulary(LearnerResponse, lms.DataValue))
                                        {
                                            bool isExist = scormInteractionCorrectResponseList
                                                            .Any(sicr => sicr.Pattern.Equals(lms.DataValue));
                                            if (isExist)
                                            {
                                                TrackingSCORMUtils.SetGeneralSetFailure(ref lms);
                                            }
                                            else
                                            {
                                                flag = true;
                                            }
                                        }
                                        break;
                                    case InteractionType.Choice:
                                        if (TrackingSCORMUtils.CheckValidInLearnerResponse(lms.DataValue, 36))
                                        {
                                            bool isExist = scormInteractionCorrectResponseList
                                                            .Any(sicr => sicr.Pattern.Equals(lms.DataValue));
                                            if (isExist)
                                            {
                                                TrackingSCORMUtils.SetGeneralSetFailure(ref lms);
                                            }
                                            else
                                            {
                                                flag = true;
                                            }
                                        }
                                        break;
                                    case InteractionType.FillIn:
                                        if (TrackingSCORMUtils.CheckValidFillInCorrectResponse(lms.DataValue))
                                        {
                                            flag = true;
                                        }
                                        break;
                                    case InteractionType.LongFillIn:
                                        string temp = TrackingSCORMUtils.RemoveLocalizedString(lms.DataValue);
                                        if (TrackingSCORMUtils.IsCMIString4000(temp))
                                        {
                                            flag = true;
                                        }
                                        break;
                                    case InteractionType.Likert:
                                        if (TrackingSCORMUtils.IsCMIString250(lms.DataValue))
                                        {
                                            //only be one correct response for this type of interaction
                                            //cmi.interactions.n.correct_responses.0.pattern
                                            if (nCorrectResponse > 0)
                                            {
                                                TrackingSCORMUtils.SetGeneralSetFailure(ref lms);
                                            }
                                            else
                                            {
                                                flag = true;
                                            }
                                        }
                                        break;
                                    case InteractionType.Matching:  //similar to learner response
                                        if (TrackingSCORMUtils.CheckValidMatchingInLearnerResponse(lms.DataValue))
                                        {
                                            flag = true;
                                        }
                                        break;
                                    case InteractionType.Performance:
                                        if (TrackingSCORMUtils.CheckValidPerformanceInCorrectResponse(lms.DataValue))
                                        {
                                            flag = true;
                                        }
                                        break;
                                    case InteractionType.Sequencing:
                                        if (TrackingSCORMUtils.CheckValidInLearnerResponse(lms.DataValue, 36,
                                            isOnceElementInArray: true))
                                        {
                                            //correct responses is uniqueness
                                            bool isExist = scormInteractionCorrectResponseList
                                                            .Any(sicr => sicr.Pattern.Equals(lms.DataValue));
                                            if (isExist)
                                            {
                                                TrackingSCORMUtils.SetGeneralSetFailure(ref lms);
                                            }
                                            else
                                            {
                                                flag = true;
                                            }
                                        }
                                        break;
                                    case InteractionType.Numeric:
                                        //check step_answer is a numerical format <min>[:]<max>
                                        if (lms.DataValue.Contains("[:]"))
                                        {
                                            string[] stepAnswerValues = lms.DataValue.Split("[:]");
                                            //check min max format real number
                                            bool check = true;
                                            foreach (string value in stepAnswerValues)
                                            {
                                                bool isRealNumer = float.TryParse(value, out float realNumber);
                                                if (!isRealNumer)
                                                {
                                                    check = false;
                                                }
                                            }
                                            if (check)
                                            {
                                                bool isExist = scormInteractionCorrectResponseList
                                                            .Any(sicr => sicr.Pattern.Equals(lms.DataValue));
                                                if (isExist)
                                                {
                                                    TrackingSCORMUtils.SetGeneralSetFailure(ref lms);
                                                }
                                                else
                                                {
                                                    flag = true;
                                                }
                                            }
                                        }
                                        break;
                                    case InteractionType.Other:
                                        if (TrackingSCORMUtils.IsCMIString4000(lms.DataValue))
                                        {
                                            bool isExist = scormInteractionCorrectResponseList
                                                            .Any(sicr => sicr.Pattern.Equals(lms.DataValue));
                                            if (isExist)
                                            {
                                                TrackingSCORMUtils.SetGeneralSetFailure(ref lms);
                                            }
                                            else
                                            {
                                                flag = true;
                                            }
                                        }
                                        break;
                                }
                                if (flag)
                                {
                                    if (tempCount > nCorrectResponse)
                                    {
                                        scormInteractionCorrect.Pattern = lms.DataValue;
                                    }
                                    else
                                    {
                                        await applicationDbContext.SCORMInteractionCorrectResponses.AddAsync(
                                            new SCORMInteractionCorrectResponse
                                        {
                                            N = nCorrectResponse,
                                            Pattern = lms.DataValue,
                                            InteractionId = scormInteraction.Id
                                        });
                                    }
                                }
                                else
                                {
                                    if (!lms.ErrorCode.Equals("351"))
                                    {
                                        if (isSCORMVersion12)
                                        {
                                            TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                                        }
                                        else
                                        {
                                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case Time: //for SCORM 1.2
                        bool isTime = TimeSpan.TryParse(scormCore.SessionTime, out TimeSpan time);
                        if (isTime)
                        {
                            if (count > n)
                            {
                                scormInteraction.Timestamp = lms.DataValue;
                            }
                            else
                            {
                                await base.AddAsync(new SCORMInteraction
                                {
                                    N = n,
                                    Timestamp = lms.DataValue,
                                    SCORMCoreId = scormCore.Id
                                });
                            }
                        }
                        else
                        {
                            if (isSCORMVersion12)
                            {
                                TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                            }
                            else
                            {
                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                            }
                        }
                        break;
                    case StudentResponse:
                        if (scormInteraction == null || scormInteraction.NId == null || scormInteraction.Type == null)
                        {
                            TrackingSCORMUtils.SetDataModelDependencyNotEstablished(ref lms);
                        }
                        else
                        {
                            //if not passed condition meaning false, don't save
                            bool flag = false;
                            switch (scormInteraction.Type)
                            {
                                case InteractionType.TrueFalse:
                                    if (TrackingSCORMUtils.IsCMIVocabulary(LearnerResponse, lms.DataValue))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Choice:
                                    if (TrackingSCORMUtils.CheckValidInLearnerResponse(lms.DataValue, 36))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.FillIn:
                                    if (TrackingSCORMUtils.CheckValidInLearnerResponse(lms.DataValue, 10))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.LongFillIn:
                                    string temp = TrackingSCORMUtils.RemoveLocalizedString(lms.DataValue);
                                    if (TrackingSCORMUtils.IsCMIString4000(temp))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Likert:
                                    if (TrackingSCORMUtils.IsCMIString250(lms.DataValue))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Matching:
                                    if (TrackingSCORMUtils.CheckValidMatchingInLearnerResponse(lms.DataValue))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Performance:
                                    if (TrackingSCORMUtils.CheckValidPerformanceInLearnerResponse(lms.DataValue))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Sequencing:
                                    if (TrackingSCORMUtils.CheckValidInLearnerResponse(lms.DataValue, 36,
                                        isOnceElementInArray: true))
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Numeric:
                                    isRealNumber = float.TryParse(lms.DataValue, out float realNumber);
                                    if (isRealNumber)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case InteractionType.Other:
                                    if (TrackingSCORMUtils.IsCMIString4000(lms.DataValue))
                                    {
                                        flag = true;
                                    }
                                    break;
                            }
                            if (flag)
                            {
                                if (count > n)
                                {
                                    scormInteraction.LearnerResponse = lms.DataValue;
                                }
                                else
                                {
                                    await base.AddAsync(new SCORMInteraction
                                    {
                                        N = n,
                                        LearnerResponse = lms.DataValue,
                                        SCORMCoreId = scormCore.Id
                                    });
                                }
                            }
                            else
                            {
                                if (isSCORMVersion12)
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
                if (lms.ErrorCode.Equals(ScormErrorCodes.E0))
                {
                    await applicationDbContext.SaveChangesAsync();
                }
            }
            return lms;
        }
    }
}
