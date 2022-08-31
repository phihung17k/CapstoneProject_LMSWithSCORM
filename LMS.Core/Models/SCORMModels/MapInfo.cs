using System.Xml;

namespace LMS.Core.Models.SCORMModels
{
    public class MapInfo
    {
        public MapInfo(XmlNode parentNode)
        {
            XmlAttributeCollection attributes = parentNode.Attributes;
            TargetObjectiveID = attributes["targetObjectiveID"]?.Value;

            ReadSatisfiedStatus = attributes["readSatisfiedStatus"] == null
                                ? true : bool.Parse(attributes["readSatisfiedStatus"].Value);

            ReadNormalizedMeasure = attributes["readNormalizedMeasure"] == null
                                ? true : bool.Parse(attributes["readNormalizedMeasure"].Value);

            WriteSatisfiedStatus = attributes["writeSatisfiedStatus"] == null
                                ? false : bool.Parse(attributes["writeSatisfiedStatus"].Value);

            WriteNormalizedMeasure = attributes["writeNormalizedMeasure"] == null
                                ? false : bool.Parse(attributes["writeNormalizedMeasure"].Value);
        }


        /// <summary>
        /// Type: Attribute
        /// Identifier of the global shared objective targeted for the mapping
        /// </summary>
        public string TargetObjectiveID { get; set; }

        /// <summary>
        /// Type: Attribute
        /// Indicates that the satisfaction status for the identified local objective should be retrieved 
        ///     (true or false) from the identified shared global objective 
        ///     when the progress for the local objective is undefined 
        /// Read more in scormcam_1.3.pdf page 231 
        /// </summary>
        public bool ReadSatisfiedStatus { get; set; } = true;

        /// <summary>
        /// Type: Attribute
        /// Indicates that the normalized measure for the identified local objective should be retrieved 
        ///     (true or false) from the identified shared global objective 
        ///     when the measure for the local objective is undefined
        /// Read more in scormcam_1.3.pdf page 231 
        /// </summary>
        public bool ReadNormalizedMeasure { get; set; } = true;

        /// <summary>
        /// Type: Attribute
        /// Indicates that the satisfaction status for the identified local objective should be transferred 
        ///     (true or false) to the identified shared global objective upon termination ( Termination(“”) ) 
        ///     of the attempt on the activity
        /// Read more in scormcam_1.3.pdf page 231 
        /// </summary>
        public bool WriteSatisfiedStatus { get; set; } = false;

        /// <summary>
        /// Type: Attribute
        /// Indicates that the normalized measure for the identified local objective should be transferred 
        ///     (true or false) to the identified shared global objective upon termination ( Termination(“”) ) 
        ///     of the attempt on the activity
        /// Read more in scormcam_1.3.pdf page 231    
        /// </summary>
        public bool WriteNormalizedMeasure { get; set; } = false;
    }
}
