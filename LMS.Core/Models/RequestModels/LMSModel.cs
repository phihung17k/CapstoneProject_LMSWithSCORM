using LMS.Core.Entity;
using LMS.Core.Models.ViewModels;
using System.Text.Json.Serialization;

namespace LMS.Core.Models.RequestModels
{
    public class LMSModel
    {

        public int SCORMCoreId { get; set; }
        //identifier of resource
        public string ScoIdentifier { get; set; }
        public string DataItem { get; set; }
        public string DataValue { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorString { get; set; }

        //addtional error
        public string ErrorDiagnostic { get; set; }

        public string ReturnValue { get; set; }

        [JsonIgnore]
        public bool IsUpdateCompletionStatus { get; set; } = false;

        //complete_status = completed in SCORM 2004 or core.lesson_status = passed in SCORM 1.2
        // so that, isStopTracking = true and LMS don't need to send "UpdateScormCoreStatus" method
        [JsonIgnore]
        public bool isStopTracking { get; set; } = false;

        [JsonIgnore]
        public SCORMCore scormCore { get; set; }

        [JsonIgnore]
        public bool IsSCORMVersion12 { get; set; } = false;
        [JsonIgnore]
        public TopicTrackingViewModel TopicTracking { get; set; }
    }
}
