using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class SequencingRules
    {
        public SequencingRules(XmlNode parentNode)
        {
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                if (node.Name.Equals("imsss:preConditionRule"))
                {
                    PreConditionRule = new PreConditionRule(node);
                }
                else if (node.Name.Equals("imsss:sequencingRules"))
                {
                    ExitConditionRule = new ExitConditionRule(node);
                }
                else if (node.Name.Equals("imsss:limitConditions"))
                {
                    PostConditionRule = new PostConditionRule(node);
                }
            }
        }


        /// <summary>
        /// Type: Element
        /// The description of actions that control sequencing decisions and delivery of a specific activity. 
        /// Rules that include such actions are used to determine if the activity will be delivered
        /// </summary>
        public PreConditionRule PreConditionRule { get; set; }

        /// <summary>
        /// Type: Element
        /// The container for the description of actions that control sequencing decisions and 
        /// delivery of a specific activity. 
        /// Rules that include such actions are applied after an activity attempt on a descendent activity terminate
        /// </summary>
        public ExitConditionRule ExitConditionRule { get; set; }

        /// <summary>
        /// Type: Element
        /// The description of actions that control sequencing decisions and delivery of a specific activity. 
        /// Rules that include such actions are applied when the activity attempt terminates
        /// </summary>
        public PostConditionRule PostConditionRule { get; set; }
    }
}
