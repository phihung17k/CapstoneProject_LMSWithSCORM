using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddSummarySurveyResultPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 17,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewSummaryOfSurveyResults", "View summary of survey result of attendees", "View summary of survey result" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewSummaryOfLearningProcessOfStudent", "View summary of student’s learning process", "View summary of student’s learning process" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfLearningProcessOfStudent", "View detail of student’s learning process", "View detail of student’s learning process" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateQuiz", "Create quiz in topic", "Create quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 21,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateQuiz", "Update quiz in topic", "Update quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 22,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteQuiz", "Delete quiz in topic", "Delete quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 23,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AttemptAndReattemptQuiz", "Attempt and Re-attempt quiz", "Attempt and Re-attempt quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 24,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ReviewYourOwnAttempts", "Review your own attempts", "Review your own attempts" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 25,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "PreviewQuiz", "Preview quiz", "Preview quiz" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 26,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AddSurveyFromTemplate", "Add survey from template", "Add survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 27,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateSurvey", "Update survey", "Update survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 28,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteSurvey", "Delete survey", "Delete survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 29,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DoAndEditSurvey", "Do and edit survey", "Do and edit survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 30,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Course", "PreviewSurvey", "Preview survey", "Preview survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 31,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewSurveyTemplatesList", "View list of survey templates", "View list of survey templates" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 32,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfSurveyTemplate", "View detail of survey template", "View detail of survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 33,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateSurveyTemplate", "Create a new survey template", "Create a new survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 34,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateSurveyTemplate", "Update the survey template", "Update the survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 35,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "SurveyTemplate", "DeleteSurveyTemplate", "Delete the survey template", "Delete the survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 36,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateQuestionBank", "Create question bank", "Create question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 37,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateQuestionBank", "Update question bank", "Update question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 38,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "QuestionBank", "DeleteQuestionBank", "Delete question bank", "Delete question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 39,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewQuestionsList", "View list of questions in a question bank", "View list of questions in a question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 40,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfQuestion", "View detail of a question", "View detail of a question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 41,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateQuestion", "Create question", "Create question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 42,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateQuestion", "Update question", "Update question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 43,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Question", "DeleteQuestion", "Delete question", "Delete question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 44,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewSubjectsList", "View list of subjects", "View list of subjects" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 45,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAssignedSubjectsList", "View list of assigned subjects", "View list of assigned subjects" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 46,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfSubject", "View detail of subject", "View detail of subject" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 47,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewLearningResourcesList", "View list of learning resources in a subject", "View list of learning resources in a subject" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 48,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewContentOfLearningResourcesInSubject", "View content of learning resource in a subject", "View content of learning resource in a subject" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 49,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AddLearningResource", "Add a new learning resource", "Add a new learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 50,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Subject", "DeleteLearningResource", "Delete the learning resource", "Delete the learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 51,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewRolesList", "View list of roles", "View list of roles" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 52,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfRole", "View detail of role", "View detail of role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 53,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateRole", "Create role", "Create role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 54,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateRole", "Update role", "Update role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 55,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Role", "DeleteRole", "Delete role", "Delete role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 56,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewUsersList", "View list of users", "View list of users" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 57,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfUser", "View detail of user", "View detail of user" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 58,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Account", "AssignRoleToUser", "Assign role to user", "Assign role to user" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 59,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAccountAnalytics", "View account analytics", "View account analytics" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 60,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAllCoursesProgressChart", "View progress chart for all courses", "View progress chart for all courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 61,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAssignedCoursesProgressChart", "View progress chart for assigned courses", "View progress chart for assigned courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 62,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAttendeesLearningProgressChartInAllCourses", "View attendees learning progress chart in all courses", "View attendees learning progress chart in all courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 63,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAttendeesLearningProgressChartInAssignedCourses", "View attendees learning progress chart in assigned courses", "View attendees learning progress chart in assigned courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 64,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewOwnLearningProgressChartInAssignedCourses", "View own learning progress chart in assigned courses", "View own learning progress chart in assigned courses" });

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "id", "category", "code", "description", "name" },
                values: new object[] { 65, "Dashboard", "ViewRoleAnalytics", "View role analytics", "View role analytics" });

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 2,
                column: "permission_id",
                value: 59);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 3,
                column: "permission_id",
                value: 51);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 4,
                column: "permission_id",
                value: 52);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 5,
                column: "permission_id",
                value: 53);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 6,
                column: "permission_id",
                value: 54);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 7,
                column: "permission_id",
                value: 55);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 8,
                column: "permission_id",
                value: 56);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 9,
                column: "permission_id",
                value: 57);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 10,
                column: "permission_id",
                value: 58);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 12,
                column: "permission_id",
                value: 64);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 16,
                column: "permission_id",
                value: 23);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 17,
                column: "permission_id",
                value: 24);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 18,
                column: "permission_id",
                value: 29);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 31, 11, 37, 3, 393, DateTimeKind.Unspecified).AddTicks(9704), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 31, 11, 37, 3, 397, DateTimeKind.Unspecified).AddTicks(2729), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 65);

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
                columns: new[] { "code", "description", "name" },
                values: new object[] { "PreviewSurvey", "Preview survey", "Preview survey" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 30,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "SurveyTemplate", "ViewSurveyTemplatesList", "View list of survey templates", "View list of survey templates" });

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
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteSurveyTemplate", "Delete the survey template", "Delete the survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 35,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "QuestionBank", "CreateQuestionBank", "Create question bank", "Create question bank" });

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
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteQuestionBank", "Delete question bank", "Delete question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 38,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Question", "ViewQuestionsList", "View list of questions in a question bank", "View list of questions in a question bank" });

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
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteQuestion", "Delete question", "Delete question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 43,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Subject", "ViewSubjectsList", "View list of subjects", "View list of subjects" });

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
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteLearningResource", "Delete the learning resource", "Delete the learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 50,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Role", "ViewRolesList", "View list of roles", "View list of roles" });

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
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteRole", "Delete role", "Delete role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 55,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Account", "ViewUsersList", "View list of users", "View list of users" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 56,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfUser", "View detail of user", "View detail of user" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 57,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AssignRoleToUser", "Assign role to user", "Assign role to user" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 58,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Dashboard", "ViewAccountAnalytics", "View account analytics", "View account analytics" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 59,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAllCoursesProgressChart", "View progress chart for all courses", "View progress chart for all courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 60,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAssignedCoursesProgressChart", "View progress chart for assigned courses", "View progress chart for assigned courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 61,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAttendeesLearningProgressChartInAllCourses", "View attendees learning progress chart in all courses", "View attendees learning progress chart in all courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 62,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAttendeesLearningProgressChartInAssignedCourses", "View attendees learning progress chart in assigned courses", "View attendees learning progress chart in assigned courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 63,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewOwnLearningProgressChartInAssignedCourses", "View own learning progress chart in assigned courses", "View own learning progress chart in assigned courses" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 64,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewRoleAnalytics", "View role analytics", "View role analytics" });

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 2,
                column: "permission_id",
                value: 58);

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
                keyValue: 10,
                column: "permission_id",
                value: 57);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 12,
                column: "permission_id",
                value: 63);

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
                value: new DateTimeOffset(new DateTime(2022, 7, 30, 12, 38, 45, 728, DateTimeKind.Unspecified).AddTicks(7697), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 30, 12, 38, 45, 732, DateTimeKind.Unspecified).AddTicks(2072), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
