using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.Core.Common
{
    public abstract class AuditableEntity
    {
        [Required]
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public DateTimeOffset? DeleteTime { get; set; }
        [Required]
        public Guid CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
        public Guid? DeleteBy { get; set; }
    }
}
