using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IService
{
    public interface IAuthService
    {
        Task<LoginResponseModel> Authenticate(LoginRequestModel requestModel);
        Task<LoginResponseModel> RefreshToken(TokenRequestModel requestModel);
        Task Logout(string userId);
    }
}
