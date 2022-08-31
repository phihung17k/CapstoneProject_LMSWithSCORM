using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class Objective
    {
        public Objective(XmlNode parentNode)
        {
            XmlAttributeCollection attributes = parentNode.Attributes;
            SatisfiedByMeasure = attributes["satisfiedByMeasure"] == null
                                ? false : bool.Parse(attributes["satisfiedByMeasure"].Value);
            ObjectiveID = attributes["objectiveID"]?.Value;

            foreach (XmlNode node in parentNode.ChildNodes)
            {
                if (node.Name.Equals("imsss:minNormalizedMeasure"))
                {
                    MinNormalizedMeasure = float.Parse(node.InnerText);
                }
                else if (node.Name.Equals("imsss:mapInfo"))
                {
                    MapInfo = new MapInfo(node);
                }
            }
        }
        /// <summary>
        /// Type: Attribute
        /// Indicates that the <minNormalizedMeasure> shall be used (if value is set to true) 
        /// in place of any other method to determine if the objective associated with the activity is satisfied
        /// </summary>
        public bool SatisfiedByMeasure { get; set; } = false;

        /// <summary>
        /// Type: Attribute
        /// Identifier of the objective associated with the activity
        /// For a given set of objectives defined for an activity 
        ///     (one <primaryObjective> and multiple <objective> elements in a given <objectives> element), 
        ///     all defined objectiveID attributes shall be unique. 
        /// LMSs shall use the value held by the objectiveID attribute to initialize the cmi.objectives.n.id 
        /// </summary>
        public string ObjectiveID { get; set; }

        /// <summary>
        /// Type: Element
        /// Identifies minimum satisfaction measure for the objective
        /// Range value: [-1; 1]
        /// If this element is used to defined a minimum satisfaction measure for the <primaryObjective> element, 
        ///     then the LMS shall use this value to initialize the cmi.scaled_passing_score 
        /// </summary>
        public float MinNormalizedMeasure { get; set; }

        /// <summary>
        /// Type: Element
        /// The container for the objective map description
        /// This defines the mapping of an activity’s local objective information to and 
        ///     from a shared global objective. 
        /// Each activity may have an unlimited number of objective maps
        /// </summary>
        public MapInfo MapInfo { get; set; }
    }
}
