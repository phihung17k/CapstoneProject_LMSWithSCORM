using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("scorm_comment_from_learner")]
    public class SCORMCommentFromLearner
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int N { get; set; }
        public string Comment { get; set; }
        public string Location { get; set; }
        public string Timestamp { get; set; }

        [Required]
        [Column("scorm_core_id")]
        public int SCORMCoreId { get; set; }

        [ForeignKey(nameof(SCORMCoreId))]
        public SCORMCore SCORMCore { get; set; }
    }
}
