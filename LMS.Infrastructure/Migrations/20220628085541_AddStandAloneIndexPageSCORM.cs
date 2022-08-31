using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddStandAloneIndexPageSCORM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "stand_alone_index_page",
                table: "scorm",
                type: "text",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stand_alone_index_page",
                table: "scorm");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 27, 17, 39, 16, 255, DateTimeKind.Unspecified).AddTicks(2325), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 27, 17, 39, 16, 262, DateTimeKind.Unspecified).AddTicks(3800), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
