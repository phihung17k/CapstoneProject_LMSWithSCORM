using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class LearningResourceViewModel
    {
        public List<OtherLearningResourceViewModel> OtherLearningResources { get; set; }
        public List<SCORMViewModel> SCORMs { get; set; }
    }
}
