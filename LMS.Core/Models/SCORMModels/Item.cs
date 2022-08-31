using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class Item
    {
        public Item(XmlNode parentNode)
        {
            XmlAttributeCollection attributes = parentNode?.Attributes;
            Identifier = attributes["identifier"]?.Value;
            Identifierref = attributes["identifierref"]?.Value;
            IsVisible = attributes["isvisible"] == null ? true : bool.Parse(attributes["isvisible"]?.Value);
            Parameters = attributes["parameters"]?.Value;

            foreach (XmlNode node in parentNode.ChildNodes)
            {
                switch (node.Name.ToLower())
                {
                    case "title":
                        Title = node.InnerText;
                        break;
                    case "item":
                        SubItem = new Item(node);
                        break;
                    case "metadata":
                        Metadata = new Metadata(node);
                        break;
                    case "adlcp:timelimitaction":
                        TimeLimitAction = node.InnerText;
                        break;
                    case "adlcp:datafromlms":
                        DataFromLMS = node.InnerText;
                        break;
                    case "adlcp:completionthreshold":
                        CompletionThreshold = node.InnerText;
                        break;
                    case "imsss:sequencing":
                        Sequencing = new Sequencing(node);
                        break;
                    case "adlnav:presentation":
                        Presentation = node.InnerText;
                        break;
                    case "adlcp:masteryscore":
                        MasteryScore = node.InnerText;
                        break;
                    case "adlcp:maxtimeallowed":
                        MaxTimeAllowed = node.InnerText;
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
        /// The attribute is a reference to an identifier in the <resources> section or a (sub)manifest
        /// If no identifierref is supplied, there is no content associated with this entry in the organization
        /// </summary>
        public string Identifierref { get; set; }

        /// <summary>
        /// Type: Attribute
        /// Indicates whether or not this item is displayed when the structure of the package is displayed or rendered
        /// If not present, value is defaulted to be true
        /// Only affects the item for which it is defined and not the children of the item 
        ///     or a resource associated with an item
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Type: Attribute
        /// Contains the static parameters to be passed to the resource at launch time
        /// The attribute should only be used for <item> elements that reference <resource> elements
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// Type: Element
        /// Describes the title of the item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Type: Element
        /// Can appear 0 or More times as a child
        /// </summary>
        public Item SubItem { get; set; }

        /// <summary>
        /// Type: Element
        /// Describe the <item> as Activity Meta-data. Refer to Section 4.5.1.3: Activity Meta-data 
        /// The attribute is NOT include <schema> and <schemaversion> (meaning set value to null)
        /// </summary>
        public Metadata Metadata { get; set; }

        /// <summary>
        /// Type: Element
        /// Defines the action that should be taken 
        ///     when the maximum time allowed in the current attempt of the activity is exceeded
        /// Only those <item> elements that reference a SCO resource can contain the <timeLimitAction> element
        /// LMS shall use the value of the <timeLimitAction> element, if provided, 
        ///     to initialize the cmi.time_limit_action data model element
        /// Set of restricted characterstring tokens: 
        ///     "exit,message"; "exit,no message", "continue,message", "continue,no message"
        /// </summary>
        public string TimeLimitAction { get; set; }

        /// <summary>
        /// Type: Element
        /// Provides initialization data expected by the resource (i.e., SCO) represented by the <item> after launch
        /// This element shall not be used for parameters that the SCO may need during the launch. 
        ///     If this type of functionality is required, 
        ///     then the developer should use the parameters attribute of the item referencing the SCO resource
        /// Only those <item> elements that reference a SCO resource can contain the <dataFromLMS> element
        /// LMS shall use the value of the <dataFromLMS> element, if provided, 
        ///     to initialize the cmi.launch_data data model element
        /// </summary>
        public string DataFromLMS { get; set; }

        /// <summary>
        /// Type: Element
        /// Defines a threshold value that can be used by the SCO resource referenced by the <item>
        /// Only those <item> elements that reference a SCO resource can contain the <completionThreshold> element
        /// LMS shall use the value of the <completionThreshold> element, 
        ///     if provided, to initialize the cmi.completion_threshold data model element
        /// The range of 0.0 and 1.0 element
        /// </summary>
        public string CompletionThreshold { get; set; }

        /// <summary>
        /// Type: Element
        /// If a leaf <item> element references a (sub)manifest, 
        ///     that leaf <item> element may not have any sequencing information (a <sequencing> element)
        /// Reference to https://www.immagic.com/eLibrary/ARCHIVES/TECH/US_DOD/A090814S.pdf
        /// 
        /// Comming soon
        /// </summary>
        public Sequencing Sequencing { get; set; }

        /// <summary>
        /// Type: Element
        /// comming soon
        /// </summary>
        public string Presentation { get; set; }

        /// <summary>
        /// Type: Element
        /// mastery score in SCORM 1.2. Range [0; 100]
        /// Similar to minNormalizedMeasure in SCORM 2004. Range [-1; 1]
        /// </summary>
        public string MasteryScore { get; set; }

        /// <summary>
        /// Type: Element
        /// use in SCORM 1.2
        /// Similar to attemptAbsoluteDurationLimit attribute in SCORM 2004 
        /// </summary>
        public string MaxTimeAllowed { get; set; }
    }
}
