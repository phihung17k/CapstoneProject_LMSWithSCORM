using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class UpdateQuestionBankQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "question_bank",
                type: "boolean",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "question_bank",
                type: "boolean",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "question",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "question",
                type: "boolean",
                nullable: true,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "question_bank");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "question_bank");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "question");

            migrationBuilder.AlterColumn<int>(
                name: "type",
                table: "question",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 17, 1, 29, 21, 184, DateTimeKind.Unspecified).AddTicks(4874), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 17, 1, 29, 21, 189, DateTimeKind.Unspecified).AddTicks(8224), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
