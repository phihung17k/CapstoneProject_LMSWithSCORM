namespace LMS.Core.Enum
{
    //the enum contains the named function in controllers or use case
    // user to check course start end
    public enum ActionMethods
    {
        Nothing,    //default, the method is not need to check specifically
        ManageTopic, //include create, update, delete
        ManageLearningResource,
        ManageQuiz,
        ManageSurvey, //use for upcoming course
        UpdateCourse //update description in upcoming course
    }
}
