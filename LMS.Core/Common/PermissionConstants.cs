namespace LMS.Core.Common
{
    public class PermissionConstants
    {
        public const string ClaimType = "Permission";

        public static class BasePermission
        {
            public const string ViewProfile = "BasePermission." + nameof(ViewProfile);
            public const string EditProfile = "BasePermission." + nameof(EditProfile);
            public const string ReceiveNotification = "BasePermission." + nameof(ReceiveNotification);
        }

        public static class Dashboard
        {
            public const string ViewUserRoleAnalytics = "Dashboard." + nameof(ViewUserRoleAnalytics);
            public const string ViewAllCoursesProgressChart = "Dashboard." + nameof(ViewAllCoursesProgressChart);
            public const string ViewAssignedCoursesProgressChart = "Dashboard." + nameof(ViewAssignedCoursesProgressChart);
            public const string ViewAttendeesLearningProgressChartInAllCourses = "Dashboard." + nameof(ViewAttendeesLearningProgressChartInAllCourses);
            public const string ViewAttendeesLearningProgressChartInAssignedCourses = "Dashboard." + nameof(ViewAttendeesLearningProgressChartInAssignedCourses);
            public const string ViewOwnLearningProgressChartInAssignedCourses = "Dashboard." + nameof(ViewOwnLearningProgressChartInAssignedCourses);
        }

        public static class Course
        {
            public const string ViewAssignedCoursesList = "Course." + nameof(ViewAssignedCoursesList);
            public const string ViewAllCourses = "Course." + nameof(ViewAllCourses);
            public const string ViewDetailOfCourse = "Course." + nameof(ViewDetailOfCourse);
            //Update course description
            public const string UpdateCourse = "Course." + nameof(UpdateCourse);

            public const string CreateTopic = "Course." + nameof(CreateTopic);
            public const string UpdateTopic = "Course." + nameof(UpdateTopic);
            public const string DeleteTopic = "Course." + nameof(DeleteTopic);

            public const string AddLearningResource = "Course." + nameof(AddLearningResource);
            //Update completionThresold
            public const string UpdateLearningResource = "Course." + nameof(UpdateLearningResource);
            public const string DeleteLearningResource = "Course." + nameof(DeleteLearningResource);
            public const string ViewContentOfLearningResources = "Course." + nameof(ViewContentOfLearningResources);

            public const string ViewAttendeesList = "Course." + nameof(ViewAttendeesList);
            public const string ViewSummaryOfQuizResults = "Course." + nameof(ViewSummaryOfQuizResults);
            public const string ViewDetailOfQuizResultOfStudent = "Course." + nameof(ViewDetailOfQuizResultOfStudent);

            public const string ViewListOfSurveyResultsInAllCourses = "Course." + nameof(ViewListOfSurveyResultsInAllCourses);
            public const string ViewListOfSurveyResultsInAssignedCourses = "Course." + nameof(ViewListOfSurveyResultsInAssignedCourses);
            public const string ViewDetailOfSurveyResult = "Course." + nameof(ViewDetailOfSurveyResult);
            public const string ViewSummaryOfSurveyResults = "Course." + nameof(ViewSummaryOfSurveyResults);

            public const string ViewSummaryOfLearningProcessOfStudent = "Course." + nameof(ViewSummaryOfLearningProcessOfStudent);
            public const string ViewDetailOfLearningProcessOfStudent = "Course." + nameof(ViewDetailOfLearningProcessOfStudent);

            public const string CreateQuiz = "Course." + nameof(CreateQuiz);
            public const string UpdateQuiz = "Course." + nameof(UpdateQuiz);
            public const string DeleteQuiz = "Course." + nameof(DeleteQuiz);
            public const string AttemptAndReattemptQuiz = "Course." + nameof(AttemptAndReattemptQuiz);
            public const string ReviewYourOwnAttempts = "Course." + nameof(ReviewYourOwnAttempts);
            public const string PreviewQuiz = "Course." + nameof(PreviewQuiz);

            public const string AddSurveyFromTemplate = "Course." + nameof(AddSurveyFromTemplate);
            public const string UpdateSurvey = "Course." + nameof(UpdateSurvey);
            public const string DeleteSurvey = "Course." + nameof(DeleteSurvey);
            public const string DoAndEditSurvey = "Course." + nameof(DoAndEditSurvey);
            public const string PreviewSurvey = "Course." + nameof(PreviewSurvey);

            public const string ViewStudentMarkReport = "Course." + nameof(ViewStudentMarkReport);
            public const string ViewOwnMarkReport = "Course." + nameof(ViewOwnMarkReport);
            public const string ViewCoursesGrades = "Course." + nameof(ViewCoursesGrades);
        }

        public static class SurveyTemplate
        {
            public const string ViewSurveyTemplatesList = "SurveyTemplate." + nameof(ViewSurveyTemplatesList);
            public const string ViewDetailOfSurveyTemplate = "SurveyTemplate." + nameof(ViewDetailOfSurveyTemplate);
            public const string CreateSurveyTemplate = "SurveyTemplate." + nameof(CreateSurveyTemplate);
            public const string UpdateSurveyTemplate = "SurveyTemplate." + nameof(UpdateSurveyTemplate);
            public const string DeleteSurveyTemplate = "SurveyTemplate." + nameof(DeleteSurveyTemplate);
        }

        public static class QuestionBank
        {
            public const string CreateQuestionBank = "QuestionBank." + nameof(CreateQuestionBank);
            public const string UpdateQuestionBank = "QuestionBank." + nameof(UpdateQuestionBank);
            public const string DeleteQuestionBank = "QuestionBank." + nameof(DeleteQuestionBank);
        }

        public static class Question
        {
            public const string ViewQuestionsList = "Question." + nameof(ViewQuestionsList);
            public const string ViewDetailOfQuestion = "Question." + nameof(ViewDetailOfQuestion);
            public const string CreateQuestion = "Question." + nameof(CreateQuestion);
            public const string UpdateQuestion = "Question." + nameof(UpdateQuestion);
            public const string DeleteQuestion = "Question." + nameof(DeleteQuestion);
        }

        public static class Subject
        {
            public const string ViewSubjectsList = "Subject." + nameof(ViewSubjectsList);
            public const string ViewAssignedSubjectsList = "Subject." + nameof(ViewAssignedSubjectsList);
            public const string ViewDetailOfSubject = "Subject." + nameof(ViewDetailOfSubject);

            public const string UpdateSubject = "Subject." + nameof(UpdateSubject);

            public const string CreateSection = "Subject." + nameof(CreateSection);
            public const string UpdateSection = "Subject." + nameof(UpdateSection);
            public const string DeleteSection = "Subject." + nameof(DeleteSection);

            public const string ViewLearningResourcesList = "Subject." + nameof(ViewLearningResourcesList);
            public const string ViewContentOfLearningResourcesInSubject = "Subject." + nameof(ViewContentOfLearningResourcesInSubject);
            public const string AddLearningResource = "Subject." + nameof(AddLearningResource);
            public const string DeleteLearningResource = "Subject." + nameof(DeleteLearningResource);
        }

        public static class Role
        {
            public const string ViewRolesList = "Role." + nameof(ViewRolesList);
            public const string ViewDetailOfRole = "Role." + nameof(ViewDetailOfRole);
            public const string CreateRole = "Role." + nameof(CreateRole);
            public const string UpdateRole = "Role." + nameof(UpdateRole);
            public const string DeleteRole = "Role." + nameof(DeleteRole);
        }

        public static class Account
        {
            public const string ViewUsersList = "Account." + nameof(ViewUsersList);
            public const string ViewDetailOfUser = "Account." + nameof(ViewDetailOfUser);
            public const string AssignRoleToUser = "Account." + nameof(AssignRoleToUser);
            public const string UpdateStatus = "Account." + nameof(UpdateStatus);
        }
    }
}
