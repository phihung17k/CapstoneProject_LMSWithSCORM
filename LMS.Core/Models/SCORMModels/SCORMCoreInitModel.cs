using Newtonsoft.Json;
using System.Collections.Generic;

namespace LMS.Core.Models.SCORMModels
{
    public class SCORMCoreInitModel
    {
        [JsonProperty("objectives")]
        public ObjectivesInit Objectives { get; set; }

        [JsonProperty("dataFromLMS")]
        public string DataFromLMS { get; set; }

        [JsonProperty("timeLimitAction")]
        public string TimeLimitAction { get; set; }

        [JsonProperty("completionThreshold")]
        public string CompletionThreshold { get; set; }

        [JsonProperty("attemptAbsoluteDurationLimit")]
        public string AttemptAbsoluteDurationLimit { get; set; }

        //SCORM 1.2
        [JsonProperty("masteryScore")]
        public string MasteryScore12 { get; set; }

    }

    public class ObjectivesInit
    {
        [JsonProperty("primaryObjective")]
        public PrimaryObjectiveInit PrimaryObjective { get; set; }
        [JsonProperty("objectiveList")]
        public List<string> ObjectiveList { get; set; }
    }

    public class PrimaryObjectiveInit
    {
        [JsonProperty("objectiveID")]
        public string ObjectiveID { get; set; }
        [JsonProperty("minNormalizedMeasure")]
        public string MinNormalizedMeasure { get; set; }
    }

    //public class ObjectiveInit
    //{
    //    [JsonProperty("objectiveID")]
    //    public string ObjectiveID { get; set; }
    //}
}
