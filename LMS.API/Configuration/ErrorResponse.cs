using System.Collections.Generic;
using System.Text.Json;

namespace LMS.API.Configuration
{
    public class ErrorResponse
    {
        public string Message { get; set; }

        public string ErrorCode { get; set; }

        public Dictionary<string, string> AdditionalInfo { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}