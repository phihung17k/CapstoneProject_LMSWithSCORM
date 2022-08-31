using System;
using LMS.Core.Models.Common.RequestModels;

namespace LMS.Core.Models.RequestModels.QuestionRequestModel
{
    public class QuestionPagingRequestModel : PagingRequestModel
    {
#nullable enable
        public string? Content { get; set; }
        public int QuestionBankId { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}