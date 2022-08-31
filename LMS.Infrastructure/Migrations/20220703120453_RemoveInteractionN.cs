using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class RemoveInteractionN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "interaction_n",
                table: "scorm_interaction_objective");

            migrationBuilder.DropColumn(
                name: "interaction_n",
                table: "scorm_interaction_correct_response");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 3, 19, 4, 52, 741, DateTimeKind.Unspecified).AddTicks(7358), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 3, 19, 4, 52, 745, DateTimeKind.Unspecified).AddTicks(4115), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "interaction_n",
                table: "scorm_interaction_objective",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "interaction_n",
                table: "scorm_interaction_correct_response",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 2, 22, 15, 23, 64, DateTimeKind.Unspecified).AddTicks(6412), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 2, 22, 15, 23, 68, DateTimeKind.Unspecified).AddTicks(4193), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
