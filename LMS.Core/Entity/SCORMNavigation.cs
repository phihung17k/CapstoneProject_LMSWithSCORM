using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("scorm_navigation")]
    public class SCORMNavigation
    {
        [Key]
        [Column("scorm_core_id")]
        public int SCORMCoreId { get; set; }
        public string Request { get; set; }
        public string ValidContinue { get; set; }
        public string ValidPrevious { get; set; }
        public SCORMCore SCORMCore { get; set; }
    }
}
