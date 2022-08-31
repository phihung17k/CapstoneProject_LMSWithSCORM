using System.Collections.Generic;
using Newtonsoft.Json;

namespace LMS.Core.Models.ViewModels
{
    public class TopicViewModelWithoutResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
    }
    public class TopicViewModelWithResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfLearningResources { get; set; }
        public int NumberOfQuizzes { get; set; }
        public int NumberOfSurveys { get; set; }
        public TopicTrackingViewModel TopicTracking { get; set; }
        public List<TopicOtherLearningResourceViewModel> TopicOtherLearningResources { get; set; }
        [JsonProperty("scorms")]
        public List<TopicSCORMViewModel> TopicSCORMs { get; set; }
        public List<SurveyInTopicViewModel> Surveys { get; set; }
        public List<QuizInTopicViewModel> Quizzes { get; set; }
    }

    public class TopicTrackingViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("isCompleted")]
        public bool IsCompleted { get; set; }
        [JsonProperty("completedLearningResourses")]
        public int CompletedLearningResourses { get; set; }
        [JsonProperty("completedQuizzes")]
        public int CompletedQuizzes { get; set; }
        [JsonProperty("completedSurveys")]
        public int CompletedSurveys { get; set; }
    }
}