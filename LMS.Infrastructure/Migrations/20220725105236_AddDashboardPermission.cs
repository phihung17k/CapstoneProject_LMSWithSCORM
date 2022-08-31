using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddDashboardPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date_of_join",
                table: "user_course");

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Course", "ViewAssignedCoursesList", "View assigned courses", "View list of assigned courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAllCourses", "View all of courses in system", "View all of courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfCourse", "View detail of course include description, outline, topics", "View detail of course" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateTopic", "Create topic in course", "Create topic" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateTopic", "Update topic in course", "Update topic" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteTopic", "Delete topic in course", "Delete topic" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AddLearningResource", "Add a new learning resource in topic", "Add a new learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteLearningResource", "Delete the learning resource in topic", "Delete the learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewContentOfLearningResources", "View content of learning resource", "View content of learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 11,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAttendeesList", "View list attendees in course", "View list attendees in course" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewSummaryOfQuizResults", "View summary of quiz results of students", "View summary of quiz results" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfQuizResultOfStudent", "View detail of student’s quiz result", "View detail of student’s quiz result" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewListOfSurveyResultsInAllCourses", "View list of survey results of students in all courses", "View list of survey results in all courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewListOfSurveyResultsInAssignedCourses", "View list of survey results of students in assigned courses", "View list of survey results in assigned courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 16,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfSurveyResult", "View detail of survey result (responses analysis) of attendees", "View detail of survey result" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 17,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewSummaryOfLearningProcessOfStudent", "View summary of student’s learning process", "View summary of student’s learning process" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfLearningProcessOfStudent", "View detail of student’s learning process", "View detail of student’s learning process" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateQuiz", "Create quiz in topic", "Create quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateQuiz", "Update quiz in topic", "Update quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 21,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteQuiz", "Delete quiz in topic", "Delete quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 22,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AttemptAndReattemptQuiz", "Attempt and Re-attempt quiz", "Attempt and Re-attempt quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 23,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ReviewYourOwnAttempts", "Review your own attempts", "Review your own attempts" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 24,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "PreviewQuiz", "Preview quiz", "Preview quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 25,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AddSurveyFromTemplate", "Add survey from template", "Add survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 26,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateSurvey", "Update survey", "Update survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 27,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteSurvey", "Delete survey", "Delete survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 28,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DoAndEditSurvey", "Do and edit survey", "Do and edit survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 29,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Course", "PreviewSurvey", "Preview survey", "Preview survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 30,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewSurveyTemplatesList", "View list of survey templates", "View list of survey templates" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 31,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfSurveyTemplate", "View detail of survey template", "View detail of survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 32,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateSurveyTemplate", "Create a new survey template", "Create a new survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 33,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateSurveyTemplate", "Update the survey template", "Update the survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 34,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "SurveyTemplate", "DeleteSurveyTemplate", "Delete the survey template", "Delete the survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 35,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateQuestionBank", "Create question bank", "Create question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 36,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateQuestionBank", "Update question bank", "Update question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 37,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "QuestionBank", "DeleteQuestionBank", "Delete question bank", "Delete question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 38,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewQuestionsList", "View list of questions in a question bank", "View list of questions in a question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 39,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfQuestion", "View detail of a question", "View detail of a question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 40,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateQuestion", "Create question", "Create question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 41,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateQuestion", "Update question", "Update question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 42,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Question", "DeleteQuestion", "Delete question", "Delete question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 43,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewSubjectsList", "View list of subjects", "View list of subjects" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 44,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAssignedSubjectsList", "View list of assigned subjects", "View list of assigned subjects" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 45,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfSubject", "View detail of subject", "View detail of subject" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 46,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewLearningResourcesList", "View list of learning resources in a subject", "View list of learning resources in a subject" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 47,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewContentOfLearningResourcesInSubject", "View content of learning resource in a subject", "View content of learning resource in a subject" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 48,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AddLearningResource", "Add a new learning resource", "Add a new learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 49,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Subject", "DeleteLearningResource", "Delete the learning resource", "Delete the learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 50,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewRolesList", "View list of roles", "View list of roles" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 51,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfRole", "View detail of role", "View detail of role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 52,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateRole", "Create role", "Create role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 53,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateRole", "Update role", "Update role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 54,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Role", "DeleteRole", "Delete role", "Delete role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 55,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewUsersList", "View list of users", "View list of users" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 56,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfUser", "View detail of user", "View detail of user" });

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "id", "category", "code", "description", "name" },
                values: new object[,]
                {
                    { 57, "Account", "AssignRoleToUser", "Assign role to user", "Assign role to user" },
                    { 63, "Dashboard", "ViewOwnLearningProgressChartInAssignedCourses", "View own learning progress chart in assigned courses", "View own learning progress chart in assigned courses" },
                    { 62, "Dashboard", "ViewAttendeesLearningProgressChartInAssignedCourses", "View attendees learning progress chart in assigned courses", "View attendees learning progress chart in assigned courses" },
                    { 61, "Dashboard", "ViewAttendeesLearningProgressChartInAllCourses", "View attendees learning progress chart in all courses", "View attendees learning progress chart in all courses" },
                    { 60, "Dashboard", "ViewAssignedCoursesProgressChart", "View progress chart for assigned courses", "View progress chart for assigned courses" },
                    { 59, "Dashboard", "ViewAllCoursesProgressChart", "View progress chart for all courses", "View progress chart for all courses" },
                    { 58, "Dashboard", "ViewAccountAnalytics", "View account analytics", "View account analytics" }
                });

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 3,
                column: "permission_id",
                value: 50);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 4,
                column: "permission_id",
                value: 51);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 5,
                column: "permission_id",
                value: 52);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 6,
                column: "permission_id",
                value: 53);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 7,
                column: "permission_id",
                value: 54);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 8,
                column: "permission_id",
                value: 55);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 9,
                column: "permission_id",
                value: 56);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 13,
                column: "permission_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 14,
                column: "permission_id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 15,
                column: "permission_id",
                value: 10);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 16,
                column: "permission_id",
                value: 22);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 17,
                column: "permission_id",
                value: 23);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 18,
                column: "permission_id",
                value: 28);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 25, 17, 52, 34, 124, DateTimeKind.Unspecified).AddTicks(7010), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 25, 17, 52, 34, 131, DateTimeKind.Unspecified).AddTicks(5393), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 2,
                column: "permission_id",
                value: 58);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 10,
                column: "permission_id",
                value: 57);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 12,
                column: "permission_id",
                value: 63);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "date_of_join",
                table: "user_course",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Dashboard", "ViewDashboard", "View a summary of information on one display", "View overview of general information" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAssignedCoursesList", "View assigned courses", "View list of assigned courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAllCourses", "View all of courses in system", "View all of courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfCourse", "View detail of course include description, outline, topics", "View detail of course" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateTopic", "Create topic in course", "Create topic" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateTopic", "Update topic in course", "Update topic" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteTopic", "Delete topic in course", "Delete topic" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AddLearningResource", "Add a new learning resource in topic", "Add a new learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteLearningResource", "Delete the learning resource in topic", "Delete the learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 11,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewContentOfLearningResources", "View content of learning resource", "View content of learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAttendeesList", "View list attendees in course", "View list attendees in course" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewSummaryOfQuizResults", "View summary of quiz results of students", "View summary of quiz results" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfQuizResultOfStudent", "View detail of student’s quiz result", "View detail of student’s quiz result" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewSummaryOfSurveyResults", "View summary of survey results of students", "View summary of survey results" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 16,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewSummaryOfLearningProcessOfStudent", "View summary of student’s learning process", "View summary of student’s learning process" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 17,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfLearningProcessOfStudent", "View detail of student’s learning process", "View detail of student’s learning process" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateQuiz", "Create quiz in topic", "Create quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateQuiz", "Update quiz in topic", "Update quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteQuiz", "Delete quiz in topic", "Delete quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 21,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AttemptAndReattemptQuiz", "Attempt and Re-attempt quiz", "Attempt and Re-attempt quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 22,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ReviewYourOwnAttempts", "Review your own attempts", "Review your own attempts" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 23,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "PreviewQuiz", "Preview quiz", "Preview quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 24,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AddSurveyFromTemplate", "Add survey from template", "Add survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 25,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateSurvey", "Update survey", "Update survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 26,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteSurvey", "Delete survey", "Delete survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 27,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DoAndEditSurvey", "Do and edit survey", "Do and edit survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 28,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "PreviewSurvey", "Preview survey", "Preview survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 29,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "SurveyTemplate", "ViewSurveyTemplatesList", "View list of survey templates", "View list of survey templates" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 30,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfSurveyTemplate", "View detail of survey template", "View detail of survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 31,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateSurveyTemplate", "Create a new survey template", "Create a new survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 32,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateSurveyTemplate", "Update the survey template", "Update the survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 33,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteSurveyTemplate", "Delete the survey template", "Delete the survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 34,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "QuestionBank", "CreateQuestionBank", "Create question bank", "Create question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 35,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateQuestionBank", "Update question bank", "Update question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 36,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteQuestionBank", "Delete question bank", "Delete question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 37,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Question", "ViewQuestionsList", "View list of questions in a question bank", "View list of questions in a question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 38,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfQuestion", "View detail of a question", "View detail of a question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 39,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateQuestion", "Create question", "Create question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 40,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateQuestion", "Update question", "Update question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 41,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteQuestion", "Delete question", "Delete question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 42,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Subject", "ViewSubjectsList", "View list of subjects", "View list of subjects" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 43,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAssignedSubjectsList", "View list of assigned subjects", "View list of assigned subjects" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 44,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfSubject", "View detail of subject", "View detail of subject" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 45,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewLearningResourcesList", "View list of learning resources in a subject", "View list of learning resources in a subject" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 46,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewContentOfLearningResourcesInSubject", "View content of learning resource in a subject", "View content of learning resource in a subject" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 47,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AddLearningResource", "Add a new learning resource", "Add a new learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 48,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteLearningResource", "Delete the learning resource", "Delete the learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 49,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Role", "ViewRolesList", "View list of roles", "View list of roles" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 50,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfRole", "View detail of role", "View detail of role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 51,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateRole", "Create role", "Create role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 52,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateRole", "Update role", "Update role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 53,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteRole", "Delete role", "Delete role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 54,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Account", "ViewUsersList", "View list of users", "View list of users" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 55,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfUser", "View detail of user", "View detail of user" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 56,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AssignRoleToUser", "Assign role to user", "Assign role to user" });

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 2,
                column: "permission_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 3,
                column: "permission_id",
                value: 49);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 4,
                column: "permission_id",
                value: 50);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 5,
                column: "permission_id",
                value: 51);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 6,
                column: "permission_id",
                value: 52);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 7,
                column: "permission_id",
                value: 53);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 8,
                column: "permission_id",
                value: 54);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 9,
                column: "permission_id",
                value: 55);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 10,
                column: "permission_id",
                value: 56);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 12,
                column: "permission_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 13,
                column: "permission_id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 14,
                column: "permission_id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 15,
                column: "permission_id",
                value: 11);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 16,
                column: "permission_id",
                value: 21);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 17,
                column: "permission_id",
                value: 22);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 18,
                column: "permission_id",
                value: 27);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 22, 47, 50, 836, DateTimeKind.Unspecified).AddTicks(6634), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 22, 47, 50, 840, DateTimeKind.Unspecified).AddTicks(2236), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
