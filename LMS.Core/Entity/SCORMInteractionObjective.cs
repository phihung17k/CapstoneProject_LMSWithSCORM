using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("scorm_interaction_objective")]
    public class SCORMInteractionObjective
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int N { get; set; }
        [Required]
        public string NId { get; set; }
        [Required]
        public long InteractionId { get; set; }
        [ForeignKey(nameof(InteractionId))]
        public SCORMInteraction SCORMInteraction { get; set; }
    }
}
