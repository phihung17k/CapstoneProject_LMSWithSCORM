using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class ExitConditionRule
    {
        public ExitConditionRule(XmlNode parentNode)
        {
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                if (node.Name.Equals("imsss:ruleConditions"))
                {
                    RuleConditions = new RuleConditions(node);
                }
                else if (node.Name.Equals("imsss:ruleAction"))
                {
                    RuleAction = new RuleAction(node);
                }
            }
        }

        /// <summary>
        /// Type: Element
        /// The set of conditions that are to be applied either the pre-condition, post-condition 
        ///     and exit condition rules
        /// </summary>
        public RuleConditions RuleConditions { get; set; }

        /// <summary>
        /// Type: Element
        /// The desired sequencing behavior if the rule evaluates to true. 
        /// The set of rule actions vary depending on the type of condition 
        ///     (<preConditionRule>, <postConditionRule>, or <exitConditionRule>) 
        /// </summary>
        public RuleAction RuleAction { get; set; }
    }
}
