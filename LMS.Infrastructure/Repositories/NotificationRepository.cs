using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using System;

namespace LMS.Infrastructure.Repositories
{
    public class NotificationRepository : BaseRepository<Notification, Guid>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
