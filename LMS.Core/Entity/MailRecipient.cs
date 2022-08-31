using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("mail_recipient")]
    public class MailRecipient
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public Guid MailId { get; set; }

        [ForeignKey(nameof(MailId))]
        public Mail Mail { get; set; }
    }
}
