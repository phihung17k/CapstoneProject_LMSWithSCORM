using System.Collections.Generic;
using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class Resources
    {
        public Resources(XmlNode parentNode)
        {
            Base = parentNode.Attributes["xml:base"]?.Value;
            ResourceList = new List<Resource>();
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                ResourceList.Add(new Resource(node));
            }
        }

        /// <summary>
        /// Type: Attribute
        /// Provides a relative path offset for the content file contained in the manifest
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// Type: Element
        /// A reference to a resource, two primary types of resources defined within SCORM: SCOs, Assets
        /// If an <item> references a <resource>, this resource is subject to being identified for delivery 
        ///     and launch to the learner
        /// Then the resource shall meet the following requirements:
        /// 1. The type attribute shall be set to webcontent
        /// 2. The adlcp:scormType shall be set to sco or asset
        /// 3. The href attribute shall be required.
        /// </summary>
        public List<Resource> ResourceList { get; set; }
    }

    public class Resource
    {
        public Resource(XmlNode parentNode)
        {
            XmlAttributeCollection attributes = parentNode.Attributes;
            Identifier = attributes["identifier"]?.Value;
            Type = attributes["type"]?.Value;
            Href = attributes["href"]?.Value;
            Base = attributes["xml:base"]?.Value;
            ScormType = attributes["adlcp:scormType"]?.Value;
            PersistState = attributes["adlcp:persistState"] == null
                ? true : bool.Parse(attributes["adlcp:persistState"].Value);

            foreach (XmlNode node in parentNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "metadata":
                        Metadata = new Metadata(node);
                        break;
                    case "file":
                        FileList.Add(new XFile(node));
                        break;
                    case "dependency":
                        Dependency = new Dependency(node);
                        break;
                }
            }
        }


        /// <summary>
        /// Type: Attribute
        /// Mandatory
        /// Represents an identifier, of the resource, that is unique within the scope of its containing manifest file
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Type: Attribute
        /// Mandatory
        /// Indicates the type of resource
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Type: Attribute
        /// A reference a Uniform Resource Locator (URL), may be External fully qualified URLs
        /// Represents the “entry point” or “launching point” of this resource
        /// This value is affected by the use of xml:base values. 
        ///     Refer to Section 3.4.4.1: Handling the XML Base Attribute
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Type: Attribute
        /// Provides a relative path offset for the content file contained in the manifest
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// Type: Attribute
        /// Defines the type of SCORM resource
        /// Restricted characterstring tokens: sco or asset
        /// </summary>
        public string ScormType { get; set; }

        /// <summary>
        /// Type: Attribute
        /// Provides a means to persist data from learner attempt to learner attempt
        /// If the adlcp:persistState attribute is defined and set to true, 
        ///     then the old learner attempt data shall be used for initializing the new learner attempt.
        /// The default value, if no attribute is provided, is false
        /// </summary>
        public bool PersistState { get; set; } = false;

        /// <summary>
        /// Type: Element
        /// Describe the <resource> as either SCO Meta-data or Asset Meta-data
        /// This depends on the SCORM type (adlcp:scormType) of resource.
        ///     Refer to Section 4.5.1: Associating Meta-data with SCORM Components
        /// The attribute is NOT include <schema> and <schemaversion> (meaning set value to null)
        /// </summary>
        public Metadata Metadata { get; set; }

        /// <summary>
        /// Type: Element
        /// A listing of files that this resource is dependent on.
        /// The element acts as an inventory system detailing the set of files used to build the resource
        /// The element shall be used to represent the file relative to the resource in which it is used
        /// </summary>
        public List<XFile> FileList { get; set; } = new();

        /// <summary>
        /// Type: Element
        /// Identify a <resource> depends on
        /// </summary>
        public Dependency Dependency { get; set; }

        public string GetStandAloneIndexPage()
        {
            foreach (XFile file in FileList)
            {
                if (file.Href.Contains("story.html"))
                {
                    return file.Href;
                }
            }
            return null;
        }
    }
}
