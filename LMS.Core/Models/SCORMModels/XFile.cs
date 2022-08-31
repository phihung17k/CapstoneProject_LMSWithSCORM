using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class XFile
    {
        public XFile(XmlNode parentNode)
        {
            Href = parentNode.Attributes["href"]?.Value;
            foreach (XmlNode node in parentNode)
            {
                if (node.Name.Equals("metadata"))
                {
                    Metadata = new Metadata(node);
                }
            }
        }

        /// <summary>
        /// Type: Attribute
        /// Mandatory
        /// Identifies the location of the file
        /// This value is affected by the use of xml:base values. 
        ///     Refer to Section 3.4.4.1: Handling the XML Base Attribute
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Type: Element
        /// Defines the metadata that is used to describe the <file> as Asset Meta-data. 
        ///     Refer to Section 4.5.1.5: Asset Meta-data
        /// The attribute is NOT include <schema> and <schemaversion> (meaning set value to null)
        /// </summary>
        public Metadata Metadata { get; set; }
    }
}
