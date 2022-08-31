using LMS.Core.Enum;
using System;

namespace LMS.Core.Models.ViewModels
{
    public class ResourceInfoModel
    {
        public string FileName { get; set; }
        public string Url { get; set; }
        public FileType FileType { get; set; }
        public TimeSpan? Duration { get; set; }
        public string pathToFile { get; set; }
    }
}
