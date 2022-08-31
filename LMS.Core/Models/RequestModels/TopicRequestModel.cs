namespace LMS.Core.Models.RequestModels
{
    public class TopicCreateRequestModel
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
    }
    public class TopicUpdateRequestModel
    {
        public string Name { get; set; }
    }
}
