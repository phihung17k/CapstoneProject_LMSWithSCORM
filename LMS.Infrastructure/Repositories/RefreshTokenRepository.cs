using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken, Guid>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public override async Task AddAsync(RefreshToken entity)
        {
            await base.AddAsync(entity);
            await applicationDbContext.SaveChangesAsync();
        }

        public RefreshToken FindByToken(string token)
        {
            RefreshToken result = null;
            try
            {
                IQueryable<RefreshToken> query = from refreshToken in applicationDbContext.RefreshTokens
                                                 where refreshToken.Content == token
                                                 select new RefreshToken
                                                 {
                                                     Id = refreshToken.Id,
                                                     Content = refreshToken.Content,
                                                     AccessToken = refreshToken.AccessToken,
                                                     User = refreshToken.User,
                                                     CreateTime = refreshToken.CreateTime,
                                                     ExpiresTime = refreshToken.ExpiresTime,
                                                     RevokedTime = refreshToken.RevokedTime
                                                 };
                if (query.Count() > 0)
                {
                    result = query.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            return result;
        }

        public void RevokeAllTokenByUserId(string userId)
        {
            try
            {
                IQueryable<RefreshToken> query = from refreshToken in applicationDbContext.RefreshTokens
                                                 where refreshToken.User.Id.ToString() == userId
                                                 && refreshToken.RevokedTime == null
                                                 orderby refreshToken.ExpiresTime
                                                 select refreshToken;
                if (query.Any())
                {
                    foreach (var token in query)
                    {
                        token.RevokedTime = DateTimeOffset.Now;
                        applicationDbContext.RefreshTokens.Update(token);
                    }
                    applicationDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public override void Update(RefreshToken entity)
        {
            try
            {
                base.Update(entity);
                applicationDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool HasRevokedToken(string jwtToken)
        {
            try
            {
                IQueryable<RefreshToken> query = from refreshToken in applicationDbContext.RefreshTokens
                                                 where refreshToken.AccessToken == jwtToken
                                                 select refreshToken;
                if (query.Any())
                {
                    RefreshToken refreshToken = query.FirstOrDefault();
                    if (refreshToken.RevokedTime != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public void RevokeAllTokenByRoleId(int roleId)
        {
            try
            {
                IQueryable<RefreshToken> query = from role in applicationDbContext.Roles
                                                 join roleUser in applicationDbContext.RoleUsers on role.Id equals roleUser.RoleId
                                                 join user in applicationDbContext.Users on roleUser.UserId equals user.Id
                                                 join refreshToken in applicationDbContext.RefreshTokens on user.Id equals refreshToken.UserId
                                                 where role.Id == roleId && refreshToken.RevokedTime == null
                                                 select refreshToken;
                if (query.Any())
                {
                    foreach (var refreshToken in query)
                    {
                        refreshToken.RevokedTime = DateTimeOffset.Now;
                        applicationDbContext.RefreshTokens.Update(refreshToken);
                    }
                    applicationDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
