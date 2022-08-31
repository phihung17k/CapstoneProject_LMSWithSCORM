using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class RemoveIsActiveIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "delete_by",
                table: "template");

            migrationBuilder.DropColumn(
                name: "delete_time",
                table: "template");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "template");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "section");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "scorm");

            migrationBuilder.DropColumn(
                name: "delete_by",
                table: "role");

            migrationBuilder.DropColumn(
                name: "delete_time",
                table: "role");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "role");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "other_learning_resource");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 18, 18, 40, 23, 151, DateTimeKind.Unspecified).AddTicks(1217), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 18, 18, 40, 23, 156, DateTimeKind.Unspecified).AddTicks(3344), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "delete_by",
                table: "template",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "delete_time",
                table: "template",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "template",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "section",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "scorm",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<Guid>(
                name: "delete_by",
                table: "role",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "delete_time",
                table: "role",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "role",
                type: "boolean",
                nullable: false,
                defaultValueSql: "false");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "other_learning_resource",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 18, 16, 22, 16, 97, DateTimeKind.Unspecified).AddTicks(2247), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 18, 16, 22, 16, 99, DateTimeKind.Unspecified).AddTicks(8800), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
