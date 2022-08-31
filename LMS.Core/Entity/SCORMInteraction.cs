using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("scorm_interaction")]
    public class SCORMInteraction
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public int N { get; set; }
        [Required]
        public string NId { get; set; }
        public string Type { get; set; }
        public string Timestamp { get; set; }
        public string Weighting { get; set; }
        public string LearnerResponse { get; set; }
        public string Result { get; set; }
        public string Latency { get; set; }
        public string Description { get; set; }
        [Required]
        [Column("scorm_core_id")]
        public int SCORMCoreId { get; set; }
        [ForeignKey(nameof(SCORMCoreId))]
        public SCORMCore SCORMCore { get; set; }
        [InverseProperty(nameof(SCORMInteractionCorrectResponse.SCORMInteraction))]
        public virtual ICollection<SCORMInteractionCorrectResponse> CorrectResonses { get; set; }
        [InverseProperty(nameof(SCORMInteractionObjective.SCORMInteraction))]
        public virtual ICollection<SCORMInteractionObjective> Objectives { get; set; }
    }
}
