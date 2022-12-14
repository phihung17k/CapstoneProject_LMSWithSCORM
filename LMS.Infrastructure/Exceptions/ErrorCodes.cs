namespace LMS.Infrastructure.Exceptions
{
    public class ErrorCodes
    {
        public const string Undefined = "undefined";
        public const string DataIsEmpty = "data-is-empty";
        public const string RoleNameExist = "role-name-exist";
        public const string RoleExistInUser = "role-exist-in-user";
        public const string NotFound = "not-found";
        public const string ValueNotValid = "value-not-valid";
        public const string CourseIsStarted = "course-is-started";
        public const string DataListIsEmpty = "data-list-is-empty";
        public const string DataListIsNull = "data-list-is-null";
        public const string QuestionBankContentExist = "questionBank-content-exist";
        public const string OptionsDuplicate = "options-are-duplicated";
        public const string QuestionDuplicate = "question-is-duplicated";
        public const string QuestionBankNotExist = "question-bank-not-exist";
        public const string CorrectOptionNotExist = "correct-option-not-exist";
        public const string OverManyCorrectOptions = "over-many-correct-options";
        public const string QuestionEmptyContent = "content-question-empty";
        public const string OptionNotExist = "option-not-exist";
        public const string OptionIsEmpty = "option-is-empty";
        public const string TopicQuestionIsEmpty = "topic-question-is-empty";
        public const string InvalidFile = "invalid-file";
        public const string UnsupportedFile = "unsupported-file";
        public const string FileTooLarge = "file-too-large";
        public const string TemplateQuestionDuplicate = "template-question-duplicate";
        public const string FileManifestNotFound = "file-manifest-not-found";
        public const string ResourceElementNotFound = "resource-element-not-found";
        public const string AttributesInResourceElementNotFound = "attributes-in-resource-element-not-found";
        public const string HrefAttributeInResourceElementNotFound = "href-attribute-in-resource-element-not-found";
        public const string TemplateNameExisted = "template-name-existed";
        public const string InvalidContentInManifest = "invalid-content-in-manifest";
        public const string ResourceIsExistedInTopic = "resource-is-existed-in-topic";
        public const string ResourceDeleteError = "resource-deleteing-error";
        //The input data does not match in the database
        public const string ParametersNotMatch = "parameters-not-match";
        public const string SomethingWentWrong = "something-went-wrong";
        public const string FileNotFound = "file-not-found";
        public const string FolderNotFound = "folder-not-found";
        public const string ResourceNotAvaiable = "resource-not-avaiable";
        public const string SurveyQuestionDuplicate = "survey-question-duplicate";
        public const string SurveyNameExisted = "survey-name-existed";
        public const string SurveyIsTaken = "survey-is-taken";
        public const string TimeSpanNotValid = "timespan-not-valid";
        public const string StartEndTimeNotValid = "start-end-time-not-valid";
        public const string NumOfQuestionsIsOutOfRange = "number-of-questions-is-out-of-range";
        public const string UserIsNotFound = "user-not-found";
        public const string SurveyIsNotFound = "survey-not-found";
        public const string QuestionInSurveyIsNotFound = "question-in-survey-not-found";
        public const string OptionInSurveyIsNotFound = "option-in-survey-not-found";
        public const string QuizIsStarted = "quiz-is-started";
        public const string NumOfAttemptIsExceed = "num-of-attempt-is-exceed";
        public const string QuizIsNotStart = "quiz-is-not-start";
        public const string QuizIsClosed = "quiz-is-closed";
        public const string AttemptCompleted = "attempt-is-completed";
        public const string QuestionNotFound = "question-not-found";
        public const string QuestionBankHasQuestion = "questionbank-has-question";
        public const string AttemptInProgress = "attempt-is-in-progress";
        public const string TemplateNotFound = "template-not-found";
        public const string TemplateQuestionNotFound = "template-question-not-found";
        public const string TopicNotFound = "topic-not-found";
        public const string NumberOfQuestionIsNotMatch = "number-of-question-is-not-match";
        public const string SearchedRoleIsNotSameExceptRole = "searched-role-is-not-same-except-role";
        public const string ConvertTMSDataModelFail = "convert-tms-data-model-fail";
        public const string TopicIsExisted = "topic-is-existed";
        public const string QuestionIsExisted = "question-is-existed";
        public const string Forbidden = "accessibility-is-forbidden";
        public const string UserCourseNotFound = "user-course-not-found";
        public const string ResourceInTopicIsNotFound = "resource-topic-not-found";
        public const string CourseNotFound = "course-not-found";
        public const string CannotAccessUpcomingCourse = "cannot-access-upcoming-course";
        public const string CannotPerformAction = "cannot-perform-action";
        public const string CourseStartEndOutOfRange = "course-start-end-out-of-range";
        public const string QuizIsOpened = "quiz-is-opened";
        public const string SubjectNotFound = "subject-not-found";
        public const string SectionIsExisted = "section-is-existed";
        public const string SectionNotFound = "section-not-found";
        public const string SectionsIsEmptyInSubject = "sections-is-empty-in-subject";
        public const string TopicNameIsSameSectionName = "topic-name-is-same-section-name";
        public const string RestrictionError = "restriction-error";
        public const string QuizNameExisted = "quiz-name-existed";
    }
}