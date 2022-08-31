using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddFieldsInSCORMCore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "scorm_objective",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "comments12",
                table: "scorm_core",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "comments_from_lms12",
                table: "scorm_core",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lesson_status12",
                table: "scorm_core",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 11, 12, 10, 37, 673, DateTimeKind.Unspecified).AddTicks(4257), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 11, 12, 10, 37, 675, DateTimeKind.Unspecified).AddTicks(6986), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "scorm_objective");

            migrationBuilder.DropColumn(
                name: "comments12",
                table: "scorm_core");

            migrationBuilder.DropColumn(
                name: "comments_from_lms12",
                table: "scorm_core");

            migrationBuilder.DropColumn(
                name: "lesson_status12",
                table: "scorm_core");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 9, 9, 53, 11, 409, DateTimeKind.Unspecified).AddTicks(4057), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 9, 9, 53, 11, 414, DateTimeKind.Unspecified).AddTicks(1994), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
