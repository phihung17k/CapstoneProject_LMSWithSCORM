using LMS.Core.Enum;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class CoursePagingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string ParentName { get; set; }

        public CourseType Type { get; set; }

        public int NumberOfTrainee { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public bool IsActive { get; set; }
        public string ActionType { get; set; }
        public int PassScore { get; set; }
        public CourseTrackingViewModel CourseTracking { get; set; }
    }
    public class CourseDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string ParentName { get; set; }

        public CourseType Type { get; set; }

        public int NumberOfTrainee { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public bool IsActive { get; set; }

        public int SubjectId { get; set; }

        public string Description { get; set; }
        public string ActionType { get; set; }
        public int PassScore { get; set; }
        public CourseTrackingViewModel CourseTracking { get; set; }
        public bool HasCreditQuiz { get; set; } = true;

        private List<TopicViewModelWithResource> topics;
        private List<InstructorViewModel> instructors;
        private List<ManagerViewModel> monitors;

        public List<TopicViewModelWithResource> Topics 
        {
            get
            {
                return topics;
            }
            set
            {
                topics = value ?? new List<TopicViewModelWithResource>();
            }
        }

        public List<InstructorViewModel> Instructors
        {
            get
            {
                return instructors;
            }
            set
            {
                instructors = value ?? new List<InstructorViewModel>();
            }
        }

        public List<ManagerViewModel> Monitors
        {
            get
            {
                return monitors;
            }
            set
            {
                monitors = value ?? new List<ManagerViewModel>();
            }
        }
    }

    public class CourseTrackingViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset? FinishTime { get; set; }
        public float FinalScore { get; set; }
        public LearningStatus LearningStatus { get; set; }
    }

    public class CourseReportViewModel
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public CourseType Type { get; set; }
        public int SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string ParentCode { get; set; }
        public string ParentName { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int PassScore { get; set; }
    }

    public class CourseGradeReportViewModel : CourseReportViewModel
    {
        public DateTimeOffset? FinishTime { get; set; }
        public float GPA { get; set; }
        public LearningStatus LearningStatus { get; set; }
    }
}
