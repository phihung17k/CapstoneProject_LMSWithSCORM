using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class RuleAction
    {
        public RuleAction(XmlNode parentNode)
        {
            Action = parentNode.Attributes["action"]?.Value;
        }

        /// <summary>
        /// Type: Attribute
        /// The action represents the desired sequencing behavior if the rule condition evaluates to true 
        /// If action is defined in a <preConditionRule>, the action shall have one of the following values: 
        /// 1. skip
        /// 2. disabled
        /// 3. hiddenFromChoice
        /// 4. stopForwardTraversal
        /// If action is defined in a <postConditionRule>, the action shall have one of the following values:
        /// 1. exitParent
        /// 2. exitAll
        /// 3. retry
        /// 4. retryAll
        /// 5. continue
        /// 6. previous
        /// If action is defined in a <exitConditionRule>, the action shall have one of the following values:
        /// 1. exit
        /// </summary>
        public string Action { get; set; }
    }
}
