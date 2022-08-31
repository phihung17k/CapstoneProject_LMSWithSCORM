using AutoMapper;
using LMS.Core.Application;
using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Core.Models.QuizHistoryModels;
using LMS.Core.Models.RequestModels.QuizRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static LMS.Core.Common.SCORMConstants;
using static LMS.Core.Common.SCORMConstants.CMIVocabularyTokens;

namespace LMS.Infrastructure.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuestionBankRepository _questionBankRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuizQuestionRepository _quizQuestionRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITopicRepository _topicRepository;
        private readonly IQuizAttemptRepository _quizAttemptRepository;
        private readonly ITopicSCORMRepository _topicScormRepository;
        private readonly ITopicOtherLearningResourceRepository _topicOLRRepository;

        public QuizService(IQuizRepository quizRepository, IQuestionBankRepository questionBankRepository, IMapper mapper,
            IUnitOfWork unitOfWork, IQuizQuestionRepository quizQuestionRepo, ICurrentUserService currentUserService,
            ITopicRepository topicRepository, IQuizAttemptRepository quizAttemptRepository, ITopicSCORMRepository topicScormRepository,
            ITopicOtherLearningResourceRepository topicOLRRepository)
        {
            _quizRepository = quizRepository;
            _questionBankRepository = questionBankRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _quizQuestionRepo = quizQuestionRepo;
            _currentUserService = currentUserService;
            _topicRepository = topicRepository;
            _quizAttemptRepository = quizAttemptRepository;
            _topicScormRepository = topicScormRepository;
            _topicOLRRepository = topicOLRRepository;
        }

        public async Task<QuizPreviewViewModel> CreateQuizInTopic(QuizCreateRequestModel createRequestModel)
        {
            // Validate request data
            var topic = _topicRepository.Get(t => t.Id == createRequestModel.TopicId, t => t.Course, t => t.Quizzes)
                .AsSplitQuery().FirstOrDefault();
            if (topic == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            ValidateUtils.CheckStringNotEmpty(nameof(createRequestModel.Name), createRequestModel.Name);
            //check quiz name exist in topic
            if (topic.Quizzes.Any(q => q.Name.ToLower().Equals(createRequestModel.Name.ToLower())))
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.QuizNameExisted,
                    ErrorMessages.QuizNameExisted);
            }

            if (createRequestModel.TimeLimit != null)
            {
                ValidateUtils.TimeLimitValidate(nameof(createRequestModel.TimeLimit), (TimeSpan)createRequestModel.TimeLimit);
            }
            ValidateUtils.CheckStartEndTime(createRequestModel.StartTime, createRequestModel.EndTime);

            //Check StartTime < Course's StartTime, EndTime > Course's EndTime
            createRequestModel.EndTime = createRequestModel.EndTime.Subtract(TimeSpan.FromSeconds(createRequestModel.EndTime.Second).Add(TimeSpan.FromMilliseconds(createRequestModel.EndTime.Millisecond)));
            createRequestModel.StartTime = createRequestModel.StartTime.Subtract(TimeSpan.FromSeconds(createRequestModel.StartTime.Second).Add(TimeSpan.FromMilliseconds(createRequestModel.StartTime.Millisecond)));
            if (DateTimeOffset.Compare(createRequestModel.StartTime, topic.Course.StartTime) < 0
                || DateTimeOffset.Compare(createRequestModel.EndTime, topic.Course.EndTime) > 0)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.CourseStartEndOutOfRange, ErrorMessages.CourseStartEndOutOfRange);
            }

            if (createRequestModel.QuestionBanks == null || createRequestModel.QuestionBanks.Count == 0)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.DataListIsEmpty, $"{nameof(createRequestModel.QuestionBanks)} is empty");
            }

            //validate restriction
            if (createRequestModel.Restrictions != null && createRequestModel.Restrictions.Any())
            {
                ValidateRestriction(createRequestModel.Restrictions, topic.CourseId, null);
            }

            //get random question from question bank
            List<int> questionIdResult = new();
            foreach (var questionQuiz in createRequestModel.QuestionBanks)
            {
                var questionBank = _questionBankRepository.Get(qb => qb.Id == questionQuiz.QuestionBankId,
                    qb => qb.Questions.Where(q => q.IsActive != false && q.IsDeleted != true))
                    .AsSplitQuery().FirstOrDefault();
                if (questionBank == null)
                {
                    throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.QuestionBankNotExist, ErrorMessages.QuestionBankNotExist);
                }
                if (questionBank.Questions == null || questionBank.NumberOfQuestions < questionQuiz.NumberOfQuestions)
                {
                    throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NumOfQuestionsIsOutOfRange, ErrorMessages.NumOfQuestionsIsOutOfRange);
                }
                var questionIdList = questionBank.Questions.Select(q => q.Id).ToList();
                var randomQuestionIdList = OtherUtils.GetRandomElements(questionIdList, questionQuiz.NumberOfQuestions);
                questionIdResult.AddRange(randomQuestionIdList);
            }
            //create new quiz
            var quiz = _mapper.Map<Quiz>(createRequestModel);

            //set restriction
            quiz.Restriction = JsonConvert.SerializeObject(createRequestModel.Restrictions ?? new List<RestrictionModel>());

            quiz.NumberOfQuestions = questionIdResult.Count;
            quiz.NumberOfActiveQuestions = questionIdResult.Count;
            await _quizRepository.AddAsync(quiz);
            await _unitOfWork.SaveChangeAsync();
            //add question to quiz
            int count = 1;
            IEnumerable<QuizQuestion> quizQuestions = questionIdResult.Select(questionId => new QuizQuestion
            {
                QuestionId = questionId,
                QuizId = quiz.Id,
                Order = count++
            });
            await _quizQuestionRepo.AddRange(quizQuestions);

            //update number of quizzes in topic
            topic.NumberOfQuizzes++;

            await _unitOfWork.SaveChangeAsync();
            quiz = _quizRepository.Get(q => q.Id == quiz.Id).Include(q => q.Questions.OrderBy(qq => qq.Order))
                .ThenInclude(qq => qq.Question).ThenInclude(q => q.Options).AsSplitQuery().First();
            var result = _mapper.Map<QuizPreviewViewModel>(quiz);
            for (int i = 0; i < result.Questions.Count; i++)
            {
                result.Questions[i].Order = quiz.Questions.ElementAt(i).Order;
            }
            result.QuestionBanks = createRequestModel.QuestionBanks;
            result.Restrictions = createRequestModel.Restrictions;
            return result;
        }

        public async Task DeleteQuiz(int quizId)
        {
            var quiz = _quizRepository.Get(q => q.Id == quizId, q => q.Questions).AsSplitQuery().FirstOrDefault();
            if (quiz == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //if quiz started, do not delete
            if (DateTimeOffset.Compare(quiz.StartTime, DateTimeOffset.Now) <= 0)
            {
                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.QuizIsStarted
                    , string.Join(" ", typeof(Quiz).ToString(), ErrorMessages.IsStarted));
            }

            var topic = _topicRepository.Get(t => t.Quizzes.Select(q => q.Id).Contains(quizId)).First();

            await _quizRepository.Remove(quizId);

            //update number of quizzes in topic           
            topic.NumberOfQuizzes--;

            await _unitOfWork.SaveChangeAsync();
        }

        public Task<QuizPreviewViewModel> GetQuizDetail(int quizId)
        {
            var quiz = _quizRepository.Get(q => q.Id == quizId)
                .Include(q => q.Questions.OrderBy(qq => qq.Order))
                .ThenInclude(qq => qq.Question).ThenInclude(q => q.Options).AsSplitQuery().FirstOrDefault();
            if (quiz == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var result = _mapper.Map<QuizPreviewViewModel>(quiz);
            for (int i = 0; i < result.Questions.Count; i++)
            {
                result.Questions[i].Order = quiz.Questions.ElementAt(i).Order;
            }

            //return question banks
            var groupedQuestionList = quiz.Questions.GroupBy(q => q.Question.QuestionBankId)
                .Select(grq => grq.ToList()).ToList();
            List<QuestionQuizCreateRequestModel> questionBanks = groupedQuestionList.Select(questionList
                => new QuestionQuizCreateRequestModel
                {
                    QuestionBankId = questionList.First().Question.QuestionBankId,
                    NumberOfQuestions = questionList.Count
                }).ToList();
            result.QuestionBanks = questionBanks;

            //check what restriction is not existed
            if (quiz.Restriction != null)
            {
                var restrictions = JsonConvert.DeserializeObject<List<RestrictionModel>>(quiz.Restriction);
                List<RestrictionModel> notExistRestrictions = new();
                foreach (var restriction in restrictions)
                {
                    if (restriction.Type == RestrictionResourceType.SCORM)
                    {
                        var topicSCORM = _topicScormRepository.FindAsync(restriction.TopicResourceId).Result;
                        if (topicSCORM == null)
                        {
                            notExistRestrictions.Add(restriction);
                        }
                    }
                    else if (restriction.Type == RestrictionResourceType.PDF || restriction.Type == RestrictionResourceType.Video)
                    {
                        var topicOLR = _topicOLRRepository.FindAsync(restriction.TopicResourceId).Result;
                        if (topicOLR == null)
                        {
                            notExistRestrictions.Add(restriction);
                        }
                    }
                    else if (restriction.Type == RestrictionResourceType.Quiz)
                    {
                        var quizInRestriction = _quizRepository.FindAsync(restriction.TopicResourceId).Result;
                        if (quizInRestriction == null)
                        {
                            notExistRestrictions.Add(restriction);
                        }
                    }
                }

                //remove restriction is not exist to db
                if (notExistRestrictions.Any())
                {
                    foreach (var notExistRestriction in notExistRestrictions)
                    {
                        restrictions.Remove(notExistRestriction);
                    }
                    //update changes
                    quiz.Restriction = JsonConvert.SerializeObject(restrictions);
                    _unitOfWork.SaveChangeAsync();
                }
                result.Restrictions = restrictions;
            }
            else
            {
                result.Restrictions = new();
            }

            return Task.FromResult(result);
        }

        public async Task<QuizPreviewViewModel> UpdateQuiz(int quizId, QuizUpdateRequestModel updateRequestModel)
        {
            var quiz = _quizRepository.Get(q => q.Id == quizId, q => q.Questions).Include(q => q.Topic).ThenInclude(t => t.Course)
                .AsSplitQuery().FirstOrDefault();
            if (quiz == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //if course started, do not update
            if (DateTimeOffset.Compare(quiz.Topic.Course.StartTime, DateTimeOffset.Now) <= 0)
            {
                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.CourseIsStarted, "Course " + ErrorMessages.IsStarted);
            }

            // Validate request data
            ValidateUtils.CheckStringNotEmpty(nameof(updateRequestModel.Name), updateRequestModel.Name);
            //check quiz name exist in topic
            var checkNameExist = _quizRepository.Get(q =>
            q.Id != quizId &&
            q.TopicId == quiz.TopicId &&
            q.Name.ToLower().Equals(updateRequestModel.Name.ToLower())).FirstOrDefault();
            if (checkNameExist != null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.QuizNameExisted, ErrorMessages.QuizNameExisted);
            }

            if (updateRequestModel.TimeLimit != null)
            {
                ValidateUtils.TimeLimitValidate(nameof(updateRequestModel.TimeLimit), (TimeSpan)updateRequestModel.TimeLimit);
            }
            ValidateUtils.CheckStartEndTime(updateRequestModel.StartTime, updateRequestModel.EndTime);
            //Check StartTime < Course's StartTime, EndTime > Course's EndTime
            updateRequestModel.EndTime = updateRequestModel.EndTime.Subtract(TimeSpan.FromSeconds(updateRequestModel.EndTime.Second).Add(TimeSpan.FromMilliseconds(updateRequestModel.EndTime.Millisecond)));
            updateRequestModel.StartTime = updateRequestModel.StartTime.Subtract(TimeSpan.FromSeconds(updateRequestModel.StartTime.Second).Add(TimeSpan.FromMilliseconds(updateRequestModel.StartTime.Millisecond)));
            if (DateTimeOffset.Compare(updateRequestModel.StartTime, quiz.Topic.Course.StartTime) < 0
                || DateTimeOffset.Compare(updateRequestModel.EndTime, quiz.Topic.Course.EndTime) > 0)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.CourseStartEndOutOfRange, ErrorMessages.CourseStartEndOutOfRange);
            }

            //validate restriction
            if (updateRequestModel.Restrictions != null && updateRequestModel.Restrictions.Any())
            {
                ValidateRestriction(updateRequestModel.Restrictions, quiz.Topic.CourseId, quizId);
            }

            //update quiz information           
            _mapper.Map(updateRequestModel, quiz);

            //set restriction
            quiz.Restriction = JsonConvert.SerializeObject(updateRequestModel.Restrictions ?? new List<RestrictionModel>());

            //re-random question
            if (updateRequestModel.IsUpdateQuestion)
            {
                if (updateRequestModel.QuestionBanks.Count == 0 || updateRequestModel.QuestionBanks == null)
                {
                    throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.DataListIsEmpty, $"{nameof(updateRequestModel.QuestionBanks)} is empty");
                }

                //get random question from question bank
                List<int> questionIdResult = new();
                foreach (var questionQuiz in updateRequestModel.QuestionBanks)
                {
                    if (questionQuiz.NumberOfQuestions <= 0)
                    {
                        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ValueNotValid, $"{nameof(questionQuiz.NumberOfQuestions)} must > 0");
                    }
                    var questionBank = _questionBankRepository.Get(qb => qb.Id == questionQuiz.QuestionBankId,
                        qb => qb.Questions.Where(q => q.IsActive != false && q.IsDeleted != true))
                        .AsSplitQuery().FirstOrDefault();
                    if (questionBank == null)
                    {
                        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.QuestionBankNotExist, ErrorMessages.QuestionBankNotExist);
                    }
                    if (questionBank.Questions == null || questionBank.NumberOfQuestions < questionQuiz.NumberOfQuestions)
                    {
                        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NumOfQuestionsIsOutOfRange, ErrorMessages.NumOfQuestionsIsOutOfRange);
                    }
                    var questionIdList = questionBank.Questions.Select(q => q.Id).ToList();
                    var randomQuestionIdList = OtherUtils.GetRandomElements(questionIdList, questionQuiz.NumberOfQuestions);
                    questionIdResult.AddRange(randomQuestionIdList);
                }
                //update new numberOfQuestions
                quiz.NumberOfQuestions = questionIdResult.Count;
                quiz.NumberOfActiveQuestions = questionIdResult.Count;

                //get current questions in quiz
                var currentQuestions = quiz.Questions.AsEnumerable();

                //remove all old curent questions
                _quizQuestionRepo.RemoveRange(currentQuestions);
                var count = 1;
                IEnumerable<QuizQuestion> quizQuestions = questionIdResult.Select(questionId => new QuizQuestion
                {
                    QuestionId = questionId,
                    QuizId = quizId,
                    Order = count++
                }); ;

                //add new questions in quiz
                await _quizQuestionRepo.AddRange(quizQuestions);
            }
            await _unitOfWork.SaveChangeAsync();
            quiz = _quizRepository.Get(q => q.Id == quizId).Include(q => q.Questions)
                .ThenInclude(qq => qq.Question).ThenInclude(q => q.Options).AsSplitQuery().First();
            var result = _mapper.Map<QuizPreviewViewModel>(quiz);
            for (int i = 0; i < result.Questions.Count; i++)
            {
                result.Questions[i].Order = quiz.Questions.ElementAt(i).Order;
            }
            result.Restrictions = updateRequestModel.Restrictions;
            return result;
        }

        public Task<QuizInfoViewModel> GetQuizInformation(int quizId)
        {
            Guid userId = _currentUserService.UserId;
            var quiz = _quizRepository.Get(q => q.Id == quizId)
                .Include(q => q.UserQuizzes.Where(uq => uq.UserId == userId))
                .ThenInclude(uq => uq.QuizAttempts.OrderBy(qa => qa.Id)).AsSplitQuery().FirstOrDefault();
            if (quiz == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            var result = _mapper.Map<QuizInfoViewModel>(quiz);

            //if quiz closed, all attempt can be review
            if (DateTimeOffset.Compare(DateTimeOffset.Now, quiz.EndTime) >= 0)
            {
                result.ReviewAvailable = true;
            }

            result.AttemptAvailable = true;

            //show restriction for student
            result.Restrictions = new();
            if (quiz.Restriction != null)
            {
                var restrictions = JsonConvert.DeserializeObject<List<RestrictionModel>>(quiz.Restriction);
                List<RestrictionTemp> restrictionTemps = new();
                List<RestrictionModel> notExistRestrictions = new(); //if topic resource is deleted, restriction is not exist
                foreach (var restriction in restrictions)
                {
                    if (restriction.Type == RestrictionResourceType.SCORM)
                    {
                        var topicScorm = _topicScormRepository.Get(ts => ts.Id == restriction.TopicResourceId, ts => ts.Topic,
                            ts => ts.SCORMCores.Where(sc => sc.LearnerId == userId)).AsSplitQuery().FirstOrDefault();
                        if (topicScorm != null)
                        {
                            var isCompleted = false;
                            if (topicScorm.SCORMCores.Any())
                            {
                                var scormCore = topicScorm.SCORMCores.First();
                                if (scormCore.CompletionStatus == CMIVocabularyTokens.CompletionStatus.Completed ||
                                    scormCore.LessonStatus12 == CoreLessonStatus.Completed ||
                                    scormCore.LessonStatus12 == CoreLessonStatus.Passed)
                                {
                                    isCompleted = true;
                                }
                            }
                            restrictionTemps.Add(new RestrictionTemp
                            {
                                TopicId = topicScorm.TopicId,
                                ResourceName = topicScorm.SCORMName,
                                TopicName = topicScorm.Topic.Name,
                                IsCompleted = isCompleted
                            });
                        }
                        else
                        {
                            notExistRestrictions.Add(restriction);
                        }
                    }
                    else if (restriction.Type == RestrictionResourceType.PDF || restriction.Type == RestrictionResourceType.Video)
                    {
                        var topicOLR = _topicOLRRepository.Get(to => to.Id == restriction.TopicResourceId, to => to.Topic,
                            to => to.OLRTrackings.Where(ot => ot.LearnerId == userId)).AsSplitQuery().FirstOrDefault();
                        if (topicOLR != null)
                        {
                            var isCompleted = false;
                            if (topicOLR.OLRTrackings.Any())
                            {
                                var olrTracking = topicOLR.OLRTrackings.First();
                                if (olrTracking.IsCompleted)
                                {
                                    isCompleted = true;
                                }
                            }
                            var resultRestrict = result.Restrictions.Where(r => r.TopicName.Equals(topicOLR.Topic.Name)).FirstOrDefault();
                            restrictionTemps.Add(new RestrictionTemp
                            {
                                TopicId = topicOLR.TopicId,
                                ResourceName = topicOLR.OtherLearningResourceName,
                                TopicName = topicOLR.Topic.Name,
                                IsCompleted = isCompleted
                            });
                        }
                        else
                        {
                            notExistRestrictions.Add(restriction);
                        }
                    }
                    else if (restriction.Type == RestrictionResourceType.Quiz)
                    {
                        var quizRestrict = _quizRepository.Get(q => q.Id == restriction.TopicResourceId, q => q.Topic,
                            q => q.UserQuizzes.Where(uq => uq.UserId == userId)).AsSplitQuery().FirstOrDefault();
                        if (quizRestrict != null)
                        {
                            var isCompleted = false;
                            if (quizRestrict.UserQuizzes.Any())
                            {
                                var userQuiz = quizRestrict.UserQuizzes.First();
                                if (userQuiz.Status == CompletionLevelType.Pass)
                                {
                                    isCompleted = true;
                                }
                            }
                            restrictionTemps.Add(new RestrictionTemp
                            {
                                TopicId = quizRestrict.TopicId,
                                ResourceName = quizRestrict.Name,
                                TopicName = quizRestrict.Topic.Name,
                                IsCompleted = isCompleted
                            });
                        }
                        else
                        {
                            notExistRestrictions.Add(restriction);
                        }
                    }
                }

                //remove restriction is not exist to db
                if (notExistRestrictions.Any())
                {
                    foreach (var notExistRestriction in notExistRestrictions)
                    {
                        restrictions.Remove(notExistRestriction);
                    }
                    //update changes
                    quiz.Restriction = JsonConvert.SerializeObject(restrictions);
                    _unitOfWork.SaveChangeAsync();
                }
                //group restriction temp to restriction model
                if (restrictionTemps.Any())
                {
                    var groupedRestrictions = restrictionTemps.GroupBy(r => r.TopicId)
                        .Select(grq => grq.ToList()).ToList();
                    result.Restrictions = groupedRestrictions.Select(restricionList
                        => new RestrictionViewModelForStudent
                        {
                            TopicName = restricionList.First().TopicName,
                            Resources = _mapper.Map<List<ResourceInTopicRestriction>>(restricionList)
                        }).ToList();
                }
            }

            //check quiz is ready for attempt or not
            if (DateTimeOffset.Compare(quiz.StartTime, DateTimeOffset.Now) > 0 ||
                DateTimeOffset.Compare(quiz.EndTime, DateTimeOffset.Now) <= 0)
            {
                result.AttemptAvailable = false;
                return Task.FromResult(result);
            }

            if (quiz.NumberOfAllowedAttempts != null)
            {
                var numOfCompletedAttempt = quiz.UserQuizzes.FirstOrDefault()?
                    .QuizAttempts.Where(qa => qa.Status != CompletionLevelType.InProgress).Count();
                if (numOfCompletedAttempt != null && numOfCompletedAttempt == quiz.NumberOfAllowedAttempts)
                {
                    result.AttemptAvailable = false;
                    return Task.FromResult(result);
                }
            }

            if (result.Restrictions.Any())
            {
                if (result.Restrictions.SelectMany(r => r.Resources).Any(r => r.IsCompleted == false))
                {
                    result.AttemptAvailable = false;
                    return Task.FromResult(result);
                }
            }
            return Task.FromResult(result);
        }

        public Task<QuizReportViewModel> ViewOverallQuizResult(int quizId, QuizReportRequestModel requestModel)
        {
            var quizQuery = _quizRepository.Get(q => q.Id == quizId);
            if (!quizQuery.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            quizQuery = quizQuery.Include(q => q.UserQuizzes).ThenInclude(uq => uq.QuizAttempts)
                .Include(q => q.UserQuizzes).ThenInclude(uq => uq.User)
                .Include(q => q.Questions.OrderBy(qq => qq.Order)).ThenInclude(qq => qq.Question);

            var quiz = quizQuery.First();
            var quizReport = _mapper.Map<QuizReportViewModel>(quiz);
            for (int i = 0; i < quizReport.Questions.Count; i++)
            {
                quizReport.Questions[i].Order = quiz.Questions.ElementAt(i).Order; //mapping order
            }
            IEnumerable<QuizAttempt> quizAttempts = Enumerable.Empty<QuizAttempt>();

            //select quiz attempts
            if (requestModel.WhichTries == NumOfTries.AllTries)
            {
                quizAttempts = quiz.UserQuizzes.SelectMany(uq => uq.QuizAttempts).OrderByDescending(qa => qa.StartAt);
            }
            else if (requestModel.WhichTries == NumOfTries.FirstTries)
            {
                quizAttempts = quiz.UserQuizzes.Select(uq => uq.QuizAttempts.First()).OrderByDescending(qa => qa.StartAt);
            }
            else
            {
                quizAttempts = quiz.UserQuizzes.Select(uq => uq.QuizAttempts.Last()).OrderByDescending(qa => qa.StartAt);
            }
            List<QuizAttemptReportViewModel> quizAttemptReportViewModelList = new();

            //mapping
            foreach (var quizAttempt in quizAttempts)
            {
                var quizAttemptReportViewModel = _mapper.Map<QuizAttemptReportViewModel>(quizAttempt);
                var questionHistory = JsonConvert.DeserializeObject<AnswerHistoryModel>(quizAttempt.AnswerHistory).Questions.OrderBy(q => q.OriginalOrder);
                quizAttemptReportViewModel.QuestionAnswer = _mapper.Map<List<QuestionAnswerReportViewModel>>(questionHistory);
                var user = quizAttempt.UserQuiz.User;
                _mapper.Map(user, quizAttemptReportViewModel);
                quizAttemptReportViewModelList.Add(quizAttemptReportViewModel);
            }
            quizReport.QuizAttempts = quizAttemptReportViewModelList;
            return Task.FromResult(quizReport);
        }

        public Task<StudentQuizResultViewModel> ReviewStudentQuizAttempt(long quizAttemptId)
        {
            var quizAttempt = _quizAttemptRepository.Get(qa => qa.Id == quizAttemptId)
                .Include(qa => qa.UserQuiz).ThenInclude(uq => uq.User).FirstOrDefault();
            if (quizAttempt == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            if (quizAttempt.Status == CompletionLevelType.InProgress)
            {
                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.AttemptInProgress, ErrorMessages.AttemptInProgress);
            }
            var answerHistory = JsonConvert.DeserializeObject<AnswerHistoryModel>(quizAttempt.AnswerHistory);

            answerHistory.Questions = answerHistory.Questions.OrderBy(q => q.OriginalOrder).ToList(); //show question by ordinary order
            foreach (var question in answerHistory.Questions)
            {
                question.Order = question.OriginalOrder; //show original order for teacher/monitor
            }

            var studentQuizResult = _mapper.Map<StudentQuizResultViewModel>(quizAttempt.UserQuiz.User);
            var quizResult = _mapper.Map<QuizResultViewModel>(quizAttempt);
            _mapper.Map(answerHistory, quizResult);
            studentQuizResult.QuizAttemptDetail = quizResult;
            return Task.FromResult(studentQuizResult);
        }

        private void ValidateRestriction(List<RestrictionModel> restrictions, int courseId, int? quizId)
        {
            var topicsInCourse = _topicRepository.Get(t => t.CourseId == courseId, t => t.TopicOtherLearningResources,
                   t => t.TopicSCORMs, t => t.Quizzes).AsSplitQuery().ToList();
            var scorms = topicsInCourse.SelectMany(tc => tc.TopicSCORMs);
            var olrs = topicsInCourse.SelectMany(tc => tc.TopicOtherLearningResources);
            var quizzes = topicsInCourse.SelectMany(tc => tc.Quizzes);
            foreach (var restriction in restrictions)
            {
                if (restriction.Type == RestrictionResourceType.SCORM)
                {
                    if (!scorms.Any(s => s.Id == restriction.TopicResourceId))
                    {
                        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
                    }
                }
                else if (restriction.Type == RestrictionResourceType.PDF || restriction.Type == RestrictionResourceType.Video)
                {
                    if (!olrs.Any(s => s.Id == restriction.TopicResourceId))
                    {
                        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
                    }
                }
                else if (restriction.Type == RestrictionResourceType.Quiz)
                {
                    if (quizId != null && quizId == restriction.TopicResourceId)
                    {
                        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ValueNotValid, ErrorMessages.RestrictionLoop);
                    }
                    if (!quizzes.Any(s => s.Id == restriction.TopicResourceId))
                    {
                        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
                    }
                }
            }
        }

        public Task<List<TopicWithRestrictionViewModel>> GetTopicListForQuizRestriction(int? topicId, int? quizId)
        {
            if (topicId == null && quizId == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ValueNotValid, ErrorMessages.IdNotNull);
            }
            Topic currentTopic = null;
            if (topicId != null)
            {
                currentTopic = _topicRepository.FindAsync((int)topicId).Result;
                if (currentTopic == null)
                {
                    throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.TopicNotFound, ErrorMessages.TopicNotFound);
                }
            }
            List<RestrictionModel> restrictions = new();
            Quiz quiz = null;
            if (quizId != null) //quiz is created, user want to update restriction
            {
                quiz = _quizRepository.Get(q => q.Id == quizId, q => q.Topic).FirstOrDefault();
                if (quiz == null)
                {
                    throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
                }
                currentTopic = quiz.Topic;
                if (quiz.Restriction != null)
                {
                    restrictions = JsonConvert.DeserializeObject<List<RestrictionModel>>(quiz.Restriction);
                }
            }
            //get list of topics in course include current topic and before topic
            var topics = _topicRepository.Get(t => t.Id <= currentTopic.Id && t.CourseId == currentTopic.CourseId, t => t.TopicSCORMs)
            .Include(t => t.Quizzes)
            .Include(t => t.TopicOtherLearningResources).ThenInclude(to => to.OtherLearningResource)
            .AsSplitQuery().OrderBy(t => t.Id).ToList();

            List<TopicWithRestrictionViewModel> result = new();
            foreach (var topic in topics)
            {
                //if quiz is created, just get resource before quiz in the current topic
                var scormResourses = _mapper.Map<List<ResourceWithRestrictionViewModel>>(topic.TopicSCORMs
                    .Where(ts => quiz == null || topic.Id != currentTopic.Id || ts.CreateTime < quiz.CreateTime));
                var olrResources = _mapper.Map<List<ResourceWithRestrictionViewModel>>(topic.TopicOtherLearningResources
                    .Where(to => quiz == null || topic.Id != currentTopic.Id || to.CreateTime < quiz.CreateTime));
                var quizzes = _mapper.Map<List<ResourceWithRestrictionViewModel>>(topic.Quizzes
                    .Where(q => q.Id != quizId &&
                    (quiz == null || topic.Id != currentTopic.Id || q.CreateTime < quiz.CreateTime) &&
                    q.Credit > 0));

                var topicWithRestriction = _mapper.Map<TopicWithRestrictionViewModel>(topic);
                var resources = scormResourses.Concat(olrResources).Concat(quizzes).OrderBy(r => r.CreateTime).ToList();
                if (restrictions.Any())
                {
                    foreach (var resource in resources)
                    {
                        if (restrictions.Any(rt => rt.TopicResourceId == resource.TopicResourceId && rt.Type == resource.Type))
                        {
                            resource.IsChecked = true;
                        }
                    }
                }

                if (resources.Any())
                {
                    topicWithRestriction.Resources = resources;
                    result.Add(topicWithRestriction);
                }
            }
            return Task.FromResult(result);
        }
    }
}
