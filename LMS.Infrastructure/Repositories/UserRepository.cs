using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<User> FindAsync(Guid id)
        {
            try
            {
                return await base.FindAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public override async Task AddAsync(User entity)
        {
            await base.AddAsync(entity);
            await applicationDbContext.SaveChangesAsync();
        }

        public List<Permission> GetPermissionsById(Guid id)
        {
            try
            {
                IQueryable<Permission> query = from roleUser in applicationDbContext.RoleUsers
                                               join role in applicationDbContext.Roles on roleUser.Role.Id equals role.Id
                                               join permissionRole in applicationDbContext.PermissionRoles on role.Id equals permissionRole.RoleId
                                               join permission in applicationDbContext.Permissions on permissionRole.PermissionId equals permission.Id
                                               where roleUser.User.Id == id
                                               select permission;
                if (query.Count() > 0)
                {
                    query = query.Distinct();
                }
                return query.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task SynchronizeBulk(List<User> listOfUser)
        {
            await applicationDbContext.Users.BulkInsertAsync(listOfUser, options =>
            {
                options.InsertIfNotExists = true;
                options.ColumnPrimaryKeyExpression = u => u.Id;
                options.AutoMapOutputDirection = false;
            });
            await applicationDbContext.Users.BulkUpdateAsync(listOfUser, options =>
            {
                options.IgnoreOnUpdateExpression = u => new
                {
                    u.DateOfJoin,
                    u.Avatar,
                    u.IsActiveInLMS
                };
            });
        }

        public async Task AssignRoleForInsertedUser(DateTimeOffset startTime, DateTimeOffset endTime, Guid creatorId, List<int> roleIds)
        {
            IQueryable<User> query = applicationDbContext.Users.Where(
                u => u.DateOfJoin > startTime && u.DateOfJoin < endTime);

            if (query.Any())
            {
                List<RoleUser> roleUsers = new();
                var newAccounts = query.ToList();
                newAccounts.ForEach(u =>
                {
                    roleUsers.Add(new RoleUser
                    {
                        RoleId = roleIds[0], // assign Authenticated User for all account
                        UserId = u.Id,
                        CreateTime = DateTimeOffset.Now,
                        CreateBy = creatorId
                    });
                    roleUsers.Add(new RoleUser
                    {
                        RoleId = roleIds[2], // assign student for all users
                        UserId = u.Id,
                        CreateTime = DateTimeOffset.Now,
                        CreateBy = creatorId
                    });
                }
                );

                if (newAccounts.Any(na => na.Id == creatorId))
                {
                    roleUsers.Add(
                        new RoleUser
                        {
                            RoleId = roleIds[1], // assign Admin for creator
                            UserId = creatorId,
                            CreateTime = DateTimeOffset.Now,
                            CreateBy = creatorId
                        });
                }
                await applicationDbContext.RoleUsers.BulkInsertAsync(roleUsers);
            }
        }
    }
}
