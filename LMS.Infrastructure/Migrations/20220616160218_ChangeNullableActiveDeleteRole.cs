using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class ChangeNullableActiveDeleteRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "role",
                type: "boolean",
                nullable: true,
                defaultValueSql: "false",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "role",
                type: "boolean",
                nullable: true,
                defaultValueSql: "true",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_time", "is_active" },
                values: new object[] { new DateTimeOffset(new DateTime(2022, 6, 16, 23, 2, 17, 454, DateTimeKind.Unspecified).AddTicks(3862), new TimeSpan(0, 7, 0, 0, 0)), true });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_time", "is_active" },
                values: new object[] { new DateTimeOffset(new DateTime(2022, 6, 16, 23, 2, 17, 458, DateTimeKind.Unspecified).AddTicks(3113), new TimeSpan(0, 7, 0, 0, 0)), true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "role",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValueSql: "false");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "role",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValueSql: "true");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_time", "is_active" },
                values: new object[] { new DateTimeOffset(new DateTime(2022, 6, 16, 22, 14, 24, 274, DateTimeKind.Unspecified).AddTicks(9244), new TimeSpan(0, 7, 0, 0, 0)), false });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_time", "is_active" },
                values: new object[] { new DateTimeOffset(new DateTime(2022, 6, 16, 22, 14, 24, 279, DateTimeKind.Unspecified).AddTicks(6357), new TimeSpan(0, 7, 0, 0, 0)), false });
        }
    }
}
