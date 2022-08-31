using System.Collections.Generic;
using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class RuleConditions
    {
        public RuleConditions(XmlNode parentNode)
        {
            XmlAttributeCollection attributes = parentNode.Attributes;
            ConditionCombination = attributes["conditionCombination"]?.Value ?? "all";

            RuleConditionList = new List<RuleCondition>();
            foreach (XmlNode node in parentNode)
            {
                if (node.Name.Equals("imsss:ruleCondition"))
                {
                    RuleConditionList.Add(new RuleCondition(node));
                }
            }
        }

        /// <summary>
        /// Type: Attribute
        /// Indicates how rule conditions (<ruleCondition>) are combined in evaluating the rule 
        /// There are 2 values: 
        /// 1. all: The rule condition evaluates to true if and only if 
        ///         ALL of the individual rule conditions evaluates to true
        /// 2. any: The rule condition evaluates to true if and only if 
        ///         ANY of the individual rule conditions evaluates to true
        /// </summary>
        public string ConditionCombination { get; set; } = "all";

        /// <summary>
        /// Type: Element
        /// Represents the condition that is evaluated
        /// </summary>
        public List<RuleCondition> RuleConditionList { get; set; }
    }
}
