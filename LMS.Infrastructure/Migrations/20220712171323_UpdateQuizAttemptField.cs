using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class UpdateQuizAttemptField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_passed",
                table: "user_quiz");

            migrationBuilder.RenameColumn(
                name: "number_of_correct_answers",
                table: "quiz_attempt",
                newName: "number_of_questions");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "user_quiz",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "start_at",
                table: "quiz_attempt",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "estimated_finish_time",
                table: "quiz_attempt",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "background_job_id",
                table: "quiz_attempt",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "mark",
                table: "quiz_attempt",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 13, 0, 13, 22, 287, DateTimeKind.Unspecified).AddTicks(5373), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 13, 0, 13, 22, 292, DateTimeKind.Unspecified).AddTicks(740), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "user_quiz");

            migrationBuilder.DropColumn(
                name: "background_job_id",
                table: "quiz_attempt");

            migrationBuilder.DropColumn(
                name: "mark",
                table: "quiz_attempt");

            migrationBuilder.RenameColumn(
                name: "number_of_questions",
                table: "quiz_attempt",
                newName: "number_of_correct_answers");

            migrationBuilder.AddColumn<bool>(
                name: "is_passed",
                table: "user_quiz",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "start_at",
                table: "quiz_attempt",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "estimated_finish_time",
                table: "quiz_attempt",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

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
    }
}
