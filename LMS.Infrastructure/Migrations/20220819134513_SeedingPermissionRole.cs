using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class SeedingPermissionRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "id", "category", "code", "description", "name" },
                values: new object[,]
                {
                    { 1, "BasePermission", "ViewProfile", "View profile", "View profile" },
                    { 56, "Subject", "DeleteSection", "Delete section in subject", "Delete section" },
                    { 55, "Subject", "UpdateSection", "Update section in subject", "Update section" },
                    { 54, "Subject", "CreateSection", "Create new section in subject", "Create section" },
                    { 53, "Subject", "ViewDetailOfSubject", "View detail of subject", "View detail of subject" },
                    { 52, "Subject", "ViewAssignedSubjectsList", "View list of assigned subjects", "View list of assigned subjects" },
                    { 51, "Subject", "ViewSubjectsList", "View list of subjects", "View list of subjects" },
                    { 50, "Question", "DeleteQuestion", "Delete question", "Delete question" },
                    { 49, "Question", "UpdateQuestion", "Update question", "Update question" },
                    { 48, "Question", "CreateQuestion", "Create question", "Create question" },
                    { 47, "Question", "ViewDetailOfQuestion", "View detail of a question", "View detail of a question" },
                    { 46, "Question", "ViewQuestionsList", "View list of questions in a question bank", "View list of questions in a question bank" },
                    { 45, "QuestionBank", "DeleteQuestionBank", "Delete question bank", "Delete question bank" },
                    { 44, "QuestionBank", "UpdateQuestionBank", "Update question bank", "Update question bank" },
                    { 43, "QuestionBank", "CreateQuestionBank", "Create question bank", "Create question bank" },
                    { 42, "SurveyTemplate", "DeleteSurveyTemplate", "Delete the survey template", "Delete the survey template" },
                    { 57, "Subject", "ViewLearningResourcesList", "View list of learning resources in a subject", "View list of learning resources in a subject" },
                    { 41, "SurveyTemplate", "UpdateSurveyTemplate", "Update the survey template", "Update the survey template" },
                    { 58, "Subject", "ViewContentOfLearningResourcesInSubject", "View content of learning resource in a subject", "View content of learning resource in a subject" },
                    { 60, "Subject", "DeleteLearningResource", "Delete the learning resource", "Delete the learning resource" },
                    { 75, "Dashboard", "ViewUserRoleAnalytics", "View user and role analytics", "View user and role analytics" },
                    { 74, "Dashboard", "ViewOwnLearningProgressChartInAssignedCourses", "View own learning progress chart in assigned courses", "View own learning progress chart in assigned courses" },
                    { 73, "Dashboard", "ViewAttendeesLearningProgressChartInAssignedCourses", "View attendees learning progress chart in assigned courses", "View attendees learning progress chart in assigned courses" },
                    { 72, "Dashboard", "ViewAttendeesLearningProgressChartInAllCourses", "View attendees learning progress chart in all courses", "View attendees learning progress chart in all courses" },
                    { 71, "Dashboard", "ViewAssignedCoursesProgressChart", "View progress chart for assigned courses", "View progress chart for assigned courses" },
                    { 70, "Dashboard", "ViewAllCoursesProgressChart", "View progress chart for all courses", "View progress chart for all courses" },
                    { 69, "Account", "UpdateStatus", "Update status of user account", "Update status" },
                    { 68, "Account", "AssignRoleToUser", "Assign role to user", "Assign role to user" },
                    { 67, "Account", "ViewDetailOfUser", "View detail of user", "View detail of user" },
                    { 66, "Account", "ViewUsersList", "View list of users", "View list of users" },
                    { 65, "Role", "DeleteRole", "Delete role", "Delete role" },
                    { 64, "Role", "UpdateRole", "Update role", "Update role" },
                    { 63, "Role", "CreateRole", "Create role", "Create role" },
                    { 62, "Role", "ViewDetailOfRole", "View detail of role", "View detail of role" },
                    { 61, "Role", "ViewRolesList", "View list of roles", "View list of roles" },
                    { 59, "Subject", "AddLearningResource", "Add a new learning resource", "Add a new learning resource" },
                    { 39, "SurveyTemplate", "ViewDetailOfSurveyTemplate", "View detail of survey template", "View detail of survey template" },
                    { 40, "SurveyTemplate", "CreateSurveyTemplate", "Create a new survey template", "Create a new survey template" },
                    { 37, "Course", "ViewCoursesGrades", "View list of course's grades", "View list of course's grades" },
                    { 17, "Course", "ViewDetailOfQuizResultOfStudent", "View detail of student’s quiz result", "View detail of student’s quiz result" },
                    { 16, "Course", "ViewSummaryOfQuizResults", "View summary of quiz results of students", "View summary of quiz results" },
                    { 15, "Course", "ViewAttendeesList", "View list attendees in course", "View list attendees in course" },
                    { 14, "Course", "ViewContentOfLearningResources", "View content of learning resource", "View content of learning resource" },
                    { 13, "Course", "DeleteLearningResource", "Delete the learning resource in topic", "Delete the learning resource" },
                    { 12, "Course", "UpdateLearningResource", "Update learning resource", "Update learning resource" },
                    { 11, "Course", "AddLearningResource", "Add a new learning resource in topic", "Add a new learning resource" },
                    { 38, "SurveyTemplate", "ViewSurveyTemplatesList", "View list of survey templates", "View list of survey templates" },
                    { 10, "Course", "DeleteTopic", "Delete topic in course", "Delete topic" },
                    { 8, "Course", "CreateTopic", "Create topic in course", "Create topic" },
                    { 7, "Course", "UpdateCourse", "Update course", "Update course" },
                    { 6, "Course", "ViewDetailOfCourse", "View detail of course include description, outline, topics", "View detail of course" },
                    { 5, "Course", "ViewAllCourses", "View all of courses in system", "View all of courses" },
                    { 4, "Course", "ViewAssignedCoursesList", "View assigned courses", "View list of assigned courses" },
                    { 3, "BasePermission", "ReceiveNotification", "Receive Notification", "Receive Notification" },
                    { 2, "BasePermission", "EditProfile", "Edit profile", "Edit profile" },
                    { 9, "Course", "UpdateTopic", "Update topic in course", "Update topic" },
                    { 19, "Course", "ViewListOfSurveyResultsInAssignedCourses", "View list of survey results of students in assigned courses", "View list of survey results in assigned courses" },
                    { 18, "Course", "ViewListOfSurveyResultsInAllCourses", "View list of survey results of students in all courses", "View list of survey results in all courses" },
                    { 21, "Course", "ViewSummaryOfSurveyResults", "View summary of survey result of attendees", "View summary of survey result" },
                    { 36, "Course", "ViewOwnMarkReport", "View own mark report", "View own mark report" },
                    { 35, "Course", "ViewStudentMarkReport", "View student mark report", "View student mark report" },
                    { 34, "Course", "PreviewSurvey", "Preview survey", "Preview survey" },
                    { 33, "Course", "DoAndEditSurvey", "Do and edit survey", "Do and edit survey" },
                    { 32, "Course", "DeleteSurvey", "Delete survey", "Delete survey" },
                    { 20, "Course", "ViewDetailOfSurveyResult", "View detail of survey result (responses analysis) of attendees", "View detail of survey result" },
                    { 30, "Course", "AddSurveyFromTemplate", "Add survey from template", "Add survey" },
                    { 31, "Course", "UpdateSurvey", "Update survey", "Update survey" },
                    { 28, "Course", "ReviewYourOwnAttempts", "Review your own attempts", "Review your own attempts" },
                    { 27, "Course", "AttemptAndReattemptQuiz", "Attempt and Re-attempt quiz", "Attempt and Re-attempt quiz" },
                    { 26, "Course", "DeleteQuiz", "Delete quiz in topic", "Delete quiz" },
                    { 25, "Course", "UpdateQuiz", "Update quiz in topic", "Update quiz" },
                    { 24, "Course", "CreateQuiz", "Create quiz in topic", "Create quiz" },
                    { 23, "Course", "ViewDetailOfLearningProcessOfStudent", "View detail of student’s learning process", "View detail of student’s learning process" },
                    { 22, "Course", "ViewSummaryOfLearningProcessOfStudent", "View summary of student’s learning process", "View summary of student’s learning process" },
                    { 29, "Course", "PreviewQuiz", "Preview quiz", "Preview quiz" }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "create_by", "create_time", "description", "is_active", "name", "update_by", "update_time" },
                values: new object[,]
                {
                    { 4, new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2022, 8, 19, 20, 45, 12, 730, DateTimeKind.Unspecified).AddTicks(5495), new TimeSpan(0, 7, 0, 0, 0)), "Users that take the course for teaching by manage resources, activites,...", true, "Teacher", null, null },
                    { 1, new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2022, 8, 19, 20, 45, 12, 728, DateTimeKind.Unspecified).AddTicks(6537), new TimeSpan(0, 7, 0, 0, 0)), "All logged in users.", true, "Authenticated User", null, null },
                    { 2, new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2022, 8, 19, 20, 45, 12, 730, DateTimeKind.Unspecified).AddTicks(5466), new TimeSpan(0, 7, 0, 0, 0)), "System Administrator", true, "Admin", null, null },
                    { 3, new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2022, 8, 19, 20, 45, 12, 730, DateTimeKind.Unspecified).AddTicks(5492), new TimeSpan(0, 7, 0, 0, 0)), "Users that take the course for learning.", true, "Student", null, null },
                    { 5, new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2022, 8, 19, 20, 45, 12, 730, DateTimeKind.Unspecified).AddTicks(5497), new TimeSpan(0, 7, 0, 0, 0)), "Users that manage and monitor for learning.", true, "Manager", null, null }
                });

            migrationBuilder.InsertData(
                table: "permission_role",
                columns: new[] { "id", "permission_id", "role_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 73, 14, 5 },
                    { 72, 10, 5 },
                    { 71, 9, 5 },
                    { 70, 8, 5 },
                    { 69, 6, 5 },
                    { 68, 5, 5 },
                    { 67, 72, 5 },
                    { 66, 70, 5 },
                    { 65, 50, 4 },
                    { 64, 49, 4 },
                    { 63, 48, 4 },
                    { 62, 47, 4 },
                    { 61, 46, 4 },
                    { 60, 45, 4 },
                    { 59, 44, 4 },
                    { 58, 43, 4 },
                    { 57, 58, 4 },
                    { 56, 57, 4 },
                    { 55, 53, 4 },
                    { 54, 52, 4 },
                    { 53, 35, 4 },
                    { 74, 15, 5 },
                    { 52, 29, 4 },
                    { 75, 16, 5 },
                    { 77, 18, 5 },
                    { 98, 58, 5 },
                    { 97, 57, 5 },
                    { 96, 56, 5 },
                    { 95, 55, 5 },
                    { 94, 54, 5 },
                    { 93, 53, 5 },
                    { 92, 51, 5 },
                    { 91, 38, 5 },
                    { 90, 39, 5 },
                    { 89, 42, 5 },
                    { 88, 41, 5 },
                    { 87, 40, 5 },
                    { 86, 35, 5 },
                    { 85, 34, 5 },
                    { 84, 32, 5 },
                    { 83, 31, 5 },
                    { 82, 30, 5 },
                    { 81, 29, 5 },
                    { 80, 23, 5 },
                    { 79, 22, 5 },
                    { 78, 21, 5 },
                    { 76, 17, 5 },
                    { 51, 26, 4 },
                    { 50, 25, 4 },
                    { 49, 24, 4 },
                    { 22, 6, 3 },
                    { 21, 4, 3 },
                    { 20, 71, 3 },
                    { 19, 74, 3 },
                    { 18, 21, 2 },
                    { 17, 18, 2 },
                    { 16, 6, 2 },
                    { 15, 5, 2 },
                    { 14, 69, 2 },
                    { 13, 68, 2 },
                    { 12, 67, 2 },
                    { 11, 66, 2 },
                    { 10, 65, 2 },
                    { 9, 64, 2 },
                    { 8, 63, 2 },
                    { 7, 62, 2 },
                    { 6, 61, 2 },
                    { 5, 70, 2 },
                    { 4, 75, 2 },
                    { 3, 3, 1 },
                    { 2, 2, 1 },
                    { 23, 14, 3 },
                    { 24, 27, 3 },
                    { 25, 28, 3 },
                    { 26, 33, 3 },
                    { 48, 23, 4 },
                    { 47, 22, 4 },
                    { 46, 21, 4 },
                    { 45, 19, 4 },
                    { 44, 17, 4 },
                    { 43, 16, 4 },
                    { 42, 15, 4 },
                    { 41, 14, 4 },
                    { 40, 13, 4 },
                    { 39, 12, 4 },
                    { 99, 59, 5 },
                    { 38, 11, 4 },
                    { 36, 9, 4 },
                    { 35, 8, 4 },
                    { 34, 7, 4 },
                    { 33, 6, 4 },
                    { 32, 4, 4 },
                    { 31, 73, 4 },
                    { 30, 71, 4 },
                    { 29, 20, 3 },
                    { 28, 37, 3 },
                    { 27, 36, 3 },
                    { 37, 10, 4 },
                    { 100, 60, 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 5);
        }
    }
}
