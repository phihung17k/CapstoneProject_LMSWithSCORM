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
using LMS.Core.Models.RequestModels.TemplateRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using LMS.Core.Enum;

namespace LMS.Infrastructure.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TemplateService(ITemplateRepository templateRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _templateRepository = templateRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        private TemplateViewModel ConvertToTemplateViewModel(Template template)
        {
            //convert view model
            //group questions have the same topic
            List<IGrouping<string, TemplateQuestion>> matrixQuestionGroupList = template.TemplateQuestions
                                                .Where(tq => tq.Type == SurveyQuestionType.Matrix)
                                                .GroupBy(tq => tq.BelongTopic).ToList();
            //key is topic, value is list of question belong the topic
            Dictionary<string, List<TemplateQuestion>> matrixQuestionDic = new();
            Dictionary<string, List<TemplateOption>> matrixOptionDic = new();
            foreach (var group in matrixQuestionGroupList)
            {
                List<TemplateQuestion> questions = new();
                List<TemplateOption> options = new();
                foreach (var question in group)
                {
                    questions.Add(question);
                    if (!matrixOptionDic.ContainsKey(group.Key))
                    {
                        options.AddRange(question.TemplateOptions.ToList());
                    }
                }
                options = options.Distinct(new TemplateOptionComparer()).ToList();
                //key is belongTopic
                matrixQuestionDic.Add(group.Key, questions);
                matrixOptionDic.Add(group.Key, options);
            }

            List<TemplateQuestionViewModel> templateQuestionViewModelList = new();
            
            template.TemplateQuestions.ToList().ForEach(tq =>
            {
                List<TemplateMatrixQuestionViewModel> questionViewModelList = new();
                if (tq.TemplateOptions == null)
                {
                    tq.TemplateOptions = new List<TemplateOption>();
                }


                bool isAllowedCreatingTemplateQuestionViewModel = false;
                if (tq.BelongTopic != null && matrixQuestionDic.ContainsKey(tq.BelongTopic))
                {
                    questionViewModelList.AddRange(
                        _mapper.Map<List<TemplateMatrixQuestionViewModel>>(matrixQuestionDic[tq.BelongTopic]));
                    matrixQuestionDic.Remove(tq.BelongTopic);
                    isAllowedCreatingTemplateQuestionViewModel = true;
                }

                if (tq.Type == SurveyQuestionType.Matrix && isAllowedCreatingTemplateQuestionViewModel)
                {
                    TemplateQuestionViewModel templateQuestionViewModel = new()
                    {
                        Id = tq.Id,
                        Type = tq.Type,
                        Name = tq.BelongTopic,
                        Columns = _mapper.Map<List<TemplateMatrixOptionViewModel>>(matrixOptionDic[tq.BelongTopic].ToList()),
                        Rows = questionViewModelList
                    };
                    templateQuestionViewModelList.Add(templateQuestionViewModel);
                }
                else if(tq.Type == SurveyQuestionType.MultipleChoice)
                {
                    TemplateQuestionViewModel templateQuestionViewModel = new()
                    {
                        Id = tq.Id,
                        Type = tq.Type,
                        Name = tq.Content,
                        Choices = _mapper.Map<List<TemplateMultipleChoiceOptionViewModel>>(tq.TemplateOptions)
                    };
                    templateQuestionViewModelList.Add(templateQuestionViewModel);
                }
                else if(tq.Type == SurveyQuestionType.InputField)
                {
                    TemplateQuestionViewModel templateQuestionViewModel = new()
                    {
                        Id = tq.Id,
                        Type = tq.Type,
                        Name = tq.Content
                    };
                    templateQuestionViewModelList.Add(templateQuestionViewModel);
                }
            });

            var templateViewModel = _mapper.Map<TemplateViewModel>(template);
            templateViewModel.Elements = templateQuestionViewModelList;
            return templateViewModel;
        }

        public async Task<TemplateViewModel> CreateTemplate(TemplateRequestModel requestModel)
        {
            ValidateUtils.CheckStringNotEmpty("title", requestModel.Title);
            var isExistedTemplateName = _templateRepository
                            .Get(t => t.Name.ToLower() == requestModel.Title.ToLower())
                            .Any();
            if (isExistedTemplateName)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.TemplateNameExisted,
                    ErrorMessages.TemplateNameExisted);
            }
            var questionList = GetTemplateQuestions(requestModel);
            Template template = new() { 
                Name = requestModel.Title,
                Description = requestModel.Description ?? "",
                TemplateQuestions = questionList
            };
            
            await _templateRepository.AddAsync(template);
            await _unitOfWork.SaveChangeAsync();
            return ConvertToTemplateViewModel(template);
        }

        private List<TemplateQuestion> GetTemplateQuestions(TemplateRequestModel requestModel)
        {
            List<TemplateQuestion> questionList = new();
            if (requestModel.Elements != null)
            {
                var questionModelList = requestModel.Elements;
                if (questionModelList.Any())
                {
                    questionModelList.ForEach(tqm =>
                    {
                        string validField = tqm.Type == SurveyQuestionType.Matrix ? "topic" : "question name";
                        ValidateUtils.CheckStringNotEmpty(validField, tqm.Name);
                        if (tqm.Type == SurveyQuestionType.Matrix)
                        {
                            ValidateUtils.CheckNullOrEmptyList("list of choice", tqm.Columns);
                            ValidateUtils.CheckNullOrEmptyList("list of matrix question", tqm.Rows);
                            ValidateContentList(tqm.Rows.Select(q => q.Content).ToList());
                            ValidateContentList(tqm.Columns.Select(o => o.Content).ToList(), isOptionContent: true);

                            tqm.Rows.ForEach(tq =>
                            {
                                int i = 1;
                                List<TemplateOption> options = tqm.Columns.ConvertAll(x => new TemplateOption { 
                                    Content = x.Content,
                                    Order = i++
                                });

                                questionList.Add(new TemplateQuestion
                                {
                                    BelongTopic = tqm.Name,
                                    Content = tq.Content,
                                    Type = SurveyQuestionType.Matrix,
                                    //TemplateOptions = _mapper.Map<List<TemplateOption>>(tqm.Columns)
                                    TemplateOptions = options
                                });
                            });
                        }
                        else if (tqm.Type == SurveyQuestionType.MultipleChoice)
                        {
                            ValidateUtils.CheckNullOrEmptyList("list of choice", tqm.Choices);
                            ValidateContentList(tqm.Choices.Select(q => q.Content).ToList(), isOptionContent: true);

                            questionList.Add(new TemplateQuestion
                            {
                                Content = tqm.Name,
                                Type = SurveyQuestionType.MultipleChoice,
                                TemplateOptions = _mapper.Map<List<TemplateOption>>(tqm.Choices)
                            });
                        }
                        else
                        {
                            questionList.Add(new TemplateQuestion
                            {
                                Content = tqm.Name,
                                Type = SurveyQuestionType.InputField
                            });
                        }
                    });
                }
            }
            return questionList;
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
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.TemplateQuestionDuplicate,
                    ErrorMessages.TemplateQuestionDuplicate);
            }
        }

        public Task<TemplateViewModel> Get(int templateId)
        {
            var template = _templateRepository.Get(t => t.Id == templateId)
                                    .Include(t => t.TemplateQuestions)
                                 .ThenInclude(tq => tq.TemplateOptions)
                                 .AsSplitQuery()
                                 .FirstOrDefault();
            if (template == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            return Task.FromResult(ConvertToTemplateViewModel(template));
        }

        public Task<PagingViewModel<TemplateViewModelWithoutQuestions>> Search(
            TemplatePagingRequestModel templatePagingRequestModel)
        {
            var resultByCondition = _templateRepository.Get(t =>
                    (templatePagingRequestModel.Name == null || t.Name.ToLower().Contains(templatePagingRequestModel.Name.ToLower()))
                    && (templatePagingRequestModel.IsActive == null || t.IsActive == templatePagingRequestModel.IsActive));
            if (!resultByCondition.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var result = resultByCondition.OrderByDescending(t => t.CreateTime)
                                            .Skip((templatePagingRequestModel.CurrentPage - 1) * templatePagingRequestModel.PageSize)
                                            .Take(templatePagingRequestModel.PageSize);
            if (!result.Any())
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            var items = _mapper.Map<List<TemplateViewModelWithoutQuestions>>(result.ToList());
            return Task.FromResult(new PagingViewModel<TemplateViewModelWithoutQuestions>
                                            (items, resultByCondition.Count(),
                                            templatePagingRequestModel.CurrentPage,
                                            templatePagingRequestModel.PageSize));
        }
       
        public async Task Delete(int templateId)
        {
            var template = _templateRepository.Get(r => r.Id == templateId).FirstOrDefault();
            if (template == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            await _templateRepository.Remove(templateId);

            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<TemplateViewModel> Update(int templateId, TemplateRequestModel requestModel)
        {
            //check template exist
            Template template = _templateRepository.Get(t => t.Id == templateId)
                                                   .Include(t => t.TemplateQuestions)
                                                   .ThenInclude(tq => tq.TemplateOptions)
                                                   .FirstOrDefault();
            if(template == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.TemplateNotFound, 
                    ErrorMessages.TemplateNotFound);
            }
            ValidateUtils.CheckStringNotEmpty("title", requestModel.Title);
            var isExistedTemplateName = _templateRepository
                .Get(t => t.Name.ToLower() == requestModel.Title.ToLower() && t.Id != templateId)
                .Any();
            if (isExistedTemplateName)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.TemplateNameExisted,
                    ErrorMessages.TemplateNameExisted);
            }
            template.TemplateQuestions = null;
            await _unitOfWork.SaveChangeAsync();

            var questionList = GetTemplateQuestions(requestModel);
            template.Name = requestModel.Title;
            template.Description = requestModel.Description ?? "";
            template.TemplateQuestions = questionList;
            await _unitOfWork.SaveChangeAsync();
            return ConvertToTemplateViewModel(template);
        }

        public async Task<TemplateViewModelWithoutQuestions> UpdateStatus(int templateId, bool isActive)
        {
            var template = _templateRepository.Get(r => r.Id == templateId).FirstOrDefault();
            if (template == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            template.IsActive = isActive;
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<TemplateViewModelWithoutQuestions>(template);
        }
    }
}