using AutoMapper;
using LMS.Core.Application;
using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Core.Models.RequestModels.UserSurveyRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services
{
    public class UserSurveyService : IUserSurveyService
    {
        private readonly IUserSurveyRepository _userSurveyRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISurveyOptionRepository _surveyOptionRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UserSurveyService(IUserSurveyRepository userSurveyRepository, ISurveyRepository surveyRepository,
            IUnitOfWork unitOfWork, ISurveyOptionRepository surveyOptionRepository, IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _userSurveyRepository = userSurveyRepository;
            _surveyRepository = surveyRepository;
            _unitOfWork = unitOfWork;
            _surveyOptionRepository = surveyOptionRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        private UserSurveyViewModel ConvertToUserSurveyViewModel(UserSurvey userSurvey)
        {
            Survey survey = userSurvey.Survey;
            //convert view model
            //group questions have the same topic
            List<IGrouping<string, UserSurveyDetail>> matrixQuestionList = userSurvey.UserSurveyDetailList
                .Where(usd => usd.SurveyQuestion.Type == SurveyQuestionType.Matrix)
                .GroupBy(usd => usd.SurveyQuestion.BelongTopic)
                .ToList();
            //key is topic, value is list of question belong the topic
            Dictionary<string, List<UserSurveyDetail>> matrixQuestionDic = new();
            Dictionary<string, List<SurveyOption>> matrixOptionDic = new();
            foreach (var group in matrixQuestionList)
            {
                List<UserSurveyDetail> userSurveyDetailList = new();
                List<SurveyOption> options = new();
                foreach (var userSurveyDetail in group)
                {
                    userSurveyDetailList.Add(userSurveyDetail);
                    if (!matrixOptionDic.ContainsKey(group.Key))
                    {
                        options.AddRange(userSurveyDetail.SurveyQuestion.SurveyOptions.ToList());
                    }
                }
                options = options.Distinct(new SurveyOptionComparer()).ToList();
                //key is belongTopic
                matrixQuestionDic.Add(group.Key, userSurveyDetailList);
                matrixOptionDic.Add(group.Key, options);
            }

            List<UserSurveyDetailViewModel> userSurveyDetailViewModelList = new();
            //List<UserSurveyQuestionViewModel> userSurveyQuestionViewModelList = new();
            userSurvey.UserSurveyDetailList.ToList().ForEach(usd =>
            {
                UserSurveyDetailViewModel detailModel = new();

                List<UserSurveyMatrixQuestionViewModel> questionViewModelList = new();
                var surveyQuestion = usd.SurveyQuestion;
                if (surveyQuestion.SurveyOptions == null)
                {
                    usd.SurveyQuestion.SurveyOptions = new List<SurveyOption>();
                }

                bool isAllowedCreatingSurveyQuestionViewModel = false;
                //add user survey detail contains matrix question in the same topic to a list
                //then the list is used to add object UserSurveyDetailViewModel (field Rows)
                if (surveyQuestion.BelongTopic != null && matrixQuestionDic.ContainsKey(surveyQuestion.BelongTopic))
                {
                    List<UserSurveyDetail> userSurveyDetails = matrixQuestionDic[surveyQuestion.BelongTopic];
                    foreach (var userSurveyDetail in userSurveyDetails)
                    {
                        if (userSurveyDetail.SelectedSurveyOptionId != null)
                        {
                            var userSurveyMatrixQuestion = _mapper.Map<UserSurveyMatrixQuestionViewModel>(userSurveyDetail);
                            int selectedOrder = _surveyOptionRepository.FindAsync(userSurveyDetail.SelectedSurveyOptionId.Value)
                                                                       .GetAwaiter().GetResult().Order;
                            userSurveyMatrixQuestion.SelectedOrder = selectedOrder;
                            questionViewModelList.Add(userSurveyMatrixQuestion);
                        }
                    }

                    //questionViewModelList.AddRange(
                    // _mapper.Map<List<UserSurveyMatrixQuestionViewModel>>(matrixQuestionDic[surveyQuestion.BelongTopic]));
                    matrixQuestionDic.Remove(surveyQuestion.BelongTopic);
                    isAllowedCreatingSurveyQuestionViewModel = true;
                }

                if (surveyQuestion.Type == SurveyQuestionType.Matrix && isAllowedCreatingSurveyQuestionViewModel)
                {
                    UserSurveyDetailViewModel detailViewModel = new()
                    {
                        Type = surveyQuestion.Type,
                        Name = surveyQuestion.BelongTopic,
                        Columns = _mapper.Map<List<SurveyMatrixOptionViewModel>>(
                            matrixOptionDic[surveyQuestion.BelongTopic].ToList().OrderBy(s => s.Order)),
                        Rows = questionViewModelList
                    };
                    userSurveyDetailViewModelList.Add(detailViewModel);
                }
                else if (surveyQuestion.Type == SurveyQuestionType.MultipleChoice)
                {
                    UserSurveyDetailViewModel surveyQuestionViewModel = new()
                    {
                        UserSurveyDetailId = usd.Id,
                        SurveyQuestionId = surveyQuestion.Id,
                        Type = surveyQuestion.Type,
                        Name = surveyQuestion.Content,
                        Choices = _mapper.Map<List<SurveyMultipleChoiceOptionViewModel>>(surveyQuestion.SurveyOptions),
                        SelectedSurveyOptionId = usd.SelectedSurveyOptionId
                    };
                    userSurveyDetailViewModelList.Add(surveyQuestionViewModel);
                }
                else if (surveyQuestion.Type == SurveyQuestionType.InputField)
                {
                    UserSurveyDetailViewModel surveyQuestionViewModel = new()
                    {
                        UserSurveyDetailId = usd.Id,
                        SurveyQuestionId = surveyQuestion.Id,
                        Type = surveyQuestion.Type,
                        Name = surveyQuestion.Content,
                        Feedback = usd.Feedback
                    };
                    userSurveyDetailViewModelList.Add(surveyQuestionViewModel);
                }
            });
            var userSurveyViewModel = _mapper.Map<UserSurveyViewModel>(userSurvey);
            userSurveyViewModel.Elements = userSurveyDetailViewModelList;
            return userSurveyViewModel;
        }

        public async Task<SubmitSurveyViewModel> SubmitSurvey(UserSurveyCreateRequestModel model)
        {
            ValidateUtils.CheckDataNotNull("user survey request model", model);
            ValidateUtils.CheckNullOrEmptyList("list of question in survey", model.UserSurveyDetailList);

            //get survey include topic and topic tracking constain the survey
            Survey survey = _surveyRepository.Get(s => s.Id == model.SurveyId)
                                   .Include(s => s.SurveyQuestions)
                                   .ThenInclude(sq => sq.SurveyOptions)
                                   .AsSplitQuery()
                                   .Include(s => s.Topic)
                                   .ThenInclude(t => t.TopicTrackings.Where(tt => tt.UserId == _currentUserService.UserId))
                                   .FirstOrDefault();
            if (survey == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.SurveyIsNotFound,
                    ErrorMessages.SurveyIsNotFound);
            }
            UserSurvey userSurvey = new()
            {
                UserId = _currentUserService.UserId,
                SurveyId = survey.Id,
                SubmitTime = DateTimeOffset.Now
            };
            await _userSurveyRepository.AddAsync(userSurvey);
            await _unitOfWork.SaveChangeAsync();

            List<UserSurveyDetail> userSurveyDetailList = new();
            foreach (var questionModel in model.UserSurveyDetailList)
            {
                var surveyQuestion = survey.SurveyQuestions.Where(sq => sq.Id == questionModel.SurveyQuestionId)
                                                           .FirstOrDefault();
                if (surveyQuestion == null)
                {
                    throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.QuestionInSurveyIsNotFound,
                        ErrorMessages.QuestionInSurveyIsNotFound);
                }

                int? selectedOptionId = null;
                string feedback = null;
                if (surveyQuestion.Type == SurveyQuestionType.MultipleChoice)
                {
                    selectedOptionId = questionModel.SelectedSurveyOptionId;

                    if (selectedOptionId != null)
                    {
                        var selectedSurveyOption = surveyQuestion.SurveyOptions.Where(so => so.Id == selectedOptionId)
                                                                       .FirstOrDefault();
                        if ((await _surveyOptionRepository.FindAsync(selectedOptionId.GetValueOrDefault())) == null)
                        {
                            throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.OptionInSurveyIsNotFound,
                                ErrorMessages.OptionInSurveyIsNotFound);
                        }
                    }
                }
                else if (surveyQuestion.Type == SurveyQuestionType.Matrix)
                {
                    if (questionModel.Order != null)
                    {
                        var selectedSurveyOption = surveyQuestion.SurveyOptions
                            .Where(so => so.Order == questionModel.Order).FirstOrDefault();
                        if (selectedSurveyOption == null)
                        {
                            throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.OptionInSurveyIsNotFound,
                                ErrorMessages.OptionInSurveyIsNotFound);
                        }
                        selectedOptionId = selectedSurveyOption.Id;
                    }
                }
                else
                {
                    feedback = questionModel.Feedback;
                }
                userSurveyDetailList.Add(new UserSurveyDetail
                {
                    UserSurveyId = userSurvey.Id,
                    SurveyQuestionId = questionModel.SurveyQuestionId,
                    SelectedSurveyOptionId = selectedOptionId,
                    Feedback = feedback
                });
            }
            userSurvey.UserSurveyDetailList = userSurveyDetailList;
            await _unitOfWork.SaveChangeAsync();
            //var surveyResult = _mapper.Map<UserSurveyViewModel>(userSurvey);
            var surveyResult = ConvertToUserSurveyViewModel(userSurvey);

            var topic = userSurvey.Survey.Topic;
            var topicTracking = topic.TopicTrackings.First();

            //update CompletedSurveys
            topicTracking.CompletedSurveys++;
            //update topicTracking status
            bool isCompleteAllLearningResourse = topicTracking.CompletedLearningResourses == topic.NumberOfLearningResources;
            bool isCompleteAllQuizzes = topicTracking.CompletedQuizzes == topic.NumberOfQuizzes;
            bool isCompleteAllSurvey = topicTracking.CompletedSurveys == topic.NumberOfSurveys;
            if (isCompleteAllLearningResourse && isCompleteAllQuizzes && isCompleteAllSurvey)
            {
                topicTracking.IsCompleted = true;
            }
            await _unitOfWork.SaveChangeAsync();
            SubmitSurveyViewModel submitSurveyViewModel = new()
            {
                SurveyResult = surveyResult,
                TopicTracking = _mapper.Map<TopicTrackingViewModel>(topicTracking)
            };
            return submitSurveyViewModel;
        }

        public UserSurveyViewModel GetFilledSurvey(int userSurveyId)
        {
            UserSurvey userSurvey = _userSurveyRepository.Get(us => us.Id == userSurveyId)
                                    .AsSplitQuery()
                                    .Include(us => us.Survey)
                                    .Include(us => us.UserSurveyDetailList)
                                    .ThenInclude(usd => usd.SurveyQuestion)
                                    .ThenInclude(q => q.SurveyOptions)
                                    .FirstOrDefault();
            if (userSurvey == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.SurveyIsNotFound,
                    ErrorMessages.SurveyIsNotFound);
            }
            return ConvertToUserSurveyViewModel(userSurvey);
        }

        public async Task<UserSurveyViewModel> UpdateSurveyOfStudent(UserSurveyUpdateRequestModel model)
        {
            UserSurvey userSurvey = _userSurveyRepository.Get(us => us.Id == model.UserSurveyId)
                                                        .AsSplitQuery()
                                                        .Include(us => us.UserSurveyDetailList)
                                                        .ThenInclude(usd => usd.SurveyQuestion)
                                                        .ThenInclude(q => q.SurveyOptions)
                                                        .FirstOrDefault();
            if (userSurvey == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.SurveyIsNotFound,
                    ErrorMessages.SurveyIsNotFound);
            }

            foreach (var userSurveyDetailModel in model.UserSurveyDetailList)
            {
                var userSurveyDetail = userSurvey.UserSurveyDetailList
                                        .Where(usd => usd.SurveyQuestionId == userSurveyDetailModel.SurveyQuestionId)
                                        .FirstOrDefault();
                if (userSurveyDetail.SurveyQuestion.Type == SurveyQuestionType.InputField)
                {
                    userSurveyDetail.Feedback = userSurveyDetailModel.Feedback;
                }
                else if (userSurveyDetail.SurveyQuestion.Type == SurveyQuestionType.MultipleChoice)
                {
                    userSurveyDetail.SelectedSurveyOptionId = userSurveyDetailModel.SelectedSurveyOptionId;
                }
                else if (userSurveyDetail.SurveyQuestion.Type == SurveyQuestionType.Matrix)
                {
                    if (userSurveyDetailModel.Order != null)
                    {
                        SurveyOption surveyOption = userSurveyDetail.SurveyQuestion.SurveyOptions
                            .Where(so => so.Order == userSurveyDetailModel.Order).FirstOrDefault();
                        if (surveyOption != null)
                        {
                            userSurveyDetail.SelectedSurveyOptionId = surveyOption.Id;
                        }
                    }
                }
            }
            if (model.UserSurveyDetailList.Count > 0)
            {
                userSurvey.SubmitTime = DateTimeOffset.Now;
                await _unitOfWork.SaveChangeAsync();
            }
            return ConvertToUserSurveyViewModel(userSurvey);
        }
    }
}
