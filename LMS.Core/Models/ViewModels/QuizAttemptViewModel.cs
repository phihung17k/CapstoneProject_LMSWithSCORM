using LMS.Core.Enum;
using LMS.Core.Models.QuizHistoryModels;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class QuizAttemptViewModel //show when user attempt quiz
    {
        public long Id { get; set; }
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset EstimatedFinishTime { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public CompletionLevelType Status { get; set; }
        public int NumberOfQuestions { get; set; }
        public int QuestionsPerPage { get; set; }
        public List<QuestionAttemptViewModel> Questions { get; set; }
    }

    public class QuestionAttemptViewModel
    {
        public int Order { get; set; }
        public int Id { get; set; }
        public string Content { get; set; }
        public QuestionType Type { get; set; }
        public int OriginalOrder { get; set; }
        public List<OptionAttemptViewModel> Options { get; set; }
    }
    public class OptionAttemptViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsSelected { get; set; }
    }
    public class QuizResultViewModel //show when user review quiz
    {
        public long Id { get; set; }
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset? FinishAt { get; set; }
        public float Mark { get; set; }
        public int NumberOfQuestions { get; set; }
        public float Score { get; set; }
        public CompletionLevelType Status { get; set; }
        public int QuestionsPerpage { get; set; }
        public List<QuestionHistoryModel> Questions { get; set; }
    }

    public class AttemptSummaryViewModel
    {
        public long Id { get; set; }
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset? FinishAt { get; set; }
        public float Mark { get; set; }
        public int NumberOfQuestions { get; set; }
        public float Score { get; set; }
        public CompletionLevelType Status { get; set; }
    }

    public class SubmitQuizViewModel
    {
        public QuizResultViewModel QuizResult { get; set; }
        public TopicTrackingViewModel TopicTracking { get; set; }
    }
}
