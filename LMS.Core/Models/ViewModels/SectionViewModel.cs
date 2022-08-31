using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class SectionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
    }

    public class SectionWithResourcesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public ICollection<OtherLearningResourceMovingViewModel> OtherLearningResourceList { get; set; }
        public ICollection<ScormMovingViewModel> SCORMList { get; set; }
    }
}
