using Newtonsoft.Json;
using System;

namespace LMS.Core.Models.TMSResponseModel
{
    public class DepartmentModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("departmentCode")]
        public string DepartmentCode { get; set; }

        [JsonProperty("departmentName")]
        public string DepartmentName { get; set; }
    }
}
