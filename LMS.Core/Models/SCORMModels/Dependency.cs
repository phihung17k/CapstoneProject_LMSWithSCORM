using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class Dependency
    {
        public Dependency(XmlNode parentNode)
        {
            Identifierref = parentNode.Attributes["identifierref"]?.Value;
        }

        /// <summary>
        /// Type: Attribute
        /// References an identifier attribute of a <resource> (within the same package) or a (sub)manifest
        /// </summary>
        public string Identifierref { get; set; }
    }
}
