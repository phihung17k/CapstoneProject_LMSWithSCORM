using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class Manifest
    {
        public Manifest(XmlDocument document)
        {
            XmlElement manifest = document.DocumentElement;
            XmlAttributeCollection attributes = manifest?.Attributes;
            Base = attributes["xml:base"]?.Value;
            Xmlns = attributes["xmlns"]?.Value;
            Adlcp = attributes["xmlns:adlcp"]?.Value;
            Adlseq = attributes["xmlns:adlseq"]?.Value;
            Adlnav = attributes["xmlns:adlnav"]?.Value;
            Imsss = attributes["xmlns:imsss"]?.Value;
            Xsi = attributes["xmlns:xsi"]?.Value;
            Identifier = attributes["identifier"]?.Value;
            Version = attributes["version"]?.Value;
            SchemaLocation = attributes["xsi:schemaLocation"]?.Value;

            foreach (XmlNode node in manifest.ChildNodes)
            {
                switch (node.Name)
                {
                    case "metadata":
                        Metadata = new Metadata(node);
                        break;
                    case "organizations":
                        Organizations = new Organizations(node);
                        break;
                    case "resources":
                        Resources = new Resources(node);
                        break;
                }
            }

            DefaultOrganization = Organizations.GetDefaultOrganization();
            if (DefaultOrganization != null)
            {
                Title = DefaultOrganization.Title;
                LoadAdditionInformation(DefaultOrganization);
            }
        }

        /// <summary>
        /// Type: Attribute
        /// Provides a relative path offset for the content file(s) contained in the manifest, similar <include>
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// Type: Attribute
        /// </summary>
        public string Xmlns { get; set; }

        /// <summary>
        /// Type: Attribute
        /// </summary>
        public string Adlcp { get; set; }

        /// <summary>
        /// Type: Attribute
        /// </summary>
        public string Adlseq { get; set; }

        /// <summary>
        /// Type: Attribute
        /// </summary>
        public string Adlnav { get; set; }

        /// <summary>
        /// Type: Attribute
        /// </summary>
        public string Imsss { get; set; }

        /// <summary>
        /// Type: Attribute
        /// </summary>
        public string Xsi { get; set; }

        /// <summary>
        /// Type: Attribute
        /// Mandatory
        /// The attribute identifies the manifest. It is unique within the Manifest
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Type: Attribute
        /// Identifies the version of the Manifest
        /// Used to distinguish between manifests with the same identifier
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Type: Attribute
        /// Describe the location where the meta-data describing the SCORM Content Model Component 
        /// </summary>
        public string SchemaLocation { get; set; }

        /// <summary>
        /// Type: Element
        /// Contains relevant information that describes the content package (i.e., Content Aggregation) as a whole
        /// </summary>
        public Metadata Metadata { get; set; }

        /// <summary>
        /// Type: Element
        /// Describes one or more structures or organizations for the content package
        /// </summary>
        public Organizations Organizations { get; set; }

        /// <summary>
        /// Type: Element
        /// Collection of references to resources
        /// </summary>
        public Resources Resources { get; set; }

        /// <summary>
        /// Type: Element
        /// 
        /// Comming soon
        /// </summary>
        public Manifest SubManifeset { get; set; }

        /// <summary>
        /// Type: Element
        /// 
        /// Comming soon
        /// </summary>
        public string SequencingCollection { get; set; }

        //addition information: external property
        public string StandAloneIndexPage { get; set; }
        public string HrefOfDefaultResource { get; set; }
        public string Title { get; set; }
        public Organization DefaultOrganization { get; set; }

        public string GetSCORMVersion()
        {
            string ScormVersion = "1.3";
            if (Metadata.SchemaVersion == null)
            {
                string namespaceString = Adlcp;
                switch (namespaceString.ToLower())
                {
                    case "http://www.adlnet.org/xsd/adlcp_rootv1p1":
                        ScormVersion = "1.1";
                        break;
                    case "http://www.adlnet.org/xsd/adlcp_rootv1p2":
                        ScormVersion = "1.2";
                        break;
                    case "http://www.adlnet.org/xsd/adlcp_v1p3":
                        ScormVersion = "1.3";
                        break;
                }
            }
            else
            {
                ScormVersion = Metadata.SchemaVersion;
            }
            return ScormVersion;
        }

        public void LoadAdditionInformation(Organization defaultOrganization)
        {
            string identifierRef = defaultOrganization.Item.Identifierref;
            foreach (Resource resource in Resources.ResourceList)
            {
                if (identifierRef.Equals(resource.Identifier))
                {
                    StandAloneIndexPage = resource.GetStandAloneIndexPage();
                    HrefOfDefaultResource = resource.Href;
                }
            }
        }
    }
}
