using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class ViewDetailSurveyTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "SurveyTemplate", "DeleteSurveyTemplate", "Delete the survey template", "Delete the survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 34,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateQuestionBank", "Create question bank", "Create question bank" });

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
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "QuestionBank", "DeleteQuestionBank", "Delete question bank", "Delete question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 37,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewQuestionsList", "View list of questions in a question bank", "View list of questions in a question bank" });

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
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Question", "DeleteQuestion", "Delete question", "Delete question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 42,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewSubjectsList", "View list of subjects", "View list of subjects" });

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
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Subject", "DeleteLearningResource", "Delete the learning resource", "Delete the learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 49,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewRolesList", "View list of roles", "View list of roles" });

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
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Role", "DeleteRole", "Delete role", "Delete role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 54,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewUsersList", "View list of users", "View list of users" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 55,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfUser", "View detail of user", "View detail of user" });

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "id", "category", "code", "description", "name" },
                values: new object[] { 56, "Account", "AssignRoleToUser", "Assign role to user", "Assign role to user" });

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
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 29, 13, 0, 33, 761, DateTimeKind.Unspecified).AddTicks(2483), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 29, 13, 0, 33, 764, DateTimeKind.Unspecified).AddTicks(3504), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 10,
                column: "permission_id",
                value: 56);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 30,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateSurveyTemplate", "Create a new survey template", "Create a new survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 31,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateSurveyTemplate", "Update the survey template", "Update the survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 32,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteSurveyTemplate", "Delete the survey template", "Delete the survey template" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 33,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "QuestionBank", "CreateQuestionBank", "Create question bank", "Create question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 34,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateQuestionBank", "Update question bank", "Update question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 35,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteQuestionBank", "Delete question bank", "Delete question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 36,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Question", "ViewQuestionsList", "View list of questions in a question bank", "View list of questions in a question bank" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 37,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfQuestion", "View detail of a question", "View detail of a question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 38,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateQuestion", "Create question", "Create question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 39,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateQuestion", "Update question", "Update question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 40,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteQuestion", "Delete question", "Delete question" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 41,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Subject", "ViewSubjectsList", "View list of subjects", "View list of subjects" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 42,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewAssignedSubjectsList", "View list of assigned subjects", "View list of assigned subjects" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 43,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfSubject", "View detail of subject", "View detail of subject" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 44,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewLearningResourcesList", "View list of learning resources in a subject", "View list of learning resources in a subject" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 45,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewContentOfLearningResourcesInSubject", "View content of learning resource in a subject", "View content of learning resource in a subject" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 46,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AddLearningResource", "Add a new learning resource", "Add a new learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 47,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteLearningResource", "Delete the learning resource", "Delete the learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 48,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Role", "ViewRolesList", "View list of roles", "View list of roles" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 49,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfRole", "View detail of role", "View detail of role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 50,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateRole", "Create role", "Create role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 51,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateRole", "Update role", "Update role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 52,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteRole", "Delete role", "Delete role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 53,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Account", "ViewUsersList", "View list of users", "View list of users" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 54,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfUser", "View detail of user", "View detail of user" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 55,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AssignRoleToUser", "Assign role to user", "Assign role to user" });

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 3,
                column: "permission_id",
                value: 48);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 4,
                column: "permission_id",
                value: 49);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 5,
                column: "permission_id",
                value: 50);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 6,
                column: "permission_id",
                value: 51);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 7,
                column: "permission_id",
                value: 52);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 8,
                column: "permission_id",
                value: 53);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 9,
                column: "permission_id",
                value: 54);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 10,
                column: "permission_id",
                value: 55);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 28, 15, 55, 40, 674, DateTimeKind.Unspecified).AddTicks(5365), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 28, 15, 55, 40, 678, DateTimeKind.Unspecified).AddTicks(7585), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
