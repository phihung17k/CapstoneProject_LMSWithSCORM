using System;
using System.Threading.Tasks;
using AutoMapper;
using LMS.Core.Enum;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.ViewModels;

namespace LMS.Infrastructure.Services
{
    public class QuestionTypeService : IQuestionTypeService
    {
        private IMapper _mapper;

        public QuestionTypeService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public Task<QuestionTypeViewModel> GetAllQuestionTypes()
        {
            return Task.FromResult(_mapper.Map<QuestionTypeViewModel>(Enum.GetValues(typeof(QuestionType))));
        }
    }
}