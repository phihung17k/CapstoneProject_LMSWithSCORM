using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class TemplateViewModel : TemplateViewModelWithoutQuestions
    {
        public List<TemplateQuestionViewModel> Elements { get; set; } = new List<TemplateQuestionViewModel>();
    }

    public class TemplateViewModelWithoutQuestions
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public Guid CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
    }
}