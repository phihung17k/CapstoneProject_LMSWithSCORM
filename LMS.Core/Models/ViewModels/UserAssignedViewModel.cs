using System;

namespace LMS.Core.Models.ViewModels
{
    public class UserAssignedViewModel
    {
        public Guid Id { get; set; }
        public string Eid { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class InstructorViewModel : UserAssignedViewModel
    {
        
    }
    public class ManagerViewModel : UserAssignedViewModel
    {

    }
    public class AttendeeViewModel : UserAssignedViewModel
    {

    }
    public class AttendeeSummaryViewModel : UserAssignedViewModel
    {
        public CourseTrackingViewModel CourseTracking { get; set; }
    }

}
