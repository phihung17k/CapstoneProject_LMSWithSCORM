using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddNullableEid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "eid",
                table: "user",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "nid",
                table: "scorm_objective",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "n_id",
                table: "scorm_interaction_objective",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "n_id",
                table: "scorm_interaction",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 6, 0, 18, 40, 367, DateTimeKind.Unspecified).AddTicks(9954), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 6, 0, 18, 40, 370, DateTimeKind.Unspecified).AddTicks(3846), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "eid",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nid",
                table: "scorm_objective",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "n_id",
                table: "scorm_interaction_objective",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "n_id",
                table: "scorm_interaction",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 5, 20, 31, 11, 9, DateTimeKind.Unspecified).AddTicks(9173), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 5, 20, 31, 11, 12, DateTimeKind.Unspecified).AddTicks(4998), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
