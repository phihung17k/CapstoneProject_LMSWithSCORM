using LMS.Core.Models.MailModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface IMailService
    {
        Task SendEmailAsync(Message message);
        string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel);
        Task CreateMail(List<Guid> userIds, string title);
    }
}
