using LMS.Core.Enum;
using System;

namespace LMS.Core.Models.ViewModels
{
    public class OtherLearningResourceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PathToFile { get; set; }
        //public TimeSpan? Duration { get; set; }
        public LearningResourceType Type { get; set; }
        public DateTimeOffset CreateTime { get; set; }
    }

    public class OtherLearningResourceMovingViewModel : OtherLearningResourceViewModel
    {
        public bool IsMoved { get; set; }
    }
}
