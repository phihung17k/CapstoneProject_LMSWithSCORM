using System.Collections.Generic;

namespace LMS.Core.Models.RequestModels.TemplateRequestModel
{
    public class TemplateRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Indicate list of question
        /// </summary>
        public List<TemplateQuestionRequestModel> Elements { get; set; }
    }
}
