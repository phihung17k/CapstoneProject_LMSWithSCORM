using System;

namespace LMS.Core.Models.ViewModels
{
    public class SCORMViewContentModel
    {
        public string Url { get; set; }
        public SCORMCoreViewModel SCORMCore { get; set; }
    }
    public class SCORMCoreViewModel
    {
        public int Id { get; set; }
        public Guid LearnerId { get; set; }
        public string LearnerName { get; set; }
        public string Mode { get; set; }
        public string Credit { get; set; }
        public string CompletionStatus { get; set; }
        public string Entry { get; set; }
        public string ProgressMeasure { get; set; }
        public string TotalTime { get; set; }
        public string SuccessStatus { get; set; }
        public int TopicSCORMId { get; set; }
        public string LessonStatus12 { get; set; }
    }
}
