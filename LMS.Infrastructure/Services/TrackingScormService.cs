using LMS.Core.Application;
using LMS.Core.Common;
using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using static LMS.Core.Common.SCORMConstants;
using static LMS.Core.Common.SCORMConstants.CMIVocabularyTokens;
using LMS.Core.Models.ViewModels;
using AutoMapper;
using LMS.Core.Enum;
using System.Net;

namespace LMS.Infrastructure.Services
{
    public class TrackingScormService : ITrackingScormService
    {
        private readonly ISCORMCoreRepository _scormCoreRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISCORMCommentFromLearnerRepository _cmtFromLearnerRepo;
        private readonly ISCORMCommentFromLMSRepository _cmtFromLMSRepo;
        private readonly ISCORMLearnerPreferenceRepository _learnerPrefRepo;
        private readonly ISCORMInteractionRepository _interactionRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISCORMObjectiveRepository _scormObjectiveRepository;
        private readonly ISCORMNavigationRepository _scormNavRepo;
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public TrackingScormService(ISCORMCoreRepository scormCoreRepository, ICurrentUserService currentUserService,
            ISCORMCommentFromLearnerRepository cmtFromLearnerRepo, ISCORMCommentFromLMSRepository cmtFromLMSRepo,
            ISCORMLearnerPreferenceRepository learnerPrefRepo, ISCORMInteractionRepository interactionRepo,
            IUnitOfWork unitOfWork, ISCORMObjectiveRepository scormObjectiveRepository,
            ISCORMNavigationRepository scormNavRepo, ITopicRepository topicRepository, IMapper mapper)
        {
            _scormCoreRepository = scormCoreRepository;
            _currentUserService = currentUserService;
            _cmtFromLearnerRepo = cmtFromLearnerRepo;
            _cmtFromLMSRepo = cmtFromLMSRepo;
            _learnerPrefRepo = learnerPrefRepo;
            _interactionRepo = interactionRepo;
            _unitOfWork = unitOfWork;
            _scormObjectiveRepository = scormObjectiveRepository;
            _scormNavRepo = scormNavRepo;
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<LMSModel> InitializeSession(LMSModel lms)
        {
            if (lms == null)
            {
                TrackingSCORMUtils.SetArgumentError(ref lms);
                return lms;
            }

            SCORMCore scormCore = await _scormCoreRepository.FindAsync(lms.SCORMCoreId);
            if (scormCore.LearnerId != _currentUserService.UserId)
            {
                TrackingSCORMUtils.SetArgumentError(ref lms);
                return lms;
            }
            //each student has only a first learner attempt
            //meaning if student accessed SCO, entry always is different to "ab-initio"
            if (scormCore.TotalTime == DefaultTotalTime)
            {
                scormCore.Entry = CMIVocabularyTokens.Entry.AbInitio;
            }
            if (scormCore.Exit != null)
            {
                if (scormCore.Exit == CMIVocabularyTokens.Exit.Suspend || scormCore.Exit == CMIVocabularyTokens.Exit.Logout)
                {
                    scormCore.Entry = CMIVocabularyTokens.Entry.Resume;
                }
                else
                {
                    scormCore.Entry = CMIVocabularyTokens.Entry.Empty;
                }
            }
            else
            {
                scormCore.Entry = CMIVocabularyTokens.Entry.Empty;
            }

            await _unitOfWork.SaveChangeAsync();

            TrackingSCORMUtils.InitializeReturnValue(ref lms);
            return lms;
        }

        public async Task<LMSModel> SetValue(LMSModel lms)
        {
            if (lms == null)
            {
                TrackingSCORMUtils.SetArgumentError(ref lms);
                return lms;
            }

            TrackingSCORMUtils.InitializeReturnValue(ref lms);

            SCORMCore scormCore = _scormCoreRepository.Get(sc => sc.Id == lms.SCORMCoreId)
                                                        .AsSplitQuery()
                                                        .Include(sc => sc.TopicSCORM)
                                                        .ThenInclude(ts => ts.SCORM)
                                                        .Include(sc => sc.TopicSCORM)
                                                        .ThenInclude(ts => ts.Topic)
                                                        .ThenInclude(t => t.Course)
                                                        .FirstOrDefault();
            if (scormCore.LearnerId != _currentUserService.UserId)
            {
                TrackingSCORMUtils.SetArgumentError(ref lms);
                return lms;
            }

            //stop update complete_status (SCORM 2004)  and lesson_status (SCORM 1.2) if course completed
            if (scormCore.TopicSCORM != null && scormCore.TopicSCORM.Topic != null )
            {
                CourseProgressStatus courseProgress = GetCourseProgress(scormCore.TopicSCORM.Topic.Course);
                if(courseProgress == CourseProgressStatus.Completed && 
                    (lms.DataItem == CmiCoreLessonStatus || lms.DataItem == CmiCompletionStatus))
                {
                    return lms;
                }
            }

            if (scormCore.TopicSCORM.SCORM.SCORMVersion.Contains(SCORMVersion12))
            {
                lms = await SetValue12(lms, scormCore);
            }
            else
            {
                lms = await SetValue2004(lms, scormCore);
            }

            var topic = _topicRepository.Get(t =>
                                    t.TopicSCORMs.Any(ts => ts.Id == scormCore.TopicSCORMId),
                                    t => t.TopicTrackings.Where(tt => tt.UserId == _currentUserService.UserId))
                                        .AsSplitQuery().FirstOrDefault();
            var topicTracking = topic?.TopicTrackings.FirstOrDefault();
            //update CompletedLearningResources
            if (topicTracking != null && lms.IsUpdateCompletionStatus)
            {
                topicTracking.CompletedLearningResourses++;
                //update topicTracking status
                bool isCompleteAllLearningResourse = topicTracking.CompletedLearningResourses == topic.NumberOfLearningResources;
                bool isCompleteAllQuizzes = topicTracking.CompletedQuizzes == topic.NumberOfQuizzes;
                bool isCompleteAllSurvey = topicTracking.CompletedSurveys == topic.NumberOfSurveys;
                if (isCompleteAllLearningResourse && isCompleteAllQuizzes && isCompleteAllSurvey)
                {
                    topicTracking.IsCompleted = true;
                }
                await _unitOfWork.SaveChangeAsync();
            }
            lms.TopicTracking = _mapper.Map<TopicTrackingViewModel>(topicTracking);
            return lms;
        }

        private CourseProgressStatus GetCourseProgress(Course course)
        {
            if (course == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.CourseNotFound,
                    ErrorMessages.CourseNotFound);
            }
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset startTime = course.StartTime;
            DateTimeOffset endTime = course.EndTime;
            if (now < startTime)
            {
                return CourseProgressStatus.UpComming;
            }
            if (startTime <= now && now <= endTime)
            {
                return CourseProgressStatus.InProgress;
            }
            //endTime < now
            return CourseProgressStatus.Completed;
        }

        public async Task<LMSModel> SetValue12(LMSModel lms, SCORMCore scormCore)
        {
            if (TrackingSCORMUtils.IsKeyword(lms.DataItem))
            {
                TrackingSCORMUtils.SetKeywordDataModelElement12(ref lms);
                return lms;
            }
            else if (TrackingSCORMUtils.IsReadOnly(lms.DataItem))
            {
                TrackingSCORMUtils.SetElementIsReadOnlyError12(ref lms);
                return lms;
            }

            if (lms.DataItem.Contains(CmiCore))
            {
                switch (lms.DataItem)
                {
                    case CmiCoreLessonLocation:
                        if (TrackingSCORMUtils.IsCMIString255(lms.DataValue))
                        {
                            scormCore.Location = lms.DataValue;
                            _scormCoreRepository.Update(scormCore);
                        }
                        else
                        {
                            TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                        }
                        break;
                    case CmiCoreLessonStatus:
                        //if scorm is completed, do not anything
                        if (scormCore.LessonStatus12 != null && scormCore.LessonStatus12 == CoreLessonStatus.Passed)
                        {
                            lms.isStopTracking = true;
                        }
                        else
                        {
                            if (TrackingSCORMUtils.IsCMIVocabulary(CmiCoreLessonStatus, lms.DataValue))
                            {
                                //
                                scormCore.LessonStatus12 = lms.DataValue;
                                SetLessonStatus12(ref scormCore);
                                _scormCoreRepository.Update(scormCore);
                            }
                            else
                            {
                                TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                            }
                        }
                        break;
                    case CmiCoreScoreRaw:
                        bool isFloatNumber = float.TryParse(lms.DataValue, out float result);
                        if (isFloatNumber && result >= 0 && result <= 100)
                        {
                            scormCore.ScoreRaw = lms.DataValue;
                            _scormCoreRepository.Update(scormCore);
                        }
                        else
                        {
                            TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                        }
                        break;
                    case CmiCoreScoreMax:
                        isFloatNumber = float.TryParse(lms.DataValue, out result);
                        if (isFloatNumber && result >= 0 && result <= 100)
                        {
                            scormCore.ScoreMax = lms.DataValue;
                            _scormCoreRepository.Update(scormCore);
                        }
                        else
                        {
                            TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                        }
                        break;
                    case CmiCoreScoreMin:
                        isFloatNumber = float.TryParse(lms.DataValue, out result);
                        if (isFloatNumber && result >= 0 && result <= 100)
                        {
                            scormCore.ScoreMin = lms.DataValue;
                            _scormCoreRepository.Update(scormCore);
                        }
                        else
                        {
                            TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                        }
                        break;
                    case CmiCoreExit:
                        if (TrackingSCORMUtils.IsCMIVocabulary(CmiCoreExit, lms.DataValue))
                        {
                            scormCore.Exit = lms.DataValue;
                            if (scormCore.Exit == CMIVocabularyTokens.Exit.Suspend)
                            {
                                scormCore.Entry = CMIVocabularyTokens.Entry.Resume;
                            }
                            else
                            {
                                scormCore.Entry = CMIVocabularyTokens.Entry.Empty;
                            }
                            _scormCoreRepository.Update(scormCore);
                        }
                        else
                        {
                            TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                        }
                        break;
                    case CmiCoreSessionTime:
                        bool isTimeSpan = TimeSpan.TryParse(lms.DataValue, out TimeSpan sessionTime);
                        if (isTimeSpan)
                        {
                            scormCore.SessionTime = lms.DataValue;
                            _scormCoreRepository.Update(scormCore);
                        }
                        else
                        {
                            TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                        }
                        break;
                }
                if (lms.ErrorCode.Equals(Scorm12ErrorCodes.E0))
                {
                    bool isSaved = await _unitOfWork.SaveChangeAsync();
                    lms.scormCore = scormCore;
                    if (isSaved && lms.DataItem.Equals(CmiCoreLessonStatus) &&
                        (scormCore.LessonStatus12 == CoreLessonStatus.Completed
                        || scormCore.LessonStatus12 == CoreLessonStatus.Passed))
                    {
                        lms.IsUpdateCompletionStatus = true;
                        lms.IsSCORMVersion12 = true;
                    }
                }
            }
            else if (lms.DataItem.Contains(CmiObjectives))
            {
                lms = await _scormObjectiveRepository.SetObjectives(lms, scormCore);
            }
            else if (lms.DataItem.Contains(CmiStudentPreference))
            {
                lms = await _learnerPrefRepo.SetStudentPreference(lms, scormCore);
            }
            else if (lms.DataItem.Contains(CmiInteractions))
            {
                lms = await _interactionRepo.SetInteractions(lms, scormCore, isSCORMVersion12: true);
            }
            else
            {
                switch (lms.DataItem)
                {
                    case CmiSuspendData:
                        SetCmiSuspendData(ref lms, ref scormCore, isSCORM12: true);
                        _scormCoreRepository.Update(scormCore);
                        break;
                    case CmiComments:
                        if (TrackingSCORMUtils.IsCMIString4096(lms.DataValue))
                        {
                            scormCore.Comments12 = lms.DataValue;
                            _scormCoreRepository.Update(scormCore);
                        }
                        else
                        {
                            TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                        }
                        break;
                }
                if (lms.ErrorCode.Equals(ScormErrorCodes.E0))
                {
                    await _unitOfWork.SaveChangeAsync();
                }
            }
            return lms;
        }

        public void SetLessonStatus12(ref SCORMCore scormCore)
        {
            //ScaledPassingScore is similar to mastery_score in SCORM 1.2
            if (scormCore.Credit == CMIVocabularyTokens.Credit.IsCredit)
            {
                if (!string.IsNullOrEmpty(scormCore.ScaledPassingScore) && !string.IsNullOrEmpty(scormCore.ScoreRaw))
                {
                    bool isRealNumber1 = float.TryParse(scormCore.ScaledPassingScore, out float scaledPassingScore);
                    bool isRealNumber2 = float.TryParse(scormCore.ScoreRaw, out float scoreRaw);
                    if (isRealNumber1 && isRealNumber2)
                    {
                        if (scoreRaw >= scaledPassingScore)
                        {
                            scormCore.LessonStatus12 = CoreLessonStatus.Passed;
                        }
                        else
                        {
                            scormCore.LessonStatus12 = CoreLessonStatus.Failed;
                        }
                    }
                }
            }
        }

        public async Task<LMSModel> SetValue2004(LMSModel lms, SCORMCore scormCore)
        {
            if (TrackingSCORMUtils.IsReadOnly(lms.DataItem))
            {
                TrackingSCORMUtils.SetDataModelElementIsReadOnlyError(ref lms);
                return lms;
            }

            if (lms.DataItem.Contains(CmiCommentsFromLearner))
            {
                lms = await _cmtFromLearnerRepo.SetCommentsFromLearner(lms, scormCore);
            }
            else if (lms.DataItem.Contains(CmiObjectives))
            {
                lms = await _scormObjectiveRepository.SetObjectives(lms, scormCore);
            }
            else if (lms.DataItem.Contains(CmiLearnerPreference))
            {
                lms = await _learnerPrefRepo.SetLearnerPreference(lms, scormCore);
            }
            else if (lms.DataItem.Contains(CmiInteractions))
            {
                lms = await _interactionRepo.SetInteractions(lms, scormCore);
            }
            else if (lms.DataItem.Contains(ADLNavigation))
            {
                var navigation = _scormNavRepo.Get(n => n.SCORMCoreId == lms.SCORMCoreId).First();
                switch (lms.DataItem)
                {
                    case ADLNavigationRequest:
                        if (TrackingSCORMUtils.IsNavVocabulary(lms.DataItem, lms.DataValue))
                        {
                            navigation.Request = lms.DataValue;
                            await _unitOfWork.SaveChangeAsync();
                        }
                        else
                        {
                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                        }
                        break;
                }
            }
            else
            {
                //remain cmi
                switch (lms.DataItem)
                {
                    case CmiLocation:
                        if (TrackingSCORMUtils.IsCMIString1000(lms.DataValue))
                        {
                            scormCore.Location = lms.DataValue;
                            _scormCoreRepository.Update(scormCore);
                        }
                        else
                        {
                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                        }
                        break;
                    case CmiCompletionStatus:
                        //if scorm is completed, do not anything
                        if (scormCore.CompletionStatus != null &&
                            scormCore.CompletionStatus == CMIVocabularyTokens.CompletionStatus.Completed)
                        {
                            lms.isStopTracking = true;
                        }
                        else
                        {
                            //ensure set cmi.progress_measure prior to this cmi
                            string completionThreshold = scormCore.CompletionThreshold;
                            string progressMeasure = scormCore.ProgressMeasure;
                            if (TrackingSCORMUtils.IsCMIVocabulary(CmiCompletionStatus, lms.DataValue))
                            {
                                if (completionThreshold == null)
                                {
                                    scormCore.CompletionStatus = lms.DataValue;
                                }
                                else
                                {
                                    if (progressMeasure == null)
                                    {
                                        scormCore.CompletionStatus = lms.DataValue;
                                    }
                                    else
                                    {
                                        float.TryParse(completionThreshold, out float completionThresholdValue);
                                        float.TryParse(progressMeasure, out float progressMeasureValue);
                                        if (completionThresholdValue > progressMeasureValue)
                                        {
                                            scormCore.CompletionStatus = CMIVocabularyTokens.CompletionStatus.InComplete;
                                        }
                                        else
                                        {
                                            scormCore.CompletionStatus = CMIVocabularyTokens.CompletionStatus.Completed;
                                        }
                                    }
                                }
                            }
                            else //No value set by the SCO
                            {
                                if (completionThreshold == null)
                                {
                                    scormCore.CompletionStatus = CMIVocabularyTokens.CompletionStatus.Unknown;
                                }
                                else
                                {
                                    if (progressMeasure == null)
                                    {
                                        scormCore.CompletionStatus = CMIVocabularyTokens.CompletionStatus.Unknown;
                                    }
                                    else
                                    {
                                        float.TryParse(completionThreshold, out float completionThresholdValue);
                                        float.TryParse(progressMeasure, out float progressMeasureValue);
                                        if (completionThresholdValue > progressMeasureValue)
                                        {
                                            scormCore.CompletionStatus = CMIVocabularyTokens.CompletionStatus.InComplete;
                                        }
                                        else
                                        {
                                            scormCore.CompletionStatus = CMIVocabularyTokens.CompletionStatus.Completed;
                                        }
                                    }
                                }
                                TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                            }
                        }
                        break;
                    case CmiExit:
                        if (TrackingSCORMUtils.IsCMIVocabulary(CmiExit, lms.DataValue))
                        {
                            scormCore.Exit = lms.DataValue;
                            if (scormCore.Exit == CMIVocabularyTokens.Exit.Suspend
                                || scormCore.Exit == CMIVocabularyTokens.Exit.Logout)
                            {
                                scormCore.Entry = CMIVocabularyTokens.Entry.Resume;
                            }
                            else
                            {
                                scormCore.Entry = CMIVocabularyTokens.Entry.Empty;
                            }
                        }
                        else
                        {
                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                        }
                        break;
                    case CmiProgressMeasure:
                        float result;
                        bool isFloatNumber = float.TryParse(lms.DataValue, out result);
                        if (isFloatNumber)
                        {
                            if (result >= 0 && result <= 1)
                            {
                                scormCore.ProgressMeasure = lms.DataValue;
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
                    case CmiSuccessStatus:
                        string scaledPassingScore = scormCore.ScaledPassingScore;
                        string scaledScore = scormCore.ScoreScaled;
                        if (TrackingSCORMUtils.IsCMIVocabulary(CmiSuccessStatus, lms.DataValue))
                        {
                            if (scaledPassingScore == null)
                            {
                                scormCore.SuccessStatus = lms.DataValue;
                            }
                            else
                            {
                                if (scaledScore == null)
                                {
                                    scormCore.SuccessStatus = lms.DataValue;
                                }
                                else
                                {
                                    float.TryParse(scaledPassingScore, out float scaledPassingScoreValue);
                                    float.TryParse(scaledScore, out float scaledScoreValue);
                                    if (scaledPassingScoreValue > scaledScoreValue)
                                    {
                                        scormCore.SuccessStatus = CMIVocabularyTokens.SuccessStatus.Failed;
                                    }
                                    else
                                    {
                                        scormCore.SuccessStatus = CMIVocabularyTokens.SuccessStatus.Passed;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (scaledPassingScore == null)
                            {
                                scormCore.SuccessStatus = CMIVocabularyTokens.SuccessStatus.Unknown;
                            }
                            else
                            {
                                if (scaledScore == null)
                                {
                                    scormCore.SuccessStatus = CMIVocabularyTokens.SuccessStatus.Unknown;
                                }
                                else
                                {
                                    float.TryParse(scaledPassingScore, out float scaledPassingScoreValue);
                                    float.TryParse(scaledScore, out float scaledScoreValue);
                                    if (scaledPassingScoreValue > scaledScoreValue)
                                    {
                                        scormCore.SuccessStatus = CMIVocabularyTokens.SuccessStatus.Failed;
                                    }
                                    else
                                    {
                                        scormCore.SuccessStatus = CMIVocabularyTokens.SuccessStatus.Passed;
                                    }
                                }
                            }
                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                        }
                        break;
                    case CmiScoreRaw:
                        isFloatNumber = float.TryParse(lms.DataValue, out result);
                        if (isFloatNumber)
                        {
                            scormCore.ScoreRaw = lms.DataValue;
                        }
                        else
                        {
                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                        }
                        break;
                    case CmiScoreMin:
                        isFloatNumber = float.TryParse(lms.DataValue, out result);
                        if (isFloatNumber)
                        {
                            scormCore.ScoreMin = lms.DataValue;
                        }
                        else
                        {
                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                        }
                        break;
                    case CmiScoreMax:
                        isFloatNumber = float.TryParse(lms.DataValue, out result);
                        if (isFloatNumber)
                        {
                            scormCore.ScoreMax = lms.DataValue;
                        }
                        else
                        {
                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                        }
                        break;
                    case CmiScoreScaled:
                        isFloatNumber = float.TryParse(lms.DataValue, out result);
                        if (isFloatNumber)
                        {
                            if (result >= -1 && result <= 1)
                            {
                                scormCore.ScoreScaled = lms.DataValue;
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
                    case CmiSessionTime:
                        try
                        {
                            XmlConvert.ToTimeSpan(lms.DataValue);
                            scormCore.SessionTime = lms.DataValue;
                        }
                        catch (FormatException)
                        {
                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                        }
                        break;
                    case CmiSuspendData:
                        if (TrackingSCORMUtils.IsCMIString4000(lms.DataValue))
                        {
                            scormCore.SuspendData = lms.DataValue;
                        }
                        else
                        {
                            TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                        }
                        break;
                }

                if (lms.ErrorCode.Equals(ScormErrorCodes.E0))
                {
                    bool isSaved = await _unitOfWork.SaveChangeAsync();
                    lms.scormCore = scormCore;
                    if (isSaved && lms.DataItem.Equals(CmiCompletionStatus) &&
                        scormCore.CompletionStatus == CMIVocabularyTokens.CompletionStatus.Completed)
                    {
                        lms.IsUpdateCompletionStatus = true;
                    }
                }
            }
            return lms;
        }

        private static void SetCmiSuspendData(ref LMSModel lms, ref SCORMCore scormCore, bool isSCORM12 = false)
        {
            if (isSCORM12)
            {
                if (TrackingSCORMUtils.IsCMIString4096(lms.DataValue))
                {
                    scormCore.SuspendData = lms.DataValue;
                }
                else
                {
                    TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
                }
            }
            else
            {
                if (TrackingSCORMUtils.IsCMIString4000(lms.DataValue))
                {
                    scormCore.SuspendData = lms.DataValue;
                }
                else
                {
                    TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                }
            }
        }

        public LMSModel GetValue(LMSModel lms)
        {
            if (lms == null)
            {
                TrackingSCORMUtils.SetArgumentError(ref lms);
                return lms;
            }

            // initialize return value
            TrackingSCORMUtils.InitializeReturnValue(ref lms);

            SCORMCore scormCore = _scormCoreRepository.Get(sc => sc.Id == lms.SCORMCoreId)
                                                        .AsSplitQuery()
                                                        .Include(sc => sc.TopicSCORM)
                                                        .ThenInclude(ts => ts.SCORM)
                                                        .FirstOrDefault();
            if (scormCore == null)
            {
                lms.ErrorCode = ScormErrorCodes.E301;
                lms.ErrorString = ScormErrorStrings.NoCMICore;
                lms.ReturnValue = ScormReturnValues.False;
            }
            else if (scormCore.LearnerId != _currentUserService.UserId)
            {
                TrackingSCORMUtils.SetArgumentError(ref lms);
            }

            if (scormCore.TopicSCORM.SCORM.SCORMVersion.Contains(SCORMVersion12))
            {
                GetValue12(ref lms, ref scormCore);
            }
            else
            {
                GetValue2004(ref lms, ref scormCore);
            }
            return lms;
        }

        public void GetValue12(ref LMSModel lms, ref SCORMCore scormCore)
        {
            if (TrackingSCORMUtils.IsWriteOnly(lms.DataItem))
            {
                TrackingSCORMUtils.SetElementIsWriteOnlyError12(ref lms);
            }
            if (lms.DataItem.Contains(CmiCore))
            {
                switch (lms.DataItem)
                {
                    case CmiCoreChildren:
                        lms.ReturnValue = string.Join(",", StudentId, StudentName, LessonLocation,
                            SCORMConstants.Credit, LessonStatus, SCORMConstants.Entry, Score, TotalTime,
                            SCORMConstants.Exit, SessionTime);
                        break;
                    case CmiCoreStudentId:
                        lms.ReturnValue = scormCore.LearnerId.ToString();
                        break;
                    case CmiCoreStudentName:
                        lms.ReturnValue = scormCore.LearnerName;
                        break;
                    case CmiCoreLessonLocation:
                        lms.ReturnValue = scormCore.Location;
                        break;
                    case CmiCoreCredit:
                        lms.ReturnValue = scormCore.Credit;
                        break;
                    case CmiCoreLessonStatus:
                        lms.ReturnValue = scormCore.LessonStatus12;
                        break;
                    case CmiCoreEntry:
                        lms.ReturnValue = scormCore.Entry;
                        break;
                    case CmiCoreScoreChildren:
                        lms.ReturnValue = string.Join(",", Raw, Min, Max);
                        break;
                    case CmiCoreScoreRaw:
                        lms.ReturnValue = scormCore.ScoreRaw ?? "";
                        break;
                    case CmiCoreScoreMax:
                        lms.ReturnValue = scormCore.ScoreMax ?? "";
                        break;
                    case CmiCoreScoreMin:
                        lms.ReturnValue = scormCore.ScoreMin ?? "";
                        break;
                    case CmiCoreTotalTime:
                        lms.ReturnValue = scormCore.TotalTime ?? "0000:00:00.00";
                        break;
                    case CmiCoreLessonMode:
                        lms.ReturnValue = scormCore.Mode;
                        break;
                }
            }
            else if (lms.DataItem.Contains(CmiStudentData))
            {
                GetStudentData(ref lms, ref scormCore);
            }
            else if (lms.DataItem.IndexOf(CmiObjectives) == 0)
            {
                _scormObjectiveRepository.GetObjective(ref lms, isSCORM12: true);
            }
            else if (lms.DataItem.IndexOf(CmiStudentPreference) == 0)
            {
                _learnerPrefRepo.GetStudentPreference(ref lms);
            }
            else if (lms.DataItem.IndexOf(CmiInteractions) == 0)
            {
                _interactionRepo.GetInteractions(ref lms, isSCORMVersion12: true);
            }
            else
            {
                switch (lms.DataItem)
                {
                    case CmiSuspendData:
                        lms.ReturnValue = scormCore.SuspendData;
                        break;
                    case CmiLaunchData:
                        lms.ReturnValue = scormCore.LaunchData;
                        break;
                    case CmiComments:
                        lms.ReturnValue = scormCore.Comments12 ?? "";
                        break;
                    case CmiCommentsFromLms:
                        lms.ReturnValue = scormCore.CommentsFromLMS12 ?? "";
                        break;
                }
            }

        }

        public void GetValue2004(ref LMSModel lms, ref SCORMCore scormCore)
        {
            if (TrackingSCORMUtils.IsWriteOnly(lms.DataItem))
            {
                lms.ErrorCode = ScormErrorCodes.E405;
                lms.ErrorString = ScormErrorStrings.E405;
                lms.ReturnValue = ScormReturnValues.False;
            }
            else if (lms.DataItem.IndexOf(CmiCommentsFromLearner) == 0)
            {
                _cmtFromLearnerRepo.GetCommentsFromLeanerValue(ref lms);
            }
            else if (lms.DataItem.IndexOf(CmiCommentsFromLms) == 0)
            {
                _cmtFromLMSRepo.GetCommentsFromLMSValue(ref lms);
            }
            else if (lms.DataItem.IndexOf(CmiLearnerPreference) == 0)
            {
                _learnerPrefRepo.GetLearnerPreference(ref lms);
            }
            else if (lms.DataItem.IndexOf(CmiInteractions) == 0)
            {
                _interactionRepo.GetInteractions(ref lms);
            }
            else if (lms.DataItem.IndexOf(CmiObjectives) == 0)
            {
                _scormObjectiveRepository.GetObjective(ref lms);
            }
            else if (lms.DataItem.IndexOf(ADLNavigation) == 0)
            {
                _scormNavRepo.GetNavigation(ref lms);
            }
            else
            {
                switch (lms.DataItem)
                {
                    case CmiVersion:
                        lms.ReturnValue = CMIVocabularyTokens.Version;
                        break;
                    case CmiCompletionStatus:
                        lms.ReturnValue = scormCore.CompletionStatus;
                        break;
                    case CmiCompletionThreshold:
                        lms.ReturnValue = scormCore.CompletionThreshold;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case CmiCredit:
                        lms.ReturnValue = scormCore.Credit;
                        break;
                    case CmiEntry:
                        lms.ReturnValue = scormCore.Entry;
                        break;
                    case CmiLaunchData:
                        lms.ReturnValue = scormCore.LaunchData;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case CmiLearnerId:
                        lms.ReturnValue = scormCore.LearnerId.ToString();
                        break;
                    case CmiLearnerName:
                        lms.ReturnValue = scormCore.LearnerName;
                        break;
                    case CmiLocation:
                        lms.ReturnValue = scormCore.Location;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case CmiMaxTimeAllowed:
                        lms.ReturnValue = scormCore.MaxTimeAllowed;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case CmiMode:
                        lms.ReturnValue = scormCore.Mode;
                        break;
                    case CmiProgressMeasure:
                        lms.ReturnValue = scormCore.ProgressMeasure;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case CmiScaledPassingScore:
                        lms.ReturnValue = scormCore.ScaledPassingScore;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case CmiScoreChildren:
                        lms.ReturnValue = "scaled,min,max,raw";
                        break;
                    case CmiScoreScaled:
                        lms.ReturnValue = scormCore.ScoreScaled;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case CmiScoreRaw:
                        lms.ReturnValue = scormCore.ScoreRaw;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case CmiScoreMin:
                        lms.ReturnValue = scormCore.ScoreMin;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case CmiScoreMax:
                        string scoreMax = scormCore.ScoreMax;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        lms.ReturnValue = scoreMax;
                        break;
                    case CmiSuccessStatus:
                        lms.ReturnValue = scormCore.SuccessStatus;
                        break;
                    case CmiSuspendData:
                        lms.ReturnValue = scormCore.SuspendData;
                        TrackingSCORMUtils.CheckNullOrEmpty(ref lms);
                        break;
                    case CmiTimeLimitAction:
                        lms.ReturnValue = scormCore.TimeLimitAction;
                        break;
                    case CmiTotalTime:
                        lms.ReturnValue = scormCore.TotalTime;
                        break;
                    default:
                        if (lms.DataItem.Contains("_count", StringComparison.CurrentCulture))
                        {
                            lms.ErrorCode = ScormErrorCodes.E301;
                            lms.ErrorString = ScormErrorStrings.CannotHaveCount;
                            lms.ReturnValue = ScormReturnValues.Empty;
                        }
                        else if (lms.DataItem.Contains("_children", StringComparison.CurrentCulture))
                        {
                            lms.ErrorCode = ScormErrorCodes.E301;
                            lms.ErrorString = ScormErrorStrings.CannotHaveChildren;
                            lms.ReturnValue = ScormReturnValues.Empty;
                        }
                        else
                        {
                            TrackingSCORMUtils.SetArgumentError(ref lms);
                        }
                        break;
                }
            }
        }

        private static void GetStudentData(ref LMSModel lms, ref SCORMCore scormCore)
        {
            switch (lms.DataItem)
            {
                case CmiStudentDataChildren:
                    lms.ReturnValue = string.Join(",", MasteryScore, MaxTimeAllowed, TimeLimitAction);
                    break;
                case CmiStudentDataMasteryScore:
                    //MasteryScore <=> MinNormalizedMeasure in manifest <=> ScaledPassingScore in scormCore
                    lms.ReturnValue = scormCore.ScaledPassingScore;
                    break;
                case CmiStudentDataMaxTimeAllowed:
                    lms.ReturnValue = scormCore.MaxTimeAllowed;
                    break;
                case CmiStudentDataTimeLimitAction:
                    lms.ReturnValue = scormCore.TimeLimitAction;
                    break;
            }
        }

        public LMSModel Commit(LMSModel lms)
        {
            if (lms == null)
            {
                TrackingSCORMUtils.SetArgumentError(ref lms);
                return lms;
            }
            TrackingSCORMUtils.InitializeReturnValue(ref lms);
            return lms;
        }

        public async Task<LMSModel> Terminate(LMSModel lms)
        {
            if (lms == null)
            {
                TrackingSCORMUtils.SetArgumentError(ref lms);
                return lms;
            }

            SCORMCore scormCore = _scormCoreRepository.Get(sc => sc.Id == lms.SCORMCoreId)
                                                        .AsSplitQuery()
                                                        .Include(sc => sc.TopicSCORM)
                                                        .ThenInclude(ts => ts.SCORM)
                                                        .FirstOrDefault();
            if (scormCore.LearnerId != _currentUserService.UserId)
            {
                TrackingSCORMUtils.SetArgumentError(ref lms);
                return lms;
            }

            if (scormCore.Exit == CMIVocabularyTokens.Exit.Suspend)
            {
                scormCore.Entry = CMIVocabularyTokens.Entry.Resume;
            }
            else
            {
                scormCore.Entry = CMIVocabularyTokens.Entry.Empty;

                //SCO calls Terminate(“”) without having set cmi.exit to “suspend”,
                //LMS may discard the value of the cmi.suspend_data
                scormCore.SuspendData = null;
            }

            if (scormCore.SessionTime != null)
            {
                //SCORM 1.2
                if (scormCore.TopicSCORM.SCORM.SCORMVersion.Contains(SCORMVersion12))
                {
                    //set total time
                    TimeSpan.TryParse(scormCore.SessionTime, out TimeSpan sessionTime);
                    TimeSpan.TryParse(scormCore.TotalTime, out TimeSpan currentTotalTime);
                    currentTotalTime += sessionTime;
                    scormCore.TotalTime = currentTotalTime.ToString();

                    //set lesson_status
                    scormCore.LessonStatus12 = CoreLessonStatus.Completed;
                    SetLessonStatus12(ref scormCore);
                }
                else
                {
                    //SCORM 2004
                    TimeSpan currentTotalTime = XmlConvert.ToTimeSpan(scormCore.TotalTime);
                    TimeSpan sessionTime = XmlConvert.ToTimeSpan(scormCore.SessionTime);
                    currentTotalTime += sessionTime;
                    scormCore.TotalTime = XmlConvert.ToString(currentTotalTime);
                }
            }

            await _unitOfWork.SaveChangeAsync();
            return lms;
        }
    }
}
