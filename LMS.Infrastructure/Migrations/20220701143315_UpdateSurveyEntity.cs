using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class UpdateSurveyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_survey_template_template_id",
                table: "survey");

            migrationBuilder.DropIndex(
                name: "ix_survey_template_id",
                table: "survey");

            migrationBuilder.DropColumn(
                name: "template_id",
                table: "survey");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "end_date",
                table: "survey",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "start_date",
                table: "survey",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 1, 21, 33, 15, 155, DateTimeKind.Unspecified).AddTicks(9232), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 1, 21, 33, 15, 157, DateTimeKind.Unspecified).AddTicks(9198), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_date",
                table: "survey");

            migrationBuilder.DropColumn(
                name: "start_date",
                table: "survey");

            migrationBuilder.AddColumn<int>(
                name: "template_id",
                table: "survey",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 29, 17, 58, 51, 561, DateTimeKind.Unspecified).AddTicks(6071), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 29, 17, 58, 51, 564, DateTimeKind.Unspecified).AddTicks(1154), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_survey_template_id",
                table: "survey",
                column: "template_id");

            migrationBuilder.AddForeignKey(
                name: "fk_survey_template_template_id",
                table: "survey",
                column: "template_id",
                principalTable: "template",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
