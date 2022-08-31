using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("user_survey")]
    public class UserSurvey
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTimeOffset SubmitTime { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [Required]
        public int SurveyId { get; set; }

        [ForeignKey(nameof(SurveyId))]
        public Survey Survey { get; set; }

        [InverseProperty(nameof(UserSurveyDetail.UserSurvey))]
        public virtual ICollection<UserSurveyDetail> UserSurveyDetailList { get; set; }
    }
}
