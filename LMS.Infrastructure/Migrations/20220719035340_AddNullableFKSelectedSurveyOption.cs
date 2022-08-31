using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddNullableFKSelectedSurveyOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_survey_detail_survey_option_selected_survey_option_id",
                table: "user_survey_detail");

            migrationBuilder.AlterColumn<int>(
                name: "selected_survey_option_id",
                table: "user_survey_detail",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 19, 10, 53, 39, 686, DateTimeKind.Unspecified).AddTicks(8101), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 19, 10, 53, 39, 690, DateTimeKind.Unspecified).AddTicks(4624), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "fk_user_survey_detail_survey_option_selected_survey_option_id",
                table: "user_survey_detail",
                column: "selected_survey_option_id",
                principalTable: "survey_option",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_survey_detail_survey_option_selected_survey_option_id",
                table: "user_survey_detail");

            migrationBuilder.AlterColumn<int>(
                name: "selected_survey_option_id",
                table: "user_survey_detail",
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
                value: new DateTimeOffset(new DateTime(2022, 7, 19, 9, 38, 26, 167, DateTimeKind.Unspecified).AddTicks(3912), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 19, 9, 38, 26, 169, DateTimeKind.Unspecified).AddTicks(8593), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "fk_user_survey_detail_survey_option_selected_survey_option_id",
                table: "user_survey_detail",
                column: "selected_survey_option_id",
                principalTable: "survey_option",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
