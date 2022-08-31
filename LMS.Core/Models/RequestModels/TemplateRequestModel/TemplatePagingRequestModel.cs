using LMS.Core.Models.Common.RequestModels;

namespace LMS.Core.Models.RequestModels.TemplateRequestModel
{
    public class TemplatePagingRequestModel : PagingRequestModel
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}