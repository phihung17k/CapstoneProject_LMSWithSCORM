using AutoMapper;
using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels.QuestionBankRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services
{
    public class QuestionBankService : IQuestionBankService
    {
        private readonly IQuestionBankRepository _questionBankRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuestionRepository _questionRepository;

        public QuestionBankService(IQuestionBankRepository questionBankRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IQuestionRepository questionRepository)
        {
            _questionBankRepository = questionBankRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _questionRepository = questionRepository;
        }

        public async Task<QuestionBankViewModel> CreateQuestionBank(QuestionBankCreateRequestModel requestModel)
        {
            // Validate request data
            ValidateUtils.CheckStringNotEmpty("questionBank.Content", requestModel.Content);

            //Validate question bank content exist in subject or not
            var checkContentExist = _questionBankRepository.Get(qb => qb.SubjectId == requestModel.SubjectId &&
            qb.Content.ToLower() == requestModel.Content.ToLower());
            if (checkContentExist.FirstOrDefault() != null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.QuestionBankContentExist, ErrorMessages.QuestionBankContentExist);
            }

            //add questionBank
            var questionBank = _mapper.Map<QuestionBank>(requestModel);
            try
            {
                await _questionBankRepository.AddAsync(questionBank);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (DbUpdateException)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            return _mapper.Map<QuestionBankViewModel>(questionBank);
        }

        public async Task<bool> DeleteQuestionBank(int questionBankId)
        {
            var questionBank = _questionBankRepository.Get(qb => qb.Id == questionBankId).FirstOrDefault();
            if (questionBank == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            if (questionBank.NumberOfQuestions > 0)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.QuestionBankHasQuestion, ErrorMessages.QuestionBankHasQuestion);
            }
            await _questionBankRepository.Remove(questionBankId);
            return await _unitOfWork.SaveChangeAsync();
        }

        public Task<List<QuestionBankBySubjectViewModel>> GetQuestionBankBySubject(QuestionBankRequestModel requestModel)
        {
            List<QuestionBankBySubjectViewModel> result = new();
            foreach (int subjectId in requestModel.SubjectIds)
            {
                var questionBanks = _questionBankRepository.Get(qb =>
                qb.SubjectId == subjectId);
                var questionBanksViewModel = _mapper.Map<List<QuestionBankViewModel>>(questionBanks);
                result.Add(new QuestionBankBySubjectViewModel
                {
                    SubjectId = subjectId,
                    QuestionBanks = questionBanksViewModel
                });
            }
            return Task.FromResult(result);
        }

        public Task<QuestionBankViewModel> GetQuestionBankDetail(int questionBankId)
        {
            var questionBank = _questionBankRepository.Get(qb => qb.Id == questionBankId).FirstOrDefault();
            if (questionBank == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            return Task.FromResult(_mapper.Map<QuestionBankViewModel>(questionBank));
        }

        public async Task<QuestionBankViewModel> UpdateQuestionBank(int questionBankId, QuestionBankUpdateRequestModel updateRequestModel)
        {
            // Validate request data
            ValidateUtils.CheckStringNotEmpty("questionBank.Content", updateRequestModel.Content);

            var questionBank = _questionBankRepository.Get(qb => qb.Id == questionBankId).FirstOrDefault();
            if (questionBank == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //Validate question bank content exist in subject or not
            var checkContentExist = _questionBankRepository.Get(qb =>
            qb.Id != questionBankId &&
            qb.SubjectId == questionBank.SubjectId &&
            qb.Content.ToLower() == updateRequestModel.Content.ToLower()).FirstOrDefault();
            if (checkContentExist != null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.QuestionBankContentExist, ErrorMessages.QuestionBankContentExist);
            }

            //update
            questionBank.Content = updateRequestModel.Content;
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<QuestionBankViewModel>(questionBank);
        }

        //public async Task<QuestionBankViewModel> UpdateQuestionBankStatus(int questionBankId, bool isActive)
        //{
        //    var questionBank = _questionBankRepository.Get(qb => qb.Id == questionBankId && qb.IsDeleted != true).FirstOrDefault();
        //    if (questionBank == null)
        //    {
        //        throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
        //    }

        //    //update status
        //    questionBank.IsActive = isActive;
        //    //update questions status
        //    var questions = _questionRepository.Get(q => q.QuestionBankId == questionBankId && q.IsDeleted != true).AsEnumerable();
        //    foreach (var question in questions)
        //    {
        //        question.IsActive = isActive;
        //    }
        //    await _unitOfWork.SaveChangeAsync();
        //    return _mapper.Map<QuestionBankViewModel>(questionBank);
        //}
    }
}
