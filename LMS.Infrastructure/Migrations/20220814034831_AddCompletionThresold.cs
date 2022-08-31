using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddCompletionThresold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "completion_threshold",
                table: "topic_other_learning_resource",
                type: "real",
                nullable: false,
                defaultValue: 0.8f);

            migrationBuilder.AddColumn<Guid>(
                name: "update_by",
                table: "topic_other_learning_resource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "update_time",
                table: "topic_other_learning_resource",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 14, 10, 48, 29, 958, DateTimeKind.Unspecified).AddTicks(554), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 14, 10, 48, 29, 960, DateTimeKind.Unspecified).AddTicks(9739), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "completion_threshold",
                table: "topic_other_learning_resource");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "topic_other_learning_resource");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "topic_other_learning_resource");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 12, 17, 20, 30, 852, DateTimeKind.Unspecified).AddTicks(318), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 12, 17, 20, 30, 854, DateTimeKind.Unspecified).AddTicks(4066), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
