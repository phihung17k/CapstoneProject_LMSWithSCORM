using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("scorm_learner_preference")]
    public class SCORMLearnerPreference
    {
        [Key]
        [Column("scorm_core_id")]
        public int SCORMCoreId { get; set; }
        public string AudioLevel { get; set; }
        public string Language { get; set; }
        public string DeliverySpeed { get; set; }
        public string AudioCaptioning { get; set; }
        public SCORMCore SCORMCore { get; set; }
    }
}
