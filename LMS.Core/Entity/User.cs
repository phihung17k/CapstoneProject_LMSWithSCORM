using LMS.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("user")]
    public class User : AuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Eid { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public bool IsMale { get; set; }

        //active in db of the lms system, not tms system
        [Required]
        public bool IsActiveInLMS { get; set; } = true;
        //active in tms system
        [Required]
        public bool IsActive { get; set; } = true;
        //delete in tms system
        [Required]
        public bool IsDeleted { get; set; }

        public string Avatar { get; set; }

        public DateTimeOffset? DateOfJoin { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }

        [InverseProperty(nameof(RoleUser.User))]
        public virtual ICollection<RoleUser> Roles { get; set; }

        [InverseProperty(nameof(UserSubject.User))]
        public virtual ICollection<UserSubject> Subjects { get; set; }
        [InverseProperty(nameof(UserCourse.User))]
        public virtual ICollection<UserCourse> Courses { get; set; }
        [InverseProperty(nameof(UserQuiz.User))]
        public virtual ICollection<UserQuiz> Quizzes { get; set; }
    }
}
