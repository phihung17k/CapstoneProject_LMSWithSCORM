using LMS.Core.Common;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class QuestionBankViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int NumberOfQuestions { get; set; }
        public int SubjectId { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset? UpdateTime { get; set; }
        public Guid CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
    }

    public class QuestionBankBySubjectViewModel
    {
        public int SubjectId { get; set; }
        public List<QuestionBankViewModel> QuestionBanks { get; set; }
    }
}