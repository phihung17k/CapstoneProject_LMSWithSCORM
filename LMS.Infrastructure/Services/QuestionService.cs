using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels.QuestionRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using LMS.Core.Enum;
using LMS.Core.Models.RequestModels.OptionRequestModel;

namespace LMS.Infrastructure.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IOptionRepository _optionRepository;
        private readonly IQuestionBankRepository _questionBankRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuizRepository _quizRepository;

        public QuestionService(IQuestionRepository questionRepository, IOptionRepository optionRepository, 
            IQuestionBankRepository questionBankRepository, IUserRepository userRepository, 
            IMapper mapper, IUnitOfWork unitOfWork, IQuizRepository quizRepository)
        {
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
            _questionBankRepository = questionBankRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _quizRepository = quizRepository;
        }

        private void ValidateQuestion(QuestionRequestModel requestModel, List<OptionRequestModel> options,
            bool isUpdated = false, int id = 0)
        {
            // Check empty option content
            options.ForEach(o => ValidateUtils.CheckStringNotEmpty("option", o.Content));

            // Check content options not duplicate
            if (options.GroupBy(o => o.Content.ToLower()).Any(c => c.Count() > 1))
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.OptionsDuplicate, ErrorMessages.OptionsDuplicate);
            }

            List<Question> duplicatedQuestionList = null;
            if (!isUpdated)
            {
                QuestionCreateRequestModel createdRequestModel = (QuestionCreateRequestModel)requestModel;
                // Check if content have in question bank
                //if duplicate content question and its options is the same content => throw error
                duplicatedQuestionList = _questionRepository.Get(q => 
                                                            q.QuestionBankId == createdRequestModel.QuestionBankId
                                                         && q.Content.ToLower() == createdRequestModel.Content.ToLower()
                                                         && q.IsDeleted != true)
                                                                .Include(q => q.Options)
                                                                .ToList();
            }
            else
            {
                QuestionUpdateRequestModel updatedRequestModel = (QuestionUpdateRequestModel)requestModel;
                // Check if content have in question bank
                //if duplicate content question and its options is the same content => throw error
                duplicatedQuestionList = _questionRepository.Get(q => 
                                                            q.Id != id
                                                         && q.Content.ToLower() == updatedRequestModel.Content.ToLower()
                                                         && q.IsDeleted != true)
                                                                .Include(q => q.Options)
                                                                .ToList();
            }
            if (duplicatedQuestionList != null && duplicatedQuestionList.Any())
            {
                List<string> optionRequestList = options.Select(o => o.Content.Trim().ToLower())
                                                                              .ToList();
                foreach (var duplicatedQuestion in duplicatedQuestionList)
                {
                    List<string> optionDbList = duplicatedQuestion.Options.Select(o => o.Content.Trim().ToLower())
                                                                                 .ToList();
                    if (!optionRequestList.Except(optionDbList).Any() && !optionDbList.Except(optionRequestList).Any())
                    {
                        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.QuestionIsExisted,
                            ErrorMessages.QuestionIsExisted);
                    }
                }
            }

            //check correct option
            int correctOptions = options.Where(o => o.IsCorrect).Count();
            if (correctOptions < 1)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.CorrectOptionNotExist,
                    ErrorMessages.CorrectOption);
            }
            if (requestModel.Type == QuestionType.SingleSelectMultipleChoice && correctOptions > 1)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.OverManyCorrectOptions,
                    ErrorMessages.OverManyCorrectOptions);
            }
        }

        

        public async Task<QuestionViewModel> CreateQuestion(QuestionCreateRequestModel requestModel)
        {
            // Validate request data
            ValidateUtils.CheckStringNotEmpty("Question content", requestModel.Content);
            ValidateUtils.CheckStringNotEmpty("Question in questionBank", requestModel.QuestionBankId + "");
            ValidateUtils.CheckStringNotEmpty("Question type", requestModel.Type.ToString());
            ValidateUtils.CheckNullOrEmptyList("Question options", requestModel.Options);

            // Check exist question bank
            var checkExistQuestionBank = await _questionBankRepository.FindAsync(requestModel.QuestionBankId);
            if (checkExistQuestionBank == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.QuestionBankNotExist, ErrorMessages.QuestionBankNotExist);
            }

            ValidateQuestion(requestModel, requestModel.Options.Cast<OptionRequestModel>().ToList());

            //convert base64 (if any) to file and save 
            string newQuestionContent = FileUtils.ConvertAndStoreImage(requestModel.Content);

            // Create question
            List<Option> options = new();
            foreach (var optionModel in requestModel.Options)
            {
                string newOptionContent = FileUtils.ConvertAndStoreImage(optionModel.Content);
                var option = new Option
                {
                    Content = newOptionContent,
                    IsCorrect = optionModel.IsCorrect
                };
                options.Add(option);
            }
            //var question = _mapper.Map<Question>(requestModel);

            var question = new Question
            {
                Content = newQuestionContent,
                QuestionBankId = requestModel.QuestionBankId,
                Options = options,
                Type = requestModel.Type
            };

            await _questionRepository.AddAsync(question);
            checkExistQuestionBank.NumberOfQuestions++;
            await _unitOfWork.SaveChangeAsync();
            var result = _mapper.Map<QuestionViewModel>(question);
            var user = _userRepository.FindAsync(result.CreateBy).Result;
            result.CreatedByName = user.LastName + " " + user.FirstName;

            return result;
        }

        public async Task<QuestionViewModel> UpdateQuestion(int id, QuestionUpdateRequestModel requestModel)
        {
            ValidateUtils.CheckStringNotEmpty("question", requestModel.Content);
            ValidateUtils.CheckStringNotEmpty("Question type", requestModel.Type.ToString());
            ValidateUtils.CheckNullOrEmptyList("Question options", requestModel.Options);
            bool isExistQuestion = _questionRepository.Get(q => q.Id == id).Any();
            if (!isExistQuestion)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            var options = requestModel.Options;
            ValidateQuestion(requestModel, requestModel.Options.Cast<OptionRequestModel>().ToList(), 
                isUpdated: true, id: id);

            try
            {
                //update question
                Question question = _questionRepository.Get(q => q.Id == id && q.IsDeleted != true, q => q.Options).First();
                
                //convert base64 (if any) to file and save 
                string newQuestionContent = FileUtils.ConvertAndStoreImage(requestModel.Content, 
                    oldContent: question.Content, isUpdate: true);

                question.Content = newQuestionContent;
                question.IsActive = requestModel.IsActive;
                question.Type = requestModel.Type;
                
                _questionRepository.Update(question);

                var requestOptions = _mapper.Map<List<Option>>(requestModel.Options);
                requestOptions.ForEach(o => o.QuestionId = question.Id);
                List<Option> exceptOptions = question.Options.Except(requestOptions, new OptionComparer()).ToList();
                if (exceptOptions.Count > 0)
                {
                    _optionRepository.RemoveRange(exceptOptions);
                }

                foreach (var requestOption in requestOptions)
                {
                    Option option = await _optionRepository.FindAsync(requestOption.Id);
                    if (option != null)
                    {
                        string newOptionContent = FileUtils.ConvertAndStoreImage(requestOption.Content,
                            oldContent: option.Content, isUpdate: true);
                        option.Content = newOptionContent;
                        option.IsCorrect = requestOption.IsCorrect;
                    }
                    else
                    {
                        string newOptionContent = FileUtils.ConvertAndStoreImage(requestOption.Content);
                        await _optionRepository.AddAsync(new Option
                        {
                            Content = newOptionContent,
                            IsCorrect = requestOption.IsCorrect,
                            QuestionId = question.Id
                        });
                    }
                }

                await _unitOfWork.SaveChangeAsync();
                var result = _mapper.Map<QuestionViewModel>(question);
                var user = _userRepository.FindAsync(result.CreateBy).Result;
                result.CreatedByName = user.LastName + " " + user.FirstName;
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<QuestionViewModel> GetDetail(int questionId)
        {
            var question = _questionRepository.Get(r => r.Id == questionId && r.IsDeleted != true, r => r.Options).FirstOrDefault();
            if (question == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            return Task.FromResult(_mapper.Map<QuestionViewModel>(question));
        }

        public async Task Delete(int questionId)
        {
            var question = _questionRepository.Get(q => q.Id == questionId && q.IsDeleted != true, 
                q => q.QuestionBank, q => q.QuizQuestions)
                .AsSplitQuery().FirstOrDefault();
            if (question == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            if (question.QuizQuestions.Any())
            {
                question.IsDeleted = true;
            } else
            {
                await _questionRepository.Remove(questionId);
            }
            question.QuestionBank.NumberOfQuestions -= 1;

            await _unitOfWork.SaveChangeAsync();

            //update numOfQuestion in Quiz
            if (question.QuizQuestions.Any())
            {
                await UpdateNumOfQuestionsInQuiz(questionId);
            }
        }

        public Task<PagingViewModel<QuestionViewModelWithoutOptions>> Search(QuestionPagingRequestModel questionPagingRequestModel)
        {
            ValidateUtils.CheckStringNotEmpty("Question.QuestionId", questionPagingRequestModel.QuestionBankId + "");
            var resultByCondition = _questionRepository.Get(q =>
                    (questionPagingRequestModel.Content != null ? q.Content.ToLower().Contains(questionPagingRequestModel.Content.ToLower()) : true)
                    && q.QuestionBankId == questionPagingRequestModel.QuestionBankId
                    && q.IsDeleted != true
                    && (questionPagingRequestModel.CreatedBy != null ? (q.CreateBy == questionPagingRequestModel.CreatedBy) : true));
            if (!resultByCondition.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var result = resultByCondition.OrderByDescending(q => q.CreateTime)
                                            .Skip((questionPagingRequestModel.CurrentPage - 1) * questionPagingRequestModel.PageSize)
                                            .Take(questionPagingRequestModel.PageSize);
            if (!result.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var items = _mapper.Map<List<QuestionViewModelWithoutOptions>>(result.ToList());
            items = items.Select(i =>
            {
                var user = _userRepository.FindAsync(i.CreateBy).Result;
                i.CreatedByName = user.LastName + " " + user.FirstName;
                return i;
            }).ToList();
            return Task.FromResult(new PagingViewModel<QuestionViewModelWithoutOptions>
                                            (items, resultByCondition.Count(),
                                            questionPagingRequestModel.CurrentPage,
                                            questionPagingRequestModel.PageSize));
        }

        public async Task<QuestionViewModelWithoutOptions> UpdateStatus(int id, bool isActive)
        {
            Question question = _questionRepository.Get(q => q.Id == id && q.IsDeleted != true).FirstOrDefault();
            if (question == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.QuestionNotFound,
                    ErrorMessages.QuestionNotFound);
            }
            question.IsActive = isActive;
            await _unitOfWork.SaveChangeAsync();

            //update numOfQuestion in Quiz
            await UpdateNumOfQuestionsInQuiz(id);

            return _mapper.Map<QuestionViewModelWithoutOptions>(question);
        }

        private async Task UpdateNumOfQuestionsInQuiz(int questionId)
        {
            //get quiz that have this question
            var quizs = _quizRepository.Get(q => q.Questions.Select(qq => qq.QuestionId).Contains(questionId))
                .Include(q => q.Questions).ThenInclude(qq => qq.Question).AsSplitQuery().ToList();
            if (quizs != null)
            {
                foreach (var quiz in quizs)
                {
                    var availableQuestions = quiz.Questions.Where(qq => qq.Question.IsActive != false && qq.Question.IsDeleted != true);
                    quiz.NumberOfActiveQuestions = availableQuestions.Count();
                }
                await _unitOfWork.SaveChangeAsync();
            }
        }
    }
}