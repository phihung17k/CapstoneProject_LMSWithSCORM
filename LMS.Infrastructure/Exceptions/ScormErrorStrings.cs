namespace LMS.Infrastructure.Exceptions
{
    public class GeneralScormErrorStrings
    {
        //No Error
        public const string E0 = "No error occurred";

        //General Argument Error
        public const string E201 = "An invalid argument was passed";
    }


    //SCORM 2004
    public class ScormErrorStrings : GeneralScormErrorStrings
    {
        //General Set Failure
        public const string E351 = "A general set failure has occurred and no other information on the error is available";

        //Undefined Data Model Element
        public const string E401 = "Data model element name passed is not a valid SCORM data model element";

        //Data Model Element Value Not Initialized
        public const string E403 = "Data model element has not been initialized by the LMS";

        //Data Model Element Is Read Only
        public const string E404 = "Data model element can only be read";

        //Data Model Element Is Write Only
        public const string E405 = "Data model element can only be written";

        //Data Model Element Type Mismatch
        public const string E406 = "Value is not consistent with the data format of the supplied data model element";

        //Data Model Element Value Out Of Range
        public const string E407 =
            "The numeric value supplied is outside of the numeric range allowed for the supplied data model element";

        //Data Model Dependency Not Established
        public const string E408 = "The prerequisite element was not set before the dependent element";

        //No cmi_core record for this session
        public const string NoCMICore = "No cmi_core record for this session";

        //Data Model Collection Element Request Out Of Range
        public const string OutOfRange = "Data Model Collection Element Request Out Of Range";

        //Element cannot have children
        public const string CannotHaveChildren = "Element cannot have children";

        //Element not an array - Cannot have count
        public const string CannotHaveCount = "Element not an array - Cannot have count";
    }

    //SCORM 1.2
    public class Scorm12ErrorStrings : GeneralScormErrorStrings
    {
        //Not initialized
        public const string E301 = "Not initialized";

        //Invalid set value, element is a keyword
        public const string E402 = "Invalid set value, element is a keyword";

        //Element is read only
        public const string E403 = "Element is read only";

        //Element is write only
        public const string E404 = "Element is write only";

        //Incorrect data type
        public const string E405 = "Incorrect data type";
    }
}
