using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class NotificationListViewModel
    {
        public List<NotificationViewModel> Notifications { get; set; }
    }

    public class NotificationViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public bool IsRead { get; set; }
    }
}
