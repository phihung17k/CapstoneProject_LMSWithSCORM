using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("notification_recipient")]
    public class NotificationRecipient
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [Required]
        public Guid NotificationId { get; set; }

        [ForeignKey(nameof(NotificationId))]
        public Notification Notification { get; set; }
        [Required]
        public bool IsRead { get; set; }
    }
}
