using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class ChangeStatusAndRemoveDepartmnet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "department",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "scorm_objective",
                newName: "status12");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 3, 19, 1, 45, 513, DateTimeKind.Unspecified).AddTicks(698), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 3, 19, 1, 45, 515, DateTimeKind.Unspecified).AddTicks(4793), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status12",
                table: "scorm_objective",
                newName: "status");

            migrationBuilder.AddColumn<string>(
                name: "department",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 2, 23, 20, 32, 151, DateTimeKind.Unspecified).AddTicks(649), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 2, 23, 20, 32, 154, DateTimeKind.Unspecified).AddTicks(9813), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
