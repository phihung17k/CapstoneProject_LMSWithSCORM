using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("refresh_token")]
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public DateTimeOffset CreateTime { get; set; }
        [Required]
        public DateTimeOffset ExpiresTime { get; set; }
        public DateTimeOffset? RevokedTime { get; set; }
    }
}
