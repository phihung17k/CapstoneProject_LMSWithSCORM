using LMS.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LMS.Core.Models.RequestModels.QuizRequestModel
{
    public class QuizUpdateRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1, 10)]
        public int? NumberOfAllowedAttempts { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public GradingMethodType GradingMethod { get; set; }
        [Range(0, 1)]
        public float PassedScore { get; set; }
        public bool ShuffledQuestion { get; set; }
        public bool ShuffledOption { get; set; }
        [Range(0, 5)]
        public int Credit { get; set; }
        [Range(1, 50)]
        [DefaultValue(10)]
        public int QuestionsPerPage { get; set; }
        public List<RestrictionModel> Restrictions { get; set; }
        public bool IsUpdateQuestion { get; set; } //user want to change question or not
        public List<QuestionQuizUpdateRequestModel> QuestionBanks { get; set; }
    }

    public class QuestionQuizUpdateRequestModel
    {
        public int QuestionBankId { get; set; }
        public int NumberOfQuestions { get; set; }
    }
}
