using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AdjustPermissionAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "date_of_join",
                table: "user",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP(0)");

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
                values: new object[] { "AddLearningResource", "Add a new learning resource", "Add a new learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 46,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Subject", "DeleteLearningResource", "Delete the learning resource", "Delete the learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 47,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewRolesList", "View list of roles", "View list of roles" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 48,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfRole", "View detail of role", "View detail of role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 49,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateRole", "Create role", "Create role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 50,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateRole", "Update role", "Update role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 51,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Role", "DeleteRole", "Delete role", "Delete role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 52,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewUsersList", "View list of users", "View list of users" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 53,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfUser", "View detail of user", "View detail of user" });

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "id", "category", "code", "description", "name" },
                values: new object[] { 54, "Account", "AssignRoleToUser", "Assign role to user", "Assign role to user" });

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 3,
                column: "permission_id",
                value: 47);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 4,
                column: "permission_id",
                value: 48);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 5,
                column: "permission_id",
                value: 49);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 6,
                column: "permission_id",
                value: 50);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 7,
                column: "permission_id",
                value: 51);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 8,
                column: "permission_id",
                value: 52);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 9,
                column: "permission_id",
                value: 53);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 17, 20, 7, 3, 808, DateTimeKind.Unspecified).AddTicks(3734), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 17, 20, 7, 3, 815, DateTimeKind.Unspecified).AddTicks(4068), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 10,
                column: "permission_id",
                value: 54);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DropColumn(
                name: "date_of_join",
                table: "user");

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 44,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AddLearningResource", "Add a new learning resource", "Add a new learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 45,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteLearningResource", "Delete the learning resource", "Delete the learning resource" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 46,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Role", "ViewRolesList", "View list of roles", "View list of roles" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 47,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfRole", "View detail of role", "View detail of role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 48,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "CreateRole", "Create role", "Create role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 49,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "UpdateRole", "Update role", "Update role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 50,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "DeleteRole", "Delete role", "Delete role" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 51,
                columns: new[] { "category", "code", "description", "name" },
                values: new object[] { "Account", "ViewUsersList", "View list of users", "View list of users" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 52,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "ViewDetailOfUser", "View detail of user", "View detail of user" });

            migrationBuilder.UpdateData(
                table: "permission",
                keyColumn: "id",
                keyValue: 53,
                columns: new[] { "code", "description", "name" },
                values: new object[] { "AssignRoleToUser", "Assign role to user", "Assign role to user" });

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 3,
                column: "permission_id",
                value: 46);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 4,
                column: "permission_id",
                value: 47);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 5,
                column: "permission_id",
                value: 48);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 6,
                column: "permission_id",
                value: 49);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 7,
                column: "permission_id",
                value: 50);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 8,
                column: "permission_id",
                value: 51);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 9,
                column: "permission_id",
                value: 52);

            migrationBuilder.UpdateData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 10,
                column: "permission_id",
                value: 53);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 17, 9, 59, 21, 788, DateTimeKind.Unspecified).AddTicks(1393), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 17, 9, 59, 21, 790, DateTimeKind.Unspecified).AddTicks(7824), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
