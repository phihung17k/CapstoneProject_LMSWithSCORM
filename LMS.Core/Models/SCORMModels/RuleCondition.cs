using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class RuleCondition
    {
        public RuleCondition(XmlNode parentNode)
        {
            XmlAttributeCollection attributes = parentNode.Attributes;
            ReferencedObjective = attributes["referencedObjective"]?.Value;
            MeasureThreshold = attributes["measureThreshold"] == null
                                ? 0 : float.Parse(attributes["measureThreshold"].Value);
            Operator = attributes["operator"]?.Value ?? "noOp";
            Condition = attributes["condition"]?.Value ?? "always";
        }


        /// <summary>
        /// Type: Attribute
        /// Represents the identifier of an objective associated with the activity used 
        ///     during the evaluation of the condition
        /// </summary>
        public string ReferencedObjective { get; set; }

        /// <summary>
        /// Type: Attribute
        /// The value used as a threshold during measure-based condition evaluations
        /// Range -1.0000 to 1.0000
        /// </summary>
        public float MeasureThreshold { get; set; } = 0;

        /// <summary>
        /// Type: Attribute
        /// The unary logical operator to be applied to the condition
        /// There are 2 values:
        /// 1. not: The corresponding condition is negated in the rule evaluation
        /// 2. noOp: The corresponding condition is used as is in rule evaluation
        /// </summary>
        public string Operator { get; set; } = "noOp";

        /// <summary>
        /// Type: Attribute
        /// Represents the actual condition for the rule
        /// Listing of the vocabulary tokens to be used for the condition attribute:
        /// 1. satisfied
        /// 2. objectiveStatusKnown
        /// 3. objectiveMeasureKnown
        /// 4. objectiveMeasureGreaterThan
        /// 5. objectiveMeasureLessThan
        /// 6. completed
        /// 7. activityProgressKnown
        /// 8. attempted
        /// 9. attempLimitExceeded
        /// 10. timeLimitExceeded
        /// 11. outsideAvailableTimeRange
        /// 12. always
        /// </summary>
        public string Condition { get; set; } = "always";
    }
}
