using System.Linq;
using System.Threading.Tasks;
using LMS.Core.Models.ViewModels;

namespace LMS.Infrastructure.IServices
{
    public interface IPermissionService
    {
        Task<IQueryable<PermissionViewModel>> GetPermissionByCategory(string category);
    }
}