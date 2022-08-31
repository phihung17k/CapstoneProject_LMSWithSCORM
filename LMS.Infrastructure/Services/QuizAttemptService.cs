using AutoMapper;
using Hangfire;
using LMS.Core.Application;
using LMS.Core.Common;
using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Core.Models.QuizHistoryModels;
using LMS.Core.Models.RequestModels.QuizAttemptRequestModel;
using LMS.Core.Models.RequestModels.QuizRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static LMS.Core.Common.SCORMConstants;
using static LMS.Core.Common.SCORMConstants.CMIVocabularyTokens;

namespace LMS.Infrastructure.Services
{
    public class QuizAttemptService : IQuizAttemptService
    {
        private readonly IQuizAttemptRepository _quizAttemptRepository;
        private readonly IUserQuizRepository _userQuizRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizQuestionRepository _quizQuestionRepository;
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITopicSCORMRepository _topicScormRepository;
        private readonly ITopicOtherLearningResourceRepository _topicOLRRepository;

        public QuizAttemptService(IQuizAttemptRepository quizAttemptRepository, IUserQuizRepository userQuizRepository,
            ICurrentUserService currentUserService, IQuizRepository quizRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IQuizQuestionRepository quizQuestionRepository, IUserCourseRepository userCourseRepository, ITopicSCORMRepository topicScormRepository,
            ITopicOtherLearningResourceRepository topicOLRRepository)
        {
            _quizAttemptRepository = quizAttemptRepository;
            _userQuizRepository = userQuizRepository;
            _currentUserService = currentUserService;
            _quizRepository = quizRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _quizQuestionRepository = quizQuestionRepository;
            _userCourseRepository = userCourseRepository;
            _topicScormRepository = topicScormRepository;
            _topicOLRRepository = topicOLRRepository;
        }

        public async Task<QuizAttemptViewModel> CreateQuizAttempt(QuizAttemptRequestModel requestModel)
        {
            var quiz = _quizRepository.FindAsync(requestModel.QuizId).Result;
            if (quiz == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //check whether quiz is not start or close
            if (DateTimeOffset.Compare(quiz.StartTime, DateTimeOffset.Now) > 0)
            {
                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.QuizIsNotStart, ErrorMessages.QuizIsNotStart);
            }
            if (DateTimeOffset.Compare(quiz.EndTime, DateTimeOffset.Now) <= 0)
            {
                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.QuizIsClosed, ErrorMessages.QuizIsClosed);
            }

            Guid userId = _currentUserService.UserId;

            //check quiz restriction
            if (quiz.Restriction != null)
            {
                var restrictions = JsonConvert.DeserializeObject<List<RestrictionModel>>(quiz.Restriction);
                foreach (var restriction in restrictions)
                {
                    if (restriction.Type == RestrictionResourceType.SCORM)
                    {
                        var topicScorm = _topicScormRepository.Get(ts => ts.Id == restriction.TopicResourceId,
                            ts => ts.SCORMCores.Where(sc => sc.LearnerId == userId)).AsSplitQuery().FirstOrDefault();
                        if (topicScorm != null)
                        {
                            if (!topicScorm.SCORMCores.Any())
                            {
                                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.RestrictionError, ErrorMessages.RestrictionError);
                            }
                            var scormCore = topicScorm.SCORMCores.First();
                            if (scormCore.CompletionStatus != CMIVocabularyTokens.CompletionStatus.Completed &&
                                scormCore.LessonStatus12 != CoreLessonStatus.Completed &&
                                scormCore.LessonStatus12 != CoreLessonStatus.Passed)
                            {
                                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.RestrictionError, ErrorMessages.RestrictionError);
                            }
                        }
                    }
                    else if (restriction.Type == RestrictionResourceType.PDF || restriction.Type == RestrictionResourceType.Video)
                    {
                        var topicOLR = _topicOLRRepository.Get(to => to.Id == restriction.TopicResourceId,
                            to => to.OLRTrackings.Where(ot => ot.LearnerId == userId)).AsSplitQuery().FirstOrDefault();
                        if (topicOLR != null)
                        {
                            if (!topicOLR.OLRTrackings.Any() || !topicOLR.OLRTrackings.First().IsCompleted)
                            {
                                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.RestrictionError, ErrorMessages.RestrictionError);
                            }
                        }
                    }
                    else if (restriction.Type == RestrictionResourceType.Quiz)
                    {
                        var quizRestrict = _quizRepository.Get(q => q.Id == restriction.TopicResourceId,
                            q => q.UserQuizzes.Where(uq => uq.UserId == userId)).AsSplitQuery().FirstOrDefault();
                        if (quizRestrict != null)
                        {
                            if (!quizRestrict.UserQuizzes.Any() ||
                                quizRestrict.UserQuizzes.First().Status != CompletionLevelType.Pass)
                            {
                                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.RestrictionError, ErrorMessages.RestrictionError);
                            }
                        }
                    }
                }
            }

            //check userQuiz is exist or not
            var userQuiz = _userQuizRepository.Get(uq => uq.UserId == userId && uq.QuizId == requestModel.QuizId).FirstOrDefault();
            if (userQuiz == null)
            {
                //create new userQuiz
                userQuiz = new UserQuiz
                {
                    UserId = userId,
                    QuizId = requestModel.QuizId
                };
                await _userQuizRepository.AddAsync(userQuiz);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                //get list of user attempts
                var quizAttempts = _quizAttemptRepository.Get(qa => qa.UserQuizId == userQuiz.Id)?.ToList();
                var lastQuizAttempt = quizAttempts.LastOrDefault();
                if (lastQuizAttempt != null)
                {
                    if (lastQuizAttempt.Status == CompletionLevelType.InProgress)
                    {
                        //return current attempt
                        var answerHistory = JsonConvert.DeserializeObject<AnswerHistoryModel>(lastQuizAttempt.AnswerHistory);
                        var quizAttemptViewModel = _mapper.Map<QuizAttemptViewModel>(lastQuizAttempt);
                        _mapper.Map(answerHistory, quizAttemptViewModel);
                        quizAttemptViewModel.TimeLimit = quiz.TimeLimit;
                        return quizAttemptViewModel;
                    }
                    if (quiz.NumberOfAllowedAttempts.HasValue && quizAttempts.Count == quiz.NumberOfAllowedAttempts)
                    {
                        throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.NumOfAttemptIsExceed, ErrorMessages.NumOfAttemptIsExceed);
                    }
                }
            }
            //create new attempt and return to user
            var createModel = new QuizAttemptCreateModel
            {
                UserQuizId = userQuiz.Id,
                Quiz = quiz,
            };
            return await AddQuizAttemptToDatabase(createModel);
        }

        [AutomaticRetry(Attempts = 0)] //background job will not retry when it fail
        public async Task<SubmitQuizViewModel> UpdateQuizAttemptResult(long quizAttemptId, QuizSubmitRequestModel requestModel, bool isAutoSubmit)
        {
            var quizAttempt = _quizAttemptRepository.Get(qa => qa.Id == quizAttemptId)
                .Include(qa => qa.UserQuiz).ThenInclude(uq => uq.Quiz).AsSplitQuery().FirstOrDefault();
            if (quizAttempt == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            //check action type of user
            if (!isAutoSubmit)
            {
                BackgroundJob.Delete(quizAttempt.BackgroundJobId);
            }

            if (quizAttempt.Status != CompletionLevelType.InProgress)
            {
                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.AttemptCompleted, ErrorMessages.AttemptCompleted);
            }
            //update attempt info
            float avgScore = QuizConstants.DecimalPointGrade / quizAttempt.NumberOfQuestions;
            quizAttempt.FinishAt = DateTimeOffset.Now;
            var answerHistory = JsonConvert.DeserializeObject<AnswerHistoryModel>(quizAttempt.AnswerHistory);
            foreach (var question in answerHistory.Questions)
            {
                var questionSubmit = requestModel?.Questions.Where(q => q.QuestionId == question.Id).FirstOrDefault();
                if (question.Type == QuestionType.SingleSelectMultipleChoice)
                {
                    var selectedOptions = questionSubmit?.SelectedOptions;
                    if (selectedOptions != null)
                    {
                        if (selectedOptions.Count > 1)
                        {
                            throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.ValueNotValid, ErrorMessages.ValueNotValid);
                        }
                        if (selectedOptions.Count == 1)
                        {
                            var optionInQuestion = question.Options.Where(o => o.Id == selectedOptions.First().OptionId).FirstOrDefault();
                            if (optionInQuestion != null)
                            {
                                optionInQuestion.IsSelected = true;
                                if (optionInQuestion.IsCorrect)
                                {
                                    question.EarnedMark = QuizConstants.AvgMark;
                                    quizAttempt.Mark += QuizConstants.AvgMark;
                                    question.EarnedScore = (float)Math.Round(avgScore, 2);
                                    quizAttempt.Score += avgScore;
                                    question.CorrectLevel = QuestionCorrectLevel.Correct;
                                }
                                else
                                {
                                    question.CorrectLevel = QuestionCorrectLevel.Incorrect;
                                }
                            }
                        }

                    }
                }
                else if (question.Type == QuestionType.MultiSelectMultipleChoice)
                {
                    //For each correct answer: give a mark equal to 1 divided by the number of correct choices
                    //For each incorrect answer: give a mark to - 1 divided by the number of incorrect choices.
                    // => earned score = earned mark * average score
                    var numOfCorrectChoices = question.Options.Where(o => o.IsCorrect).Count();
                    var numOfInCorrectChoices = question.Options.Where(o => !o.IsCorrect).Count();
                    float avgCorrectMark = QuizConstants.AvgMark / numOfCorrectChoices;
                    float avgInCorrectMark = QuizConstants.AvgMark / numOfInCorrectChoices;
                    float earnedMark = 0;
                    int numOfCorrectAnswer = 0;
                    var selectedOptions = questionSubmit?.SelectedOptions;
                    if (selectedOptions != null && selectedOptions.Count >= 1)
                    {
                        foreach (var selectedOption in selectedOptions)
                        {
                            var optionInQuestion = question.Options.Where(o => o.Id == selectedOption.OptionId).FirstOrDefault();
                            if (optionInQuestion != null)
                            {
                                optionInQuestion.IsSelected = true;
                                if (optionInQuestion.IsCorrect)
                                {
                                    earnedMark += avgCorrectMark;
                                    numOfCorrectAnswer++;
                                }
                                else
                                {
                                    earnedMark -= avgInCorrectMark;
                                }
                            }
                        }

                        //update quiz attempt
                        if (earnedMark < 0)
                        {
                            earnedMark = 0;
                        }
                        question.EarnedMark = (float)Math.Round(earnedMark, 2);
                        quizAttempt.Mark += (float)Math.Round(earnedMark, 2);
                        question.EarnedScore = (float)Math.Round(avgScore * earnedMark, 2);
                        quizAttempt.Score += avgScore * earnedMark;
                        if (earnedMark == QuizConstants.AvgMark)
                        {
                            question.CorrectLevel = QuestionCorrectLevel.Correct;
                        }
                        else if (earnedMark > 0 && earnedMark < QuizConstants.AvgMark)
                        {
                            question.CorrectLevel = QuestionCorrectLevel.PartiallyCorrect;
                        }
                        else if (earnedMark == 0)
                        {
                            question.CorrectLevel = QuestionCorrectLevel.Incorrect;
                        }
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            //round quiz attempt score
            quizAttempt.Score = (float)Math.Round(quizAttempt.Score, 2);

            quizAttempt.AnswerHistory = JObject.FromObject(answerHistory).ToString();
            var passedScore = quizAttempt.UserQuiz.Quiz.PassedScore;
            if (quizAttempt.Score >= (passedScore * QuizConstants.DecimalPointGrade))
            {
                quizAttempt.Status = CompletionLevelType.Pass;
            }
            else
            {
                quizAttempt.Status = CompletionLevelType.Fail;
            }
            await _unitOfWork.SaveChangeAsync();
            var gradingMethod = quizAttempt.UserQuiz.Quiz.GradingMethod;
            int credit = quizAttempt.UserQuiz.Quiz.Credit;
            //update final result (userQuiz)
            var topicTracking = await UpdateFinalQuizResult(quizAttempt.UserQuiz.Id, gradingMethod, passedScore, credit);
            var quizResult = _mapper.Map<QuizResultViewModel>(quizAttempt);
            _mapper.Map(answerHistory, quizResult);

            SubmitQuizViewModel submitQuizViewModel = new()
            {
                QuizResult = quizResult,
                TopicTracking = _mapper.Map<TopicTrackingViewModel>(topicTracking)
            };
            return submitQuizViewModel;
        }

        private async Task<TopicTracking> UpdateFinalQuizResult(long userQuizId, GradingMethodType gradingMethod, float passedScore, int credit)
        {
            //get list of attempt
            var userQuiz = _userQuizRepository.Get(uq => uq.Id == userQuizId, uq => uq.QuizAttempts)
                .Include(uq => uq.Quiz).ThenInclude(q => q.Topic).ThenInclude(t => t.TopicTrackings
                .Where(tt => tt.UserId == _currentUserService.UserId)).First();
            var attempts = userQuiz.QuizAttempts.OrderBy(qa => qa.Id).ToList();
            bool WasPassed = userQuiz.Status == CompletionLevelType.Pass;
            if (gradingMethod == GradingMethodType.LatestGrade)
            {
                var lastAttempt = attempts.Last();
                userQuiz.FinalScore = lastAttempt.Score;
                userQuiz.Status = lastAttempt.Status;
            }
            else if (gradingMethod == GradingMethodType.HighestGrade)
            {
                var hishestAttempt = attempts.OrderByDescending(a => a.Score).First();
                userQuiz.FinalScore = hishestAttempt.Score;
                userQuiz.Status = hishestAttempt.Status;
            }
            else if (gradingMethod == GradingMethodType.AverageGrade)
            {
                float avgScore = attempts.Average(a => a.Score);
                userQuiz.FinalScore = (float)Math.Round(avgScore, 2);
                if (avgScore >= (passedScore * QuizConstants.DecimalPointGrade))
                {
                    userQuiz.Status = CompletionLevelType.Pass;
                }
                else
                {
                    userQuiz.Status = CompletionLevelType.Fail;
                }
            }
            await _unitOfWork.SaveChangeAsync();

            var topic = userQuiz.Quiz.Topic;
            var topicTracking = topic.TopicTrackings.First();

            //update CompletedQuizzes
            if (userQuiz.Status == CompletionLevelType.Pass && !WasPassed)
            {
                topicTracking.CompletedQuizzes++;
            }
            else if (userQuiz.Status == CompletionLevelType.Fail && WasPassed)
            {
                topicTracking.CompletedQuizzes--;
            }
            //await _unitOfWork.SaveChangeAsync();

            //update topicTracking status
            bool isCompleteAllLearningResourse = topicTracking.CompletedLearningResourses == topic.NumberOfLearningResources;
            bool isCompleteAllQuizzes = topicTracking.CompletedQuizzes == topic.NumberOfQuizzes;
            bool isCompleteAllSurvey = topicTracking.CompletedSurveys == topic.NumberOfSurveys;
            if (isCompleteAllLearningResourse && isCompleteAllQuizzes && isCompleteAllSurvey)
            {
                topicTracking.IsCompleted = true;
            }
            else
            {
                topicTracking.IsCompleted = false;
            }
            await _unitOfWork.SaveChangeAsync();

            if (credit > 0)
            {
                //update course tracking
                UpdateCourseTracking(topic.CourseId, userQuiz.UserId);
            }

            return topicTracking;
        }

        private void UpdateCourseTracking(int courseId, Guid userId)
        {
            //1. calculate final score
            var quizsInCourse = _quizRepository.Get(q => q.Topic.CourseId == courseId,
                q => q.UserQuizzes.Where(uq => uq.UserId == userId)).AsSplitQuery().ToList();

            float totalCredit = quizsInCourse.Sum(q => q.Credit);
            if (totalCredit > 0)
            {
                float totalScore = 0;
                int numOfRequiredQuiz = 0;
                int numOfCompletedRequiredQuiz = 0;
                int numOfPassedRequiredQuiz = 0;
                foreach (var quiz in quizsInCourse)
                {
                    if (quiz.Credit > 0) numOfRequiredQuiz++;
                    var finalQuizResult = quiz.UserQuizzes.FirstOrDefault();
                    if (finalQuizResult != null && finalQuizResult.Status != CompletionLevelType.InProgress && quiz.Credit > 0)
                    {
                        numOfCompletedRequiredQuiz++;
                        totalScore += finalQuizResult.FinalScore * quiz.Credit;
                        if (finalQuizResult.Status == CompletionLevelType.Pass)
                        {
                            numOfPassedRequiredQuiz++;
                        }
                    }
                }

                var courseTracking = _userCourseRepository.Get(uc => uc.CourseId == courseId && uc.UserId == userId)
                    .Include(uc => uc.Course).ThenInclude(c => c.Subject).First();

                var finalScore = (float)Math.Round(totalScore / totalCredit, 2);
                //2. update learning progress
                //check if all quiz with credit > 0 (required) are completed or not, if not, learning status still in progress
                if (numOfCompletedRequiredQuiz == numOfRequiredQuiz)
                {
                    //if completed, update course final score (GPA) and finish time
                    courseTracking.FinalScore = finalScore;
                    courseTracking.FinishTime = DateTimeOffset.Now;
                    //check all required quiz are passed or not
                    if (numOfPassedRequiredQuiz == numOfRequiredQuiz)
                    {
                        //update learning status by subject's pass score                   
                        var subjectPassScore = courseTracking.Course.Subject.PassScore;
                        courseTracking.LearningStatus = (courseTracking.FinalScore / QuizConstants.DecimalPointGrade * 100) >= subjectPassScore
                            ? LearningStatus.Passed
                            : LearningStatus.Failed;
                    }
                    else if (numOfPassedRequiredQuiz < numOfRequiredQuiz) //user not passed all required quizs
                    {
                        courseTracking.LearningStatus = LearningStatus.Failed;
                    }
                }
                _unitOfWork.SaveChangeAsync();
            }
        }
        private async Task<QuizAttemptViewModel> AddQuizAttemptToDatabase(QuizAttemptCreateModel createModel)
        {
            DateTimeOffset estimatedFinishTime = new();
            if (createModel.Quiz.TimeLimit != null)
            {
                estimatedFinishTime = DateTimeOffset.Now.Add((TimeSpan)createModel.Quiz.TimeLimit);
            }
            else
            {
                estimatedFinishTime = createModel.Quiz.EndTime;
            }
            var quizQuestions = _quizQuestionRepository.Get(qq => qq.QuizId == createModel.Quiz.Id
            && qq.Question.IsActive != false && qq.Question.IsDeleted != true)
                .OrderBy(qq => qq.Order)
                .Include(qq => qq.Question).ThenInclude(q => q.Options
                .OrderBy(o => o.Id))
                .AsSplitQuery().ToList();
            if (createModel.Quiz.ShuffledQuestion)
            {
                quizQuestions = quizQuestions.OrderBy(q => Guid.NewGuid()).ToList();
            }
            var questions = quizQuestions.Select(qq => qq.Question).ToList();
            if (createModel.Quiz.ShuffledOption)
            {
                questions.ForEach(q =>
                {
                    q.Options = q.Options.OrderBy(o => Guid.NewGuid()).ToList();
                });
            }
            var answerHistoryModel = new AnswerHistoryModel
            {
                QuestionsPerPage = createModel.Quiz.QuestionsPerPage,
                Questions = _mapper.Map<List<QuestionHistoryModel>>(questions)
            };

            for (int i = 0; i < answerHistoryModel.Questions.Count; i++)
            {
                answerHistoryModel.Questions[i].Order = i + 1;
                answerHistoryModel.Questions[i].OriginalOrder = quizQuestions[i].Order;
            }
            var quizAttempt = new QuizAttempt
            {
                StartAt = DateTimeOffset.Now,
                EstimatedFinishTime = estimatedFinishTime,
                Status = CompletionLevelType.InProgress,
                AnswerHistory = JObject.FromObject(answerHistoryModel).ToString(),
                UserQuizId = createModel.UserQuizId,
                NumberOfQuestions = questions.Count
            };
            await _quizAttemptRepository.AddAsync(quizAttempt);
            await _unitOfWork.SaveChangeAsync();

            //add background job for automatic submit
            TimeSpan delayTime = new();
            if (createModel.Quiz.TimeLimit != null)
            {
                delayTime = createModel.Quiz.TimeLimit.Value;
            }
            else
            {
                //auto submit when quiz end
                delayTime = createModel.Quiz.EndTime.Subtract(DateTimeOffset.Now);
            }
            var jobId = BackgroundJob.Schedule(() => UpdateQuizAttemptResult(quizAttempt.Id, null, true), delayTime + TimeSpan.FromSeconds(2));
            quizAttempt.BackgroundJobId = jobId;
            await _unitOfWork.SaveChangeAsync();
            var quizAttemptViewModel = _mapper.Map<QuizAttemptViewModel>(quizAttempt);
            _mapper.Map(answerHistoryModel, quizAttemptViewModel);
            quizAttemptViewModel.TimeLimit = createModel.Quiz.TimeLimit;
            return quizAttemptViewModel;
        }

        public Task<QuizResultViewModel> ReviewOwnQuizAttempt(long quizAttemptId)
        {
            var quizAttempt = _quizAttemptRepository.Get(qa => qa.Id == quizAttemptId
            && qa.UserQuiz.UserId == _currentUserService.UserId)
                .Include(qa => qa.UserQuiz).ThenInclude(uq => uq.Quiz).AsSplitQuery().FirstOrDefault();
            if (quizAttempt == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            if (quizAttempt.Status == CompletionLevelType.InProgress)
            {
                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.AttemptInProgress, ErrorMessages.AttemptInProgress);
            }
            if (DateTimeOffset.Compare(quizAttempt.UserQuiz.Quiz.EndTime, DateTimeOffset.Now) > 0)
            {
                throw new RequestException(HttpStatusCode.MethodNotAllowed, ErrorCodes.QuizIsOpened, ErrorMessages.QuizIsOpened);
            }
            var answerHistory = JsonConvert.DeserializeObject<AnswerHistoryModel>(quizAttempt.AnswerHistory);
            var result = _mapper.Map<QuizResultViewModel>(quizAttempt);
            _mapper.Map(answerHistory, result);
            return Task.FromResult(result);
        }

        public async Task CreateUserQuizWhenQuizEnd(List<Guid> studentIds, Quiz quiz, int courseId)
        {
            foreach(Guid studentId in studentIds)
            {
                var userQuiz = _userQuizRepository.Get(uq => uq.UserId == studentId && uq.QuizId == quiz.Id).FirstOrDefault();
                if (userQuiz == null) //if student didn't attempt quiz
                {
                    userQuiz = new()
                    {
                        QuizId = quiz.Id,
                        UserId = studentId,
                        FinalScore = 0,
                        Status = quiz.PassedScore == 0 ? CompletionLevelType.Pass : CompletionLevelType.Fail
                    };
                    await _userQuizRepository.AddAsync(userQuiz);
                    await _unitOfWork.SaveChangeAsync();

                    if (quiz.Credit > 0)
                    {
                        UpdateCourseTracking(courseId, studentId);
                    }
                }
            }
            
        }
    }
}
