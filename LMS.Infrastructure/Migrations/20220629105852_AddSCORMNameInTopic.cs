using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddSCORMNameInTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "scorm_name",
                table: "topic_scorm",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 29, 17, 58, 51, 561, DateTimeKind.Unspecified).AddTicks(6071), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 29, 17, 58, 51, 564, DateTimeKind.Unspecified).AddTicks(1154), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "scorm_name",
                table: "topic_scorm");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 29, 17, 22, 33, 777, DateTimeKind.Unspecified).AddTicks(2295), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 29, 17, 22, 33, 779, DateTimeKind.Unspecified).AddTicks(5293), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
