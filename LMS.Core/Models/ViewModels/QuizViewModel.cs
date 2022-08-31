using LMS.Core.Enum;
using LMS.Core.Models.RequestModels.QuizRequestModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LMS.Core.Models.ViewModels
{
    public class QuizPreviewViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? NumberOfAllowedAttempts { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfActiveQuestions { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public GradingMethodType GradingMethod { get; set; }
        public float PassedScore { get; set; }
        public bool ShuffledQuestion { get; set; }
        public bool ShuffledOption { get; set; }
        public int Credit { get; set; }
        public int TopicId { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public Guid CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
        public int QuestionsPerPage { get; set; }
        public List<RestrictionModel> Restrictions { get; set; }
        public List<QuestionQuizCreateRequestModel> QuestionBanks { get; set; }
        public List<QuestionInQuizViewModel> Questions { get; set; }
    }
    public class QuizInTopicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public Guid CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
        public FinalResultViewModel FinalResult { get; set; }
    }

    public class QuizInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? NumberOfAllowedAttempts { get; set; }
        public int NumberOfActiveQuestions { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public GradingMethodType GradingMethod { get; set; }
        public float PassedScore { get; set; }
        public int Credit { get; set; }
        public bool AttemptAvailable { get; set; }
        public bool ReviewAvailable { get; set; } = false;
        public List<RestrictionViewModelForStudent> Restrictions { get; set; }
        public UserQuizViewModel AttemptResult { get; set; }
    }

    public class RestrictionViewModelForStudent
    {
        public string TopicName { get; set; }
        public List<ResourceInTopicRestriction> Resources { get; set; }
    }

    public class RestrictionTemp
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public string ResourceName { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class ResourceInTopicRestriction
    {
        public string ResourceName { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class TopicWithRestrictionViewModel
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public List<ResourceWithRestrictionViewModel> Resources { get; set; }
    }

    public class ResourceWithRestrictionViewModel : RestrictionModel
    {
        public string TopicResourceName { get; set; }
        [DefaultValue(false)]
        public bool IsChecked { get; set; }
        public DateTimeOffset CreateTime { get; set; }
    }
}
