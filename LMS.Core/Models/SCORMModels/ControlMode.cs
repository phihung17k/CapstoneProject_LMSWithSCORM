using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class ControlMode
    {
        public ControlMode(XmlNode parentNode)
        {
            XmlAttributeCollection attributes = parentNode.Attributes;
            Choice = attributes["choice"] == null ? true : bool.Parse(attributes["choice"].Value);

            ChoiceExit = attributes["choiceExit"] == null ? true : bool.Parse(attributes["choiceExit"].Value);

            Flow = attributes["flow"] == null ? false : bool.Parse(attributes["flow"].Value);

            ForwardOnly = attributes["forwardOnly"] == null ? false : bool.Parse(attributes["forwardOnly"].Value);

            UseCurrentAttemptObjectiveInfo = attributes["useCurrentAttemptObjectiveInfo"] == null
                                            ? true : bool.Parse(attributes["useCurrentAttemptObjectiveInfo"].Value);

            UseCurrentAttemptProgressInfo = attributes["useCurrentAttemptProgressInfo"] == null
                                            ? true : bool.Parse(attributes["useCurrentAttemptProgressInfo"].Value);
        }


        /// <summary>
        /// Type: Attribute
        /// Indicates that a choice sequencing request is permitted (not permitted if value = false) 
        ///     to target the children of the activity
        /// </summary>
        public bool Choice { get; set; } = true;

        /// <summary>
        /// Type: Attribute
        /// Indicates that an active child of this activity is permitted to terminate 
        ///     (or not permitted if value = false) if a choice sequencing request is processed
        /// </summary>
        public bool ChoiceExit { get; set; } = true;

        /// <summary>
        /// Type: Attribute
        /// Indicates the flow sequencing requests is permitted (or not permitted if value = false) 
        ///     to the children of this activity
        /// </summary>
        public bool Flow { get; set; } = false;

        /// <summary>
        /// Type: Attribute
        /// Indicates that backward targets (in terms of activity tree traversal) are not permitted 
        ///     (or are permitted if value = false) for the children of this activity
        /// </summary>
        public bool ForwardOnly { get; set; } = false;

        /// <summary>
        /// Type: Attribute
        /// Indicates that the OBJECTIVE progress information for the children of the activity will only be used 
        ///     (or not used if value = false) in rule evaluations and rollup if that information was recorded 
        ///     during the current attempt on the activity
        /// </summary>
        public bool UseCurrentAttemptObjectiveInfo { get; set; } = true;

        /// <summary>
        /// Type: Attribute
        /// Indicates that the ATTEMPT progress information for the children of the activity will only be used 
        ///     (or not used if value = false) in rule evaluations and rollup if that information was recorded 
        ///     during the current attempt on the activity 
        /// </summary>
        public bool UseCurrentAttemptProgressInfo { get; set; } = true;
    }
}
