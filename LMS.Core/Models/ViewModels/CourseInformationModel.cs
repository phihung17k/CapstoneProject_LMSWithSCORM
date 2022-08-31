using LMS.Core.Entity;
using LMS.Core.Models.TMSResponseModel;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class CourseInformationModel
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public Guid? MonitorId { get; set; }
        public List<Guid> ListOfInstructorId { get; set; } = new List<Guid>();
        public List<Guid> ListOfTraineeId { get; set; } = new List<Guid>();
        public int SubjectId { get; set; }
    }

    public class UserCourseSubjectModel
    {
        public List<UserCourse> UserCourseList { get; set; } = new List<UserCourse>();
        public List<UserSubject> UserSubjectList { get; set; } = new List<UserSubject>();
    }
}
