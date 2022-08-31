using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("scorm_objective")]
    public class SCORMObjective
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int N { get; set; }
        [Required]
        public string Nid { get; set; }
        public string ScoreScaled { get; set; }
        public string ScoreRaw { get; set; }
        public string ScoreMin { get; set; }
        public string ScoreMax { get; set; }
        public string SuccessStatus { get; set; }
        public string CompletionStatus { get; set; }
        public string ProgressMeasure { get; set; }
        public string Description { get; set; }
        [Column("scorm_core_id")]
        public int SCORMCoreId { get; set; }
        [ForeignKey(nameof(SCORMCoreId))]
        public SCORMCore SCORMCore { get; set; }

        //SCORM 1.2
        public string Status12 { get; set; }
    }
}
