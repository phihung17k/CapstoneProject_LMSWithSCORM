using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("sync_log")]
    public class SyncLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TableName { get; set; }
        [Required]
        public int StatusCode { get; set; }
        [Required]
        public DateTimeOffset StartTime { get; set; }
        [Required]
        public DateTimeOffset EndTime { get; set; }
    }
}
