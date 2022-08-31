using LMS.Core.Entity;
using LMS.Core.Models.MailModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using MailKit.Net.Smtp;
using MimeKit;
using RazorEngineCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly EmailConfig _emailConfig;
        private readonly IMailRepository _mailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailRecipientRepository _mailRecipientRepository;

        public MailService(EmailConfig emailConfig, IMailRepository mailRepository, IUnitOfWork unitOfWork, 
            IMailRecipientRepository mailRecipientRepository)
        {
            _emailConfig = emailConfig;
            _mailRepository = mailRepository;
            _unitOfWork = unitOfWork;
            _mailRecipientRepository = mailRecipientRepository;
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);
            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.Add(new MailboxAddress(message.To));
            IEnumerable<MailboxAddress> CC = message.CC?.Select(c => new MailboxAddress(c));
            if (CC != null)
            {
                emailMessage.Cc.AddRange(CC);
            }
            emailMessage.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder { HtmlBody = message.Content };
            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        public string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel)
        {
            string mailTemplate = LoadTemplate(emailTemplate);
            IRazorEngine razorEngine = new RazorEngine();
            IRazorEngineCompiledTemplate modifiedMailTemplate = razorEngine.Compile(mailTemplate);
            return modifiedMailTemplate.Run(emailTemplateModel);
        }
        public static string LoadTemplate(string emailTemplate)
        {
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            string templateDir = Path.Combine(solutiondir, "LMS.Core\\Common\\Templates");
            if (!isDevelopment)
            {
                solutiondir = Directory.GetCurrentDirectory();
                templateDir = Path.Combine(solutiondir, "Templates");
            }
            string templatePath = Path.Combine(templateDir, $"{emailTemplate}.cshtml");
            using FileStream fileStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using StreamReader streamReader = new StreamReader(fileStream, Encoding.Default);
            string mailTemplate = streamReader.ReadToEnd();
            streamReader.Close();
            return mailTemplate;
        }

        public async Task CreateMail(List<Guid> userIds, string title)
        {
            var mail = new Mail
            {
                Id = Guid.NewGuid(),
                Title = title
            };
            await _mailRepository.AddAsync(mail);
            await _unitOfWork.SaveChangeAsync();
            foreach (var userId in userIds)
            {
                var mailRecipient = new MailRecipient
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    MailId = mail.Id
                };
                await _mailRecipientRepository.AddAsync(mailRecipient);
            }
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
