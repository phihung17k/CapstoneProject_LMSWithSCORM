using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("notification")]
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Message { get; set; }
        //link to page (if any) or link api for FE call
        public string Url { get; set; }
        [Required]
        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

        [InverseProperty(nameof(NotificationRecipient.Notification))]
        public virtual ICollection<NotificationRecipient> NotificationRecipientList { get; set; }
    }
}
