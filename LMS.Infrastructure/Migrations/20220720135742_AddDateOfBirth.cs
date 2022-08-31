using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddDateOfBirth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "date_of_birth",
                table: "user",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 20, 20, 57, 41, 647, DateTimeKind.Unspecified).AddTicks(2265), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 20, 20, 57, 41, 650, DateTimeKind.Unspecified).AddTicks(8785), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "user");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 20, 20, 8, 17, 951, DateTimeKind.Unspecified).AddTicks(8263), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 20, 20, 8, 17, 954, DateTimeKind.Unspecified).AddTicks(4901), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
