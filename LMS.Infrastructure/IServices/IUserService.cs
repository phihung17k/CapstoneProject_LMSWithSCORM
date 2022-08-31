using LMS.Core.Models.RequestModels.UserRequestModel;
using LMS.Core.Models.RequestModels.UserRoleRequestModel;
using LMS.Core.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface IUserService
    {
        Task<UserViewModel> GetDetailUser(Guid userId);
        Task<List<RoleViewModelWithoutPermission>> AssignRoleToUser(string userId, UserRoleRequestModel userRoleRequestModel);
        Task<UserRoleViewModel> GetDetailUserRole(string userId);
        Task<PagingViewModel<UserRoleViewModel>> GetAllUser(UserPagingRequestModel requestModel);
        Task<string> UpdateAvatar(Guid userId, IFormFile image);
        Task SyncUser();
        List<QuestionCreatorViewModel> GetQuestionCreators(int questionBankId);
        Task<UserViewModel> UpdateStatus(Guid id, bool isActive);
    }
}
