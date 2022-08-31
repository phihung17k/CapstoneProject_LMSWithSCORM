using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace LMS.Core.Models.MailModels
{
    public class Message
    {
        public string To { get; set; }
        public IEnumerable<string> CC { get; set; }
        public string Subject { get; set; } = "";
        public string Content { get; set; } = "";
        public IFormFileCollection Attachments { get; set; }
    }
}
