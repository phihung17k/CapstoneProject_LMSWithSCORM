using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using System;

namespace LMS.Infrastructure.IRepositories
{
    public interface IMailRepository : IBaseRepository<Mail, Guid>
    {
    }
}
