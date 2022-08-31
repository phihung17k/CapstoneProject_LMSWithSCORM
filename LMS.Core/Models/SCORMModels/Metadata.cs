using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class Metadata
    {
        public Metadata(XmlNode parentNode)
        {
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "schema":
                        Schema = node.InnerText;
                        break;
                    case "schemaversion":
                        SchemaVersion = node.InnerText;
                        break;
                    case "adlcp:location":
                        Location = node.InnerText;
                        break;
                }
            }
        }

        /// <summary>
        /// Type: Element
        /// describes the schema that defines and controls the Manifest 
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// Type: Element
        /// describes the version of the above <schema>
        /// </summary>
        public string SchemaVersion { get; set; }

        /// <summary>
        /// Type: Element
        /// describe the location where the meta-data describing the SCORM Content Model Component may be found
        /// </summary>
        public string Location { get; set; }
    }
}
