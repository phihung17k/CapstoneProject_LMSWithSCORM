using System;

namespace LMS.Core.Models.ViewModels
{
    public class SCORMViewModel
    {
        public int Id { get; set; }
        public string TitleFromUpload { get; set; }
        public string PathToIndex { get; set; }
        public string PathToFolder { get; set; }
        public string StandAloneIndexPage { get; set; }
        public DateTimeOffset CreateTime { get; set; }
    }

    public class ScormMovingViewModel : SCORMViewModel
    {
        public bool IsMoved { get; set; }
    }
}
