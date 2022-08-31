using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using System;

namespace LMS.Infrastructure.Repositories
{
    public class MailRepository : BaseRepository<Mail, Guid>, IMailRepository
    {
        public MailRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
