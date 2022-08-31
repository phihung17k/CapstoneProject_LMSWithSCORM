using System.Threading.Tasks;
using LMS.Core.Models.ViewModels;

namespace LMS.Infrastructure.IServices
{
    public interface IQuestionTypeService
    {
        Task<QuestionTypeViewModel> GetAllQuestionTypes();
    }
}