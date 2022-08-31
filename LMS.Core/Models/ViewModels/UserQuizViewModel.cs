using LMS.Core.Enum;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class FinalResultViewModel //view when get course detail
    {
        public long Id { get; set; }
        public float FinalScore { get; set; }
        public CompletionLevelType Status { get; set; }
    }
    public class UserQuizViewModel //view when view attempts summary
    {
        public long Id { get; set; }
        public float FinalScore { get; set; }
        public CompletionLevelType Status { get; set; }

        public List<AttemptSummaryViewModel> AttemptsSummary { get; set; }
    }
}
