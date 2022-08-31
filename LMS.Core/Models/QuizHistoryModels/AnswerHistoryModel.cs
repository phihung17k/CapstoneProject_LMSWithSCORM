using LMS.Core.Enum;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace LMS.Core.Models.QuizHistoryModels
{
    public class AnswerHistoryModel
    {
        [JsonProperty("questionsPerPage")]
        public int QuestionsPerPage { get; set; }
        [JsonProperty("questions")]
        public List<QuestionHistoryModel> Questions { get; set; }
    }

    public class QuestionHistoryModel
    {
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("type")]
        public QuestionType Type { get; set; }
        [JsonProperty("correctLevel")]
        public QuestionCorrectLevel CorrectLevel { get; set; }
        [JsonProperty("earnedMark")]
        public float EarnedMark { get; set; }
        [JsonProperty("earnedScore")]
        public float EarnedScore { get; set; }      
        [JsonProperty("originalOrder")]
        public int OriginalOrder { get; set; }
        [JsonProperty("options")]
        public List<OptionHistoryModel> Options { get; set; }
    }
    public class OptionHistoryModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("isCorrect")]
        public bool IsCorrect { get; set; }
        [JsonProperty("isSelected")]
        [DefaultValue(false)]
        public bool IsSelected { get; set; }
    }
}
