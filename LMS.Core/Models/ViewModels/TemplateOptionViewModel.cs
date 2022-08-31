namespace LMS.Core.Models.ViewModels
{
    public class TemplateOptionViewModel
    {
        public string Content { get; set; }
    }

    public class TemplateMultipleChoiceOptionViewModel : TemplateOptionViewModel
    {
        public int Id { get; set; }
    }

    public class TemplateMatrixOptionViewModel : TemplateOptionViewModel
    {
        public int Order { get; set; }
    }
}