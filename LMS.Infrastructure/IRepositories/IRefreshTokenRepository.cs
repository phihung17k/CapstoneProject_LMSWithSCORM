using LMS.Core.Entity;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IRepositories
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken entity);
        RefreshToken FindByToken(string token);
        void Update(RefreshToken entity);
        void RevokeAllTokenByUserId(string userId);
        bool HasRevokedToken(string jwtToken);
        void RevokeAllTokenByRoleId(int roleId);
    }
}
