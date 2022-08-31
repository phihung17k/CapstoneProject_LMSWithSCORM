using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddRoleDashboardPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "id", "category", "code", "description", "name" },
                values: new object[] { 64, "Dashboard", "ViewRoleAnalytics", "View role analytics", "View role analytics" });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 30, 12, 38, 45, 728, DateTimeKind.Unspecified).AddTicks(7697), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 30, 12, 38, 45, 732, DateTimeKind.Unspecified).AddTicks(2072), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 64);

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
    }
}
