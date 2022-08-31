using LMS.Core.Enum;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class QuizReportViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionInQuizReportViewModel> Questions { get; set; } //report table header
        public List<QuizAttemptReportViewModel> QuizAttempts { get; set; } //report table data
    }

    public class QuestionInQuizReportViewModel
    {
        public int Order { get; set; }
        public int Id { get; set; }
        public string Content { get; set; }
    }

    public class QuizAttemptReportViewModel
    {
        public int QuizAttemptId { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset? FinishAt { get; set; }
        public float Mark { get; set; }
        public int NumberOfQuestions { get; set; }
        public float Score { get; set; }
        public CompletionLevelType Status { get; set; }
        public List<QuestionAnswerReportViewModel> QuestionAnswer { get; set; }
    }

    public class QuestionAnswerReportViewModel
    {
        public int Order { get; set; }
        public int Id { get; set; }
        public QuestionCorrectLevel CorrectLevel { get; set; }
        public float EarnedGrade { get; set; }
        public float EarnedScore { get; set; }
    }

    public class StudentQuizResultViewModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public QuizResultViewModel QuizAttemptDetail { get; set; }
    }
}
