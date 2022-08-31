using System;
using Newtonsoft.Json;

namespace LMS.Core.Models.ViewModels
{
    public class TopicSCORMViewModel
    {
        public int Id { get; set; }
        public int SCORMId { get; set; }
        public string TitleFromUpload { get; set; }
        public string SCORMName { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        [JsonProperty("scormCore")]
        public SCORMCoreViewModel SCORMCore { get; set; }
    }

    public class TopicSCORMWithoutCoreViewModel
    {
        public int TopicId { get; set; }
        public int SCORMId { get; set; }
        public int TopicSCORMId { get; set; }
        public string SCORMName { get; set; }
        public string PathToIndex { get; set; }
        public string PathToFolder { get; set; }
        public string StandAloneIndexPage { get; set; }
        public string SCORMVersion { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public Guid CreateBy { get; set; }
    }
}