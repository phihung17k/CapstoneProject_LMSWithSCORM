using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Core.Models.ViewModels
{
    public class QuestionViewModel : QuestionViewModelWithoutOptions
    {
        [Display(Order = 1)]
        public List<OptionViewModel> Options { get; set; }
    }

    public class QuestionViewModelWithoutOptions
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public Guid CreateBy { get; set; }
        public string CreatedByName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class QuestionInQuizViewModel
    {
        [Display(Order = -1)]
        public int Order { get; set; }
        public int Id { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public Guid CreateBy { get; set; }
        public bool IsAvailable { get; set; }
        public List<OptionViewModel> Options { get; set; }
    }
}