using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Core.Models.RequestModels.QuestionBankRequestModel
{
    public class QuestionBankRequestModel
    {
        [Required]
        public List<int> SubjectIds { get; set; }
    }
}
