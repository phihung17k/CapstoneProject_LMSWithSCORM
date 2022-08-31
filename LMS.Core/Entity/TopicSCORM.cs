using LMS.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("topic_scorm")]
    public class TopicSCORM : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TopicId { get; set; }

        [Required]
        [Column("scorm_id")]
        public int SCORMId { get; set; }
        [Required]
        public string SCORMName { get; set; }

        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }

        [ForeignKey(nameof(SCORMId))]
        public SCORM SCORM { get; set; }
        [InverseProperty(nameof(SCORMCore.TopicSCORM))]
        public virtual ICollection<SCORMCore> SCORMCores { get; set; }
    }
}
