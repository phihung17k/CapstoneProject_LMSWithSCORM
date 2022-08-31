using System.Collections.Generic;
using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class Objectives
    {
        public Objectives(XmlNode parentNode)
        {
            ObjectiveList = new List<Objective>();
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                if (node.Name.Equals("imsss:primaryObjective"))
                {
                    PrimaryObjective = new PrimaryObjective(node);
                }
                else if (node.Name.Equals("imsss:objective"))
                {
                    ObjectiveList.Add(new Objective(node));
                }
            }
        }

        /// <summary>
        /// Type: Element
        /// Identifies the objective that contributes to the rollup associated with the activity
        /// </summary>
        public PrimaryObjective PrimaryObjective { get; set; }

        /// <summary>
        /// Type: Element
        /// Identify objectives that do NOT contribute to rollup associated with the activity. 
        /// This element can only exist if a <primaryObjective> has been defined
        /// </summary>
        public List<Objective> ObjectiveList { get; set; }
    }
}
