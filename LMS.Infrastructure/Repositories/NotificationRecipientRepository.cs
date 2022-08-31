using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using System;

namespace LMS.Infrastructure.Repositories
{
    public class NotificationRecipientRepository : BaseRepository<NotificationRecipient, Guid>,
        INotificationRecipientRepository
    {
        public NotificationRecipientRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
