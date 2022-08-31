using LMS.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("survey")]
    public class Survey : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public DateTimeOffset EndDate { get; set; }
        [Required]
        public int TopicId { get; set; }

        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }

        [InverseProperty(nameof(SurveyQuestion.Survey))]
        public ICollection<SurveyQuestion> SurveyQuestions { get; set; }

        [InverseProperty(nameof(UserSurvey.Survey))]
        public ICollection<UserSurvey> UserSurveys { get; set; }
    }
}
