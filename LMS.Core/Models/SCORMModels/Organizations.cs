using System.Collections.Generic;
using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class Organizations
    {
        public Organizations(XmlNode parentNode)
        {
            Default = parentNode.Attributes["default"]?.Value;
            OrganizationList = new List<Organization>();
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                OrganizationList.Add(new Organization(node));
            }
        }

        /// <summary>
        /// Type: Attribute
        /// Mandatory
        /// Identify the default organization to use
        /// The value of this element must reference an identifier attribute of an <organization> element 
        ///     that is a direct descendent of the <organizations> element
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Type: Element
        /// The content organization can be a lesson, module, course, chapter, etc
        /// </summary>
        public List<Organization> OrganizationList { get; set; }

        public Organization GetDefaultOrganization()
        {
            foreach (Organization organization in OrganizationList)
            {
                if (Default.Equals(organization.Identifier))
                {
                    return organization;
                }
            }
            return null;
        }
    }

    public class Organization
    {
        public Organization(XmlNode parentNode)
        {
            XmlAttributeCollection attributes = parentNode?.Attributes;
            Identifier = attributes["identifier"]?.Value;
            Structure = attributes["structure"]?.Value;
            ObjectivesGlobalToSystem = attributes["adlseq:objectivesGlobalToSystem"] == null
                ? true : bool.Parse(attributes["adlseq:objectivesGlobalToSystem"]?.Value);

            foreach (XmlNode node in parentNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "title":
                        Title = node.InnerText;
                        break;
                    case "item":
                        Item = new Item(node);
                        break;
                    case "metadata":
                        Metadata = new Metadata(node);
                        break;
                    case "imsss:sequencing":
                        Sequencing = new Sequencing(node);
                        break;
                }
            }
        }

        /// <summary>
        /// Type: Attribute
        /// Mandatory
        /// The attribute is unique within the Manifest
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Type: Attribute
        /// Describes the shape of the organization
        /// If not provided, the default value shall be hierarchical
        /// </summary>
        public string Structure { get; set; } = "hierarchical";

        /// <summary>
        /// Type: Attribute
        /// Default value is true
        /// Indicates that any mapped global shared objectives defined in sequencing information <sequencing>
        ///     are either global to the learner and the content organization (false) 
        ///     or global for the lifetime of the learner within the LMS (true) across all content organizations
        /// </summary>
        public bool ObjectivesGlobalToSystem { get; set; } = true;

        /// <summary>
        /// Type: Element
        /// Describes the title of the organization
        /// Help a learner decide which organization to choose. 
        /// Depending on what the organization is describing, this title could be for a course, module, lesson, etc.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Type: Element
        /// Represents an Activity in the content organization
        /// Can be nested and repeated within other <item> elements to any number of levels
        /// This structuring of <item> elements shapes the content organization 
        ///     and describes the relationships between parts of the learning content
        /// If an <item> is a leaf node, then the <item> shall reference a <resource> element
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// Type: Element
        /// Refer to Section 4.5.1.2: Content Organization Meta-data in scormcam.pdf
        /// The attribute is NOT include <schema> and <schemaversion> (meaning set value to null)
        /// </summary>
        public Metadata Metadata { get; set; }

        /// <summary>
        /// Type: Element
        /// </summary>
        public Sequencing Sequencing { get; set; }
    }
}
