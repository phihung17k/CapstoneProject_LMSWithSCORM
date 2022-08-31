using System.Collections.Generic;

namespace LMS.Core.Models.TMSResponseModel
{
    public class DepartmentResponseModel
    {
        public List<DepartmentModel> Data { get; set; }

        public PagingTMSResponseModel Paging { get; set; }
    }
}
