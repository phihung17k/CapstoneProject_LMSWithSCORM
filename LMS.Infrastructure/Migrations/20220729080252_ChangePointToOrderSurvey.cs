using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class ChangePointToOrderSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "point",
                table: "template_option",
                newName: "order");

            migrationBuilder.RenameColumn(
                name: "point",
                table: "survey_option",
                newName: "order");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 29, 15, 2, 51, 278, DateTimeKind.Unspecified).AddTicks(7288), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 29, 15, 2, 51, 284, DateTimeKind.Unspecified).AddTicks(2694), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "order",
                table: "template_option",
                newName: "point");

            migrationBuilder.RenameColumn(
                name: "order",
                table: "survey_option",
                newName: "point");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 28, 15, 11, 55, 855, DateTimeKind.Unspecified).AddTicks(2687), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 28, 15, 11, 55, 857, DateTimeKind.Unspecified).AddTicks(6203), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
