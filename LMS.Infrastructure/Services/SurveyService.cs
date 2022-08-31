using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels.SurveyRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using LMS.Core.Enum;
using LMS.Core.Application;

namespace LMS.Infrastructure.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public SurveyService(ISurveyRepository surveyRepository, ITopicRepository topicRepository, 
            IMapper mapper, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _surveyRepository = surveyRepository;
            _topicRepository = topicRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        private SurveyViewModel ConvertToSurveyViewModel(Survey survey)
        {
            //convert view model
            //group questions have the same topic
            List<IGrouping<string, SurveyQuestion>> matrixQuestionGroupList = survey.SurveyQuestions
                                                .Where(tq => tq.Type == SurveyQuestionType.Matrix)
                                                .GroupBy(tq => tq.BelongTopic).ToList();
            //key is topic, value is list of question belong the topic
            Dictionary<string, List<SurveyQuestion>> matrixQuestionDic = new();
            Dictionary<string, List<SurveyOption>> matrixOptionDic = new();
            foreach (var group in matrixQuestionGroupList)
            {
                List<SurveyQuestion> questions = new();
                List<SurveyOption> options = new();
                foreach (var question in group)
                {
                    questions.Add(question);
                    if (!matrixOptionDic.ContainsKey(group.Key))
                    {
                        options.AddRange(question.SurveyOptions.ToList());
                    }
                }
                options = options.Distinct(new SurveyOptionComparer()).ToList();
                //key is belongTopic
                matrixQuestionDic.Add(group.Key, questions);
                matrixOptionDic.Add(group.Key, options);
            }

            List<SurveyQuestionViewModel> surveyQuestionViewModelList = new();

            survey.SurveyQuestions.ToList().ForEach(sq =>
            {
                List<SurveyMatrixQuestionViewModel> questionViewModelList = new();
                if (sq.SurveyOptions == null)
                {
                    sq.SurveyOptions = new List<SurveyOption>();
                }


                bool isAllowedCreatingSurveyQuestionViewModel = false;
                if (sq.BelongTopic != null && matrixQuestionDic.ContainsKey(sq.BelongTopic))
                {
                    questionViewModelList.AddRange(
                        _mapper.Map<List<SurveyMatrixQuestionViewModel>>(matrixQuestionDic[sq.BelongTopic]));
                    matrixQuestionDic.Remove(sq.BelongTopic);
                    isAllowedCreatingSurveyQuestionViewModel = true;
                }

                if (sq.Type == SurveyQuestionType.Matrix && isAllowedCreatingSurveyQuestionViewModel)
                {
                    SurveyQuestionViewModel surveyQuestionViewModel = new()
                    {
                        Id = sq.Id,
                        Type = sq.Type,
                        Name = sq.BelongTopic,
                        Columns = _mapper.Map<List<SurveyMatrixOptionViewModel>>(matrixOptionDic[sq.BelongTopic].ToList()),
                        Rows = questionViewModelList
                    };
                    surveyQuestionViewModelList.Add(surveyQuestionViewModel);
                }
                else if (sq.Type == SurveyQuestionType.MultipleChoice)
                {
                    SurveyQuestionViewModel surveyQuestionViewModel = new()
                    {
                        Id = sq.Id,
                        Type = sq.Type,
                        Name = sq.Content,
                        Choices = _mapper.Map<List<SurveyMultipleChoiceOptionViewModel>>(sq.SurveyOptions)
                    };
                    surveyQuestionViewModelList.Add(surveyQuestionViewModel);
                }
                else if (sq.Type == SurveyQuestionType.InputField)
                {
                    SurveyQuestionViewModel surveyQuestionViewModel = new()
                    {
                        Id = sq.Id,
                        Type = sq.Type,
                        Name = sq.Content
                    };
                    surveyQuestionViewModelList.Add(surveyQuestionViewModel);
                }
            });

            var surveyViewModel = _mapper.Map<SurveyViewModel>(survey);
            surveyViewModel.Elements = surveyQuestionViewModelList;
            if(survey.UserSurveys != null && survey.UserSurveys.Any())
            {
                surveyViewModel.UserSurveyId = survey.UserSurveys.Where(us => us.UserId == _currentUserService.UserId)
                                                                 .FirstOrDefault()?.Id;
            }
            return surveyViewModel;
        }

        public async Task<SurveyViewModel> CreateSurveyInTopic(SurveyCreateRequestModel requestModel)
        {
            ValidateSurveyInformation(requestModel, requestModel.TopicId);
            List<SurveyQuestion> questionList = new();
            if (requestModel.Elements != null)
            {
                var questionModelList = requestModel.Elements;
                if (questionModelList.Any())
                {
                    questionModelList.ForEach(sqm =>
                    {
                        string validField = sqm.Type == SurveyQuestionType.Matrix ? "topic" : "question name";
                        ValidateUtils.CheckStringNotEmpty(validField, sqm.Name);
                        if (sqm.Type == SurveyQuestionType.Matrix)
                        {
                            ValidateUtils.CheckNullOrEmptyList("list of choice", sqm.Columns);
                            ValidateUtils.CheckNullOrEmptyList("list of matrix question", sqm.Rows);
                            ValidateContentList(sqm.Rows.Select(q => q.Content).ToList());
                            ValidateContentList(sqm.Columns.Select(o => o.Content).ToList(), isOptionContent: true);

                            sqm.Rows.ForEach(sq =>
                            {
                                int i = 1;
                                List<SurveyOption> options = sqm.Columns.ConvertAll(x => new SurveyOption
                                {
                                    Content = x.Content,
                                    Order = i++
                                });

                                questionList.Add(new SurveyQuestion
                                {
                                    BelongTopic = sqm.Name,
                                    Content = sq.Content,
                                    Type = SurveyQuestionType.Matrix,
                                    //SurveyOptions = _mapper.Map<List<SurveyOption>>(sqm.Columns)
                                    SurveyOptions = options
                                });
                            });
                        }
                        else if (sqm.Type == SurveyQuestionType.MultipleChoice)
                        {
                            ValidateUtils.CheckNullOrEmptyList("list of choice", sqm.Choices);
                            ValidateContentList(sqm.Choices.Select(q => q.Content).ToList(), isOptionContent: true);

                            questionList.Add(new SurveyQuestion
                            {
                                Content = sqm.Name,
                                Type = SurveyQuestionType.MultipleChoice,
                                SurveyOptions = _mapper.Map<List<SurveyOption>>(sqm.Choices)
                            });
                        }
                        else
                        {
                            questionList.Add(new SurveyQuestion
                            {
                                Content = sqm.Name,
                                Type = SurveyQuestionType.InputField
                            });
                        }
                    });
                }
            }
            Survey survey = new()
            {
                Name = requestModel.Title,
                Description = requestModel.Description ?? "",
                StartDate = requestModel.StartDate,
                EndDate = requestModel.EndDate,
                SurveyQuestions = questionList,
                TopicId = requestModel.TopicId
            };

            await _surveyRepository.AddAsync(survey);
            //update number of surveys in topic
            Topic topic = await _topicRepository.FindAsync(requestModel.TopicId);
            topic.NumberOfSurveys++;
            await _unitOfWork.SaveChangeAsync();
            return ConvertToSurveyViewModel(survey);
        }

        private void ValidateContentList(List<string> contentList, bool isOptionContent = false)
        {
            //check empty
            bool isEmptyOption = contentList.Any(o => string.IsNullOrEmpty(o?.Trim()));
            if (isEmptyOption)
            {
                if (isOptionContent)
                {
                    throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.OptionIsEmpty,
                    ErrorMessages.OptionIsEmpty);
                }
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.QuestionEmptyContent,
                    ErrorMessages.QuestionEmptyContent);
            }

            //check duplicatie
            bool isDuplicateOption = contentList.GroupBy(o => o.ToLower()).Any(c => c.Count() > 1);
            if (isDuplicateOption)
            {
                if (isOptionContent)
                {
                    throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.OptionsDuplicate,
                    ErrorMessages.OptionsDuplicate);
                }
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.SurveyQuestionDuplicate,
                    ErrorMessages.SurveyQuestionDuplicate);
            }
        }

        public async Task Delete(int surveyId)
        {
            var survey = _surveyRepository.Get(s => s.Id == surveyId, s => s.UserSurveys).FirstOrDefault();
            if (survey == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.SurveyIsNotFound,
                    ErrorMessages.SurveyIsNotFound);
            }
            if (survey.UserSurveys.Any())
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.SurveyIsTaken, 
                    ErrorMessages.SurveyIsTaken);
            }
            var topic = _topicRepository.Get(t => t.Surveys.Select(q => q.Id).Contains(surveyId)).First();
            await _surveyRepository.Remove(surveyId);

            //update number of quizzes in topic           
            topic.NumberOfSurveys--;
            await _unitOfWork.SaveChangeAsync();
        }

        public Task<SurveyViewModel> Get(int surveyId)
        {
            var survey = _surveyRepository.Get(t => t.Id == surveyId)
                                          .Include(t => t.SurveyQuestions)
                                          .ThenInclude(tq => tq.SurveyOptions)
                                          .Include(s => s.UserSurveys)
                                          .AsSplitQuery()
                                          .FirstOrDefault();
            if (survey == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.SurveyIsNotFound,
                    ErrorMessages.SurveyIsNotFound);
            }
            return Task.FromResult(ConvertToSurveyViewModel(survey));
        }

        private void ValidateSurveyInformation(SurveyRequestModel requestModel, int topicId, int surveyId = 0)
        {
            ValidateUtils.CheckStringNotEmpty("title", requestModel.Title);
            ValidateUtils.CheckStringNotEmpty("survey start date", requestModel.StartDate.ToString());
            ValidateUtils.CheckStringNotEmpty("survey end date", requestModel.EndDate.ToString());
            var topic = _topicRepository.Get(t => t.Id == topicId)
                                        .Include(t => t.Surveys)
                                        .AsSplitQuery()
                                        .FirstOrDefault();
            if (topic == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.TopicNotFound, 
                    ErrorMessages.TopicNotFound);
            }
            //check exist survey name
            var isExistedSurveyName = false;
            if (surveyId != 0)
            {
                isExistedSurveyName = topic.Surveys.ToList()
                            .Where(s => s.Id != surveyId &&
                                    s.Name.ToLower() == requestModel.Title.ToLower())
                            .Any();
            }
            else
            {
                isExistedSurveyName = topic.Surveys.ToList()
                            .Where(s => s.Name.ToLower() == requestModel.Title.ToLower())
                            .Any();
            }
            
            if (isExistedSurveyName)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.SurveyNameExisted,
                    ErrorMessages.SurveyNameExisted);
            }
            //check start date, end date to course date
            ValidateUtils.CheckStartEndTime(requestModel.StartDate, requestModel.EndDate);
        }

        public async Task<SurveyViewModel> Update(int surveyId, SurveyUpdateRequestModel requestModel)
        {
            var survey = _surveyRepository.Get(r => r.Id == surveyId)
                                          .Include(s => s.SurveyQuestions)
                                          .ThenInclude(sq => sq.SurveyOptions)
                                          .Include(s => s.UserSurveys)
                                          .AsSplitQuery()
                                          .FirstOrDefault();
            if (survey == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.SurveyIsNotFound,
                    ErrorMessages.SurveyIsNotFound);
            }
            if (survey.UserSurveys.Any())
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.SurveyIsTaken,
                    ErrorMessages.SurveyIsTaken);
            }
            ValidateSurveyInformation(requestModel, survey.TopicId, surveyId);

            if (requestModel.Elements != null && survey.SurveyQuestions != null)
            {
                var questionModelList = requestModel.Elements;
                if (questionModelList.Any() && survey.SurveyQuestions.Any())
                {
                    //update question is existed in db
                    //constraint to add question: id = 0 (default value). if id != 0 => not found
                    foreach (var tqm in questionModelList)
                    {
                        string validField = tqm.Type == SurveyQuestionType.Matrix ? "topic" : "question name";
                        ValidateUtils.CheckStringNotEmpty(validField, tqm.Name);
                        if (tqm.Type == SurveyQuestionType.Matrix)
                        {
                            ValidateUtils.CheckNullOrEmptyList("list of choice", tqm.Columns);
                            ValidateUtils.CheckNullOrEmptyList("list of matrix question", tqm.Rows);
                            ValidateContentList(tqm.Rows.Select(q => q.Content).ToList());
                            ValidateContentList(tqm.Columns.Select(o => o.Content).ToList(), isOptionContent: true);
                            foreach (var tq in tqm.Rows)
                            {
                                //check exist in db
                                var surveyQuestion = survey.SurveyQuestions.Where(dtq => dtq.Id == tq.Id
                                    && dtq.Type == tqm.Type).FirstOrDefault();
                                if (surveyQuestion == null)
                                {
                                    throw new RequestException(HttpStatusCode.BadRequest,
                                            ErrorCodes.QuestionInSurveyIsNotFound, ErrorMessages.QuestionInSurveyIsNotFound);
                                }
                                else
                                {
                                    int i = 1;
                                    List<SurveyOption> options = tqm.Columns.ConvertAll(x => new SurveyOption
                                    {
                                        Content = x.Content,
                                        Order = i++
                                    });
                                    surveyQuestion.Content = tq.Content;
                                    surveyQuestion.BelongTopic = tqm.Name;
                                    //surveyQuestion.SurveyOptions = _mapper.Map<List<SurveyOption>>(tqm.Columns);
                                    surveyQuestion.SurveyOptions = options;
                                }
                            }
                        }
                        else if (tqm.Type == SurveyQuestionType.MultipleChoice)
                        {
                            ValidateUtils.CheckNullOrEmptyList("list of choice", tqm.Choices);
                            ValidateContentList(tqm.Choices.Select(q => q.Content).ToList(), isOptionContent: true);
                            var surveyQuestion = survey.SurveyQuestions.Where(dtq => dtq.Id == tqm.Id
                                    && dtq.Type == tqm.Type).FirstOrDefault();
                            if (surveyQuestion == null)
                            {
                                throw new RequestException(HttpStatusCode.BadRequest, 
                                        ErrorCodes.QuestionInSurveyIsNotFound, ErrorMessages.QuestionInSurveyIsNotFound);
                            }
                            else
                            {
                                //if option is not exist in request and is exist in db, delete option
                                List<SurveyOption> deletedOptionList = surveyQuestion.SurveyOptions
                                                    .Where(to => !tqm.Choices.Where(tom => tom.Id == to.Id).Any())
                                                    .ToList();
                                deletedOptionList.ToList().ForEach(dop => surveyQuestion.SurveyOptions.Remove(dop));
                                //if option is exist in request (id = 0) and is not exist in db, add new
                                //if option is exist in request (id != 0) and is not exist in db, not found
                                //if option is exist in request (id != 0) and db, update option
                                tqm.Choices.ForEach(tom =>
                                {
                                    if (tom.Id == 0)
                                    {
                                        surveyQuestion.SurveyOptions.Add(_mapper.Map<SurveyOption>(tom));
                                    }
                                    else
                                    {
                                        var surveyOption = surveyQuestion.SurveyOptions
                                            .Where(to => to.Id == tom.Id).FirstOrDefault();
                                        if (surveyOption == null)
                                        {
                                            throw new RequestException(HttpStatusCode.BadRequest,
                                                ErrorCodes.OptionNotExist,
                                                ErrorMessages.OptionNotExist);
                                        }
                                        surveyOption.Content = tom.Content;
                                    }
                                });
                                surveyQuestion.Content = tqm.Name;
                                surveyQuestion.SurveyOptions = _mapper.Map<List<SurveyOption>>(tqm.Choices);
                            }
                        }
                        else
                        {
                            var surveyQuestion = survey.SurveyQuestions.Where(dtq => dtq.Id == tqm.Id
                                    && dtq.Type == tqm.Type).FirstOrDefault();
                            if (surveyQuestion == null)
                            {
                                throw new RequestException(HttpStatusCode.BadRequest,
                                        ErrorCodes.QuestionInSurveyIsNotFound, ErrorMessages.QuestionInSurveyIsNotFound);
                            }
                            else
                            {
                                surveyQuestion.Content = tqm.Name;
                            }
                        }
                    }
                }
                else
                {
                    if (!requestModel.Elements.Any() && !survey.SurveyQuestions.Any())
                    {

                    }
                    else
                    {
                        throw new RequestException(HttpStatusCode.BadRequest,
                               ErrorCodes.NumberOfQuestionIsNotMatch, ErrorMessages.NumberOfQuestionIsNotMatch);
                    }
                }
            }
            else
            {
                if(requestModel.Elements == null && survey.SurveyQuestions == null)
                {

                }
                else
                {
                    throw new RequestException(HttpStatusCode.BadRequest,
                           ErrorCodes.NumberOfQuestionIsNotMatch, ErrorMessages.NumberOfQuestionIsNotMatch);
                }
            }
            survey.Name = requestModel.Title;
            survey.Description = requestModel.Description ?? "";
            survey.StartDate = requestModel.StartDate;
            survey.EndDate = requestModel.EndDate;
            await _unitOfWork.SaveChangeAsync();
            return ConvertToSurveyViewModel(survey);
        }

        public SurveyAggregationViewModel GetSurveyAggregation(int surveyId)
        {
            var survey = _surveyRepository.Get(r => r.Id == surveyId)
                                          .Include(s => s.UserSurveys)
                                          .ThenInclude(us => us.UserSurveyDetailList)
                                          .ThenInclude(usd => usd.SurveyQuestion)
                                          .ThenInclude(sq => sq.SurveyOptions)
                                          .AsSplitQuery()
                                          .Include(s => s.Topic)
                                          .ThenInclude(t => t.TopicTrackings)
                                          .Include(s => s.Topic)
                                          .ThenInclude(t => t.Course)
                                          .FirstOrDefault();
            if (survey == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.SurveyIsNotFound,
                    ErrorMessages.SurveyIsNotFound);
            }
            var surveyAggregation = _mapper.Map<SurveyAggregationViewModel>(survey);
            surveyAggregation.TotalAttendees = survey.Topic.Course.NumberOfTrainee;
            int numberOfResponse = 0;
            if(survey.UserSurveys != null && survey.UserSurveys.Any())
            {
                numberOfResponse = survey.UserSurveys.Count();
            }
            surveyAggregation.NumberOfResponses = numberOfResponse;

            //group question id
            List<UserSurveyDetail> listOfUserSurveyDetail = new();
            survey.UserSurveys.ToList().ForEach(us =>
            {
                listOfUserSurveyDetail.AddRange(us.UserSurveyDetailList);
            });
            List<IGrouping<int, UserSurveyDetail>> questionGroupList = 
                listOfUserSurveyDetail.GroupBy(us => us.SurveyQuestionId).ToList();

            List<SurveyQuestionAggregationViewModel> surveyQuestionAggregations = new();
            foreach (var group in questionGroupList)
            {
                //key is option id
                Dictionary<int, SurveyOptionAggregationViewModel> numberOfOptionResponseDic = new();
                List<string> listOfFeedback = new();

                //collect data in question group
                foreach (var userSurveyDetail in group)
                {
                    if (userSurveyDetail.SurveyQuestion.Type != SurveyQuestionType.InputField)
                    {
                        int selectedOptionId = userSurveyDetail.SelectedSurveyOptionId.GetValueOrDefault();
                        if (numberOfOptionResponseDic.ContainsKey(selectedOptionId))
                        {
                            //int numberOfOptionResponse = numberOfOptionResponseDic[selectedOptionId];
                            var optionAggregation = numberOfOptionResponseDic[selectedOptionId];
                            optionAggregation.NumberOfResponses += 1;
                            numberOfOptionResponseDic[selectedOptionId] = optionAggregation;
                        }
                        else
                        {
                            numberOfOptionResponseDic.Add(selectedOptionId, new SurveyOptionAggregationViewModel { 
                                Id = selectedOptionId,
                                Content = userSurveyDetail.SurveyOption.Content,
                                NumberOfResponses = 1
                            });
                        }
                    }
                    else
                    {
                        if(userSurveyDetail.Feedback != null && userSurveyDetail.Feedback.Trim().Length > 0)
                        {
                            listOfFeedback.Add(userSurveyDetail.Feedback);
                        }
                    }
                }

                var surveyQuestion = group.FirstOrDefault().SurveyQuestion;
                //assign number of option response to list of option
                List<SurveyOptionAggregationViewModel> listOfOptionAggregation = new();
                foreach (var option in surveyQuestion.SurveyOptions.OrderBy(so => so.Id))
                {
                    if (numberOfOptionResponseDic.ContainsKey(option.Id))
                    {
                        listOfOptionAggregation.Add(numberOfOptionResponseDic[option.Id]);
                    }
                    else
                    {
                        listOfOptionAggregation.Add(new SurveyOptionAggregationViewModel
                        {
                            Id = option.Id,
                            Content = option.Content,
                            NumberOfResponses = 0
                        });
                    }
                }
                var questionAggregation = new SurveyQuestionAggregationViewModel()
                {
                    Id = group.Key,
                    Content = surveyQuestion.Content,
                    BelongTopic = surveyQuestion.BelongTopic,
                    Type = surveyQuestion.Type,
                    NumberOfResponses = group.Count(),
                    Options = listOfOptionAggregation,
                    ListOfFeedback = listOfFeedback
                };
                surveyQuestionAggregations.Add(questionAggregation);
            }
            surveyAggregation.Questions = surveyQuestionAggregations.OrderBy(sq => sq.Id).ToList();
            return surveyAggregation;
        }

        public Task<PagingViewModel<SurveyManagementViewModel>> GetSurveyList(SurveyPagingRequestModel requestModel, Guid? userId)
        {
            if (requestModel.ActionType != null && userId == null)
            {
                userId = _currentUserService.UserId;
            }
            if (requestModel.EndDate != null)
            {
                //var endTimeWithoutTimezone = DateTimeOffset.Parse(requestModel.EndTime.ToString(), null);
                var hour = ((DateTimeOffset)requestModel.EndDate).Hour;
                if (hour == 0)
                {
                    requestModel.EndDate = ((DateTimeOffset)requestModel.EndDate).Add(TimeSpan.FromHours(24));
                }
            }
            string search = requestModel.Search?.ToLower().Trim();
            var surveys = _surveyRepository.Get(s =>
            (search == null || s.Name.ToLower().Contains(search) ||
            s.Topic.Course.Code.ToLower().Contains(search) || s.Topic.Course.Name.ToLower().Contains(search) ||
            s.Topic.Course.ParentCode.ToLower().Contains(search) || s.Topic.Course.ParentName.ToLower().Contains(search))
            && (requestModel.StartDate == null || s.StartDate >= requestModel.StartDate)
            && (requestModel.EndDate == null || s.EndDate <= requestModel.EndDate)
            && (userId == null || s.Topic.Course.Users.Any(uc => uc.UserId == userId && uc.ActionType != ActionType.Study))
            && (requestModel.ActionType != ActionTypeWithoutStudy.Teach || s.Topic.Course.Users.Any(uc => uc.ActionType == ActionType.Teach && uc.UserId == userId))
            && (requestModel.ActionType != ActionTypeWithoutStudy.Manage || s.Topic.Course.Users.Any(uc => uc.ActionType == ActionType.Manage && uc.UserId == userId)))
                .Include(s => s.Topic).ThenInclude(t => t.Course).ThenInclude(c => c.Users).AsSplitQuery();

            if (!surveys.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //default sort
            surveys = surveys.OrderByDescending(s => s.CreateTime);

            int totalRecord = surveys.Count();
            surveys = surveys.Skip((requestModel.CurrentPage - 1) * requestModel.PageSize).Take(requestModel.PageSize);

            //mapping
            var items = _mapper.Map<List<SurveyManagementViewModel>>(surveys.ToList());
            if (!items.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            return Task.FromResult(new PagingViewModel<SurveyManagementViewModel>
                                           (items, totalRecord,
                                           requestModel.CurrentPage,
                                           requestModel.PageSize));
        }
    }
}