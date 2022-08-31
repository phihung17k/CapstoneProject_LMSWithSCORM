using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class Sequencing
    {
        public Sequencing(XmlNode parentNode)
        {
            XmlAttributeCollection attributes = parentNode?.Attributes;
            ID = attributes["ID"]?.Value;
            IDRef = attributes["IDRef"]?.Value;

            foreach (XmlNode node in parentNode.ChildNodes)
            {
                if (node.Name.Equals("imsss:controlMode"))
                {
                    ControlMode = new ControlMode(node);
                }
                else if (node.Name.Equals("imsss:sequencingRules"))
                {
                    SequencingRules = new SequencingRules(node);
                }
                else if (node.Name.Equals("imsss:limitConditions"))
                {
                    LimitConditions = new LimitConditions(node);
                }
                else if (node.Name.Equals("imsss:objectives"))
                {
                    Objectives = new Objectives(node);
                }
            }
        }


        /// <summary>
        /// Type: Attribute
        /// The attribute only occur on a <sequencing> element that is a child of a <sequencingCollection> element
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Type: Attribute
        /// A reference to a unique identifier (i.e., ID attribute of a <sequencing> element) assigned to 
        ///     a set of sequencing information
        /// Used to link to reusable sequencing information
        /// </summary>
        public string IDRef { get; set; }

        /// <summary>
        /// Type: Element
        /// Sequencing control mode information including descriptions types of 
        ///     sequencing behaviors specified for an activity
        /// </summary>
        public ControlMode ControlMode { get; set; }

        /// <summary>
        /// Type: Element
        /// Sequencing rule description
        /// Each rule describes the sequencing behavior for an activity
        /// Each activity may have an unlimited number of sequencing rules 
        ///     and within any grouping the rules are evaluated in the order in which they are listed
        /// </summary>
        public SequencingRules SequencingRules { get; set; }

        /// <summary>
        /// Type: Element
        /// ADL supports the usage of only two current limit conditions. 
        /// The limit condition deals with attempts on the activity and maximum time allowed in the attempt
        /// </summary>
        public LimitConditions LimitConditions { get; set; }

        /// <summary>
        /// Type: Element
        /// </summary>
        public string AuxiliaryResources { get; set; }

        /// <summary>
        /// Type: Element
        /// </summary>
        public string RollupRules { get; set; }

        /// <summary>
        /// Type: Element
        /// Set of objectives that are to be associated with an activity
        /// Each activity must have at least one primary objective and may have an unlimited number of objectives
        /// </summary>
        public Objectives Objectives { get; set; }

        /// <summary>
        /// Type: Element
        /// </summary>
        public string RandomizationControls { get; set; }

        /// <summary>
        /// Type: Element
        /// </summary>
        public DeliveryControls DeliveryControls { get; set; }

        /// <summary>
        /// Type: Element
        /// </summary>
        public string ConstrainedChoiceConsiderations { get; set; }

        /// <summary>
        /// Type: Element
        /// </summary>
        public string RollupConsiderations { get; set; }


    }
}
