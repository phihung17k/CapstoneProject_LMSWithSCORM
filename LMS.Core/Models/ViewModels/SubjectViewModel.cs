using LMS.Core.Enum;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class SubjectViewModel : SubjectViewModelWithoutSection
    {
        public List<SectionWithResourcesViewModel> Sections { get; set; }
    }

    public class SubjectWithSectionsViewModel 
    {
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public List<SectionWithResourcesViewModel> Sections { get; set; }
    }

    public class SubjectViewModelWithoutSection
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PassScore { get; set; }
        public CourseType Type { get; set; }
        public bool IsActive { get; set; }
        public List<InstructorViewModel> Instructors { get; set; }
    }
}
