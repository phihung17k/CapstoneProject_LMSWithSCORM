using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class DeliveryControls
    {
        public DeliveryControls(XmlNode parentNode)
        {
            XmlAttributeCollection attributes = parentNode?.Attributes;
            Tracked = attributes["tracked"] == null ? true : bool.Parse(attributes["tracked"]?.Value);
            CompletionSetByContent = attributes["completionSetByContent"] == null ? 
                false : bool.Parse(attributes["completionSetByContent"]?.Value);
            ObjectiveSetByContent = attributes["objectiveSetByContent"] == null ? 
                false : bool.Parse(attributes["objectiveSetByContent"]?.Value);
        }

        /// <summary>
        /// Type: Attribute
        /// This attribute indicates that the objective progress information and activity/attempt progress information 
        /// for the attempt should be recorded (true or false) and 
        /// the data will contribute to the rollup for its parent activity
        /// </summary>
        public bool Tracked { get; set; } = true;

        /// <summary>
        /// Type: Attribute
        /// This attribute indicates that the attempt completion status for the activity will be set by the SCO 
        /// (true or false)
        /// </summary>
        public bool CompletionSetByContent { get; set; } = false;

        /// <summary>
        /// Type: Attribute
        /// This attribute indicates that the objective satisfied status for the activity’s associated objective 
        /// that contributes to rollup will be set by the SCO
        /// </summary>
        public bool ObjectiveSetByContent { get; set; } = false;
    }
}
