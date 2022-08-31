using System.Threading.Tasks;
using LMS.Core.Models.RequestModels.RoleRequestModel;
using LMS.Core.Models.ViewModels;

namespace LMS.Infrastructure.IServices
{
    public interface IRoleService
    {
        Task<RoleViewModel> CreateRole(RoleCreateRequestModel roleRequestModel);
        Task<RoleViewModel> Get(int roleId);
        Task<PagingViewModel<RoleViewModelWithoutPermission>> Search(RolePagingRequestModel rolePagingRequestModel);
        Task<RoleViewModel> Update(int roleId, RoleUpdateRequestModel roleRequestModel);
        Task Delete(int roleId);
        Task<RoleViewModelWithoutPermission> UpdateStatus(int roleId, bool isActive);
    }
}