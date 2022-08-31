using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface ITMSService
    {
        Task Authenticate();
        Task VerifyAuthentication();
    }
}
