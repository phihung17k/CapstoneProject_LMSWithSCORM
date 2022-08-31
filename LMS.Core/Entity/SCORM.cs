using LMS.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LMS.Core.Entity
{
    [Table("scorm")]
    public class SCORM : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TitleFromManifest { get; set; }
        [Required]
        public string TitleFromUpload { get; set; }
        [Required]
        public string PathToIndex { get; set; }
        [Required]
        public string PathToFolder { get; set; }
        public string StandAloneIndexPage { get; set; }

        [Required]
        [Column("scorm_version")]
        public string SCORMVersion { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Column(TypeName = "jsonb")]
        public string ManifestItemData { get; set; }

        //public int? SubjectId { get; set; }

        //[ForeignKey(nameof(SubjectId))]
        //public Subject Subject { get; set; }

        public int? SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public Section Section { get; set; }


        [InverseProperty(nameof(TopicSCORM.SCORM))]
        public virtual ICollection<TopicSCORM> TopicSCORMs { get; set; }
    }
}
