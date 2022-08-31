using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AdjustEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "scorm");

            migrationBuilder.DropColumn(
                name: "has_score",
                table: "quiz");

            migrationBuilder.AddColumn<float>(
                name: "final_score",
                table: "user_course",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<float>(
                name: "score",
                table: "quiz_attempt",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "number_of_correct_answers",
                table: "quiz_attempt",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 20, 17, 12, 44, 81, DateTimeKind.Unspecified).AddTicks(2574), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 20, 17, 12, 44, 83, DateTimeKind.Unspecified).AddTicks(6513), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "final_score",
                table: "user_course");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "scorm",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "score",
                table: "quiz_attempt",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "number_of_correct_answers",
                table: "quiz_attempt",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "has_score",
                table: "quiz",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 20, 16, 49, 58, 273, DateTimeKind.Unspecified).AddTicks(9512), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 20, 16, 49, 58, 279, DateTimeKind.Unspecified).AddTicks(7412), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
