using LMS.Core.Enum;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class CourseMarkReportViewModel : CourseReportViewModel
    {
        public List<InstructorViewModel> Instructors { get; set; }
        public List<ManagerViewModel> Monitors { get; set; }
        public List<QuizGradingInfoViewModel> QuizGradingInfo { get; set; }
        public List<AttendeeMarkReportViewModel> AttendeesMarkResult { get; set; }
    }

    public class QuizGradingInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
    }

    public class AttendeeMarkReportViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<FinalQuizResultViewModel> QuizResult { get; set; }
        public float GPA { get; set; }
        public LearningStatus LearningStatus { get; set; }
    }

    public class FinalQuizResultViewModel
    {
        public int QuizId { get; set; }
        public float FinalScore { get; set; }
        public CompletionLevelType Status { get; set; }
    }

    public class OwnMarkReportViewModel : CourseReportViewModel
    {
        public List<InstructorViewModel> Instructors { get; set; }
        public List<ManagerViewModel> Monitors { get; set; }
        public List<TopicWithQuizResultViewModel> Topics { get; set; } = new();
        public float GPA { get; set; }
        public LearningStatus LearningStatus { get; set; }
    }

    public class TopicWithQuizResultViewModel
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public List<QuizWithResultViewModel> Quizzes{ get; set; }
    }

    public class QuizWithResultViewModel : QuizGradingInfoViewModel
    {
        public FinalQuizResultViewModel QuizResult { get; set; }
    }

    public class TopicWithQuizViewModel
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public List<QuizGradingInfoViewModel> Quizzes { get; set; }
    }
}
