using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using System;

namespace LMS.Infrastructure.Repositories
{
    public class MailRecipientRepository : BaseRepository<MailRecipient, Guid>, IMailRecipientRepository
    {
        public MailRecipientRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
