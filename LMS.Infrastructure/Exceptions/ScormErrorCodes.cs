namespace LMS.Infrastructure.Exceptions
{
    public class GeneralScormErrorCodes
    {
        //No Error
        public const string E0 = "0";

        //General Argument Error
        public const string E201 = "201";
    }

    //SCORM 2004
    public class ScormErrorCodes : GeneralScormErrorCodes
    {
        //No other specific error code is applicable
        public const string E301 = "301";

        //General Set Failure
        public const string E351 = "351";

        //Undefined Data Model Element
        public const string E401 = "401";

        //Data Model Element Value Not Initialized
        public const string E403 = "403";

        //Data Model Element Is Read Only
        public const string E404 = "404";

        //Data Model Element Is Write Only
        public const string E405 = "405";

        //Data Model Element Type Mismatch
        public const string E406 = "406";

        //Data Model Element Value Out Of Range
        public const string E407 = "407";

        //Data Model Dependency Not Established
        public const string E408 = "408";
    }

    //SCORM 1.2
    public class Scorm12ErrorCodes : GeneralScormErrorCodes
    {
        //Not initialized
        public const string E301 = "301";

        //Invalid set value, element is a keyword
        public const string E402 = "402";

        //Element is read only
        public const string E403 = "403";

        //Element is write only
        public const string E404 = "404";

        //Incorrect data type
        public const string E405 = "405";
    }
}
