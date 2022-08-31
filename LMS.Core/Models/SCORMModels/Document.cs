using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class Document
    {
        public Manifest Manifest { get; set; }
        public string IndexPage { get; set; }
        public string StandAloneIndexPage { get; set; }
        public string Title { get; set; }
        public string Version { get; set; } //SCORM version
        public string CompletionThreshold { get; set; }
        public string DataFromLMS { get; set; }
        public string AttemptAbsoluteDurationLimit { get; set; } = "0.0";
        public Objectives Objectives { get; set; }
        public string TimeLimitAction { get; set; }
        public string MinNormalizedMeasure { get; set; }

        public Document(string path)
        {
            XmlDocument document = new();
            document.Load(path);
            Manifest = new Manifest(document);

            Version = Manifest.GetSCORMVersion();

            Title = Manifest.Title;

            IndexPage = Manifest.HrefOfDefaultResource;

            StandAloneIndexPage = Manifest.StandAloneIndexPage;

            Item item = Manifest.DefaultOrganization.Item;

            CompletionThreshold = item?.CompletionThreshold;

            DataFromLMS = item?.DataFromLMS;

            Objectives = item?.Sequencing?.Objectives;

            if (Version.Contains("1.2"))
            {
                AttemptAbsoluteDurationLimit = item.MaxTimeAllowed;
                MinNormalizedMeasure = item.MasteryScore;
            }
            else
            {
                AttemptAbsoluteDurationLimit = item?.Sequencing?.LimitConditions?.AttemptAbsoluteDurationLimit;
                MinNormalizedMeasure = Objectives?.PrimaryObjective?.MinNormalizedMeasure;
            }

            TimeLimitAction = item.TimeLimitAction;
        }
    }
}
