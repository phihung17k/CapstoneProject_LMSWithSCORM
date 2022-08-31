using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class LimitConditions
    {
        public LimitConditions(XmlNode parentNode)
        {
            XmlAttributeCollection attributes = parentNode.Attributes;
            AttemptLimit = attributes["attemptLimit"] == null ? 0 : uint.Parse(attributes["attemptLimit"].Value);
            AttemptAbsoluteDurationLimit = attributes["attemptAbsoluteDurationLimit"]?.Value;
        }


        /// <summary>
        /// Type: Attribute
        /// This value indicates the maximum number of attempts for the activity
        /// </summary>
        public uint AttemptLimit { get; set; } = 0;

        /// <summary>
        /// Type: Attribute
        /// Indicate the maximum time duration that the learner is permitted to spend experiencing 
        ///     a single attempt on the activity. 
        /// The limit applies to only the time the learner is actually interacting with the activity and 
        ///     does not apply when the activity is suspended
        /// This element is used to initialize the cmi.max_time_allowed
        /// </summary>
        public string AttemptAbsoluteDurationLimit { get; set; } = "0.0";
    }
}
