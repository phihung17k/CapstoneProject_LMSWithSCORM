using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AdjustRelationPreferenceAndCore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_scorm_learner_preference_scorm_core_id",
                table: "scorm_learner_preference");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 2, 22, 15, 23, 64, DateTimeKind.Unspecified).AddTicks(6412), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 2, 22, 15, 23, 68, DateTimeKind.Unspecified).AddTicks(4193), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_scorm_learner_preference_scorm_core_id",
                table: "scorm_learner_preference",
                column: "scorm_core_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_scorm_learner_preference_scorm_core_id",
                table: "scorm_learner_preference");

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

            migrationBuilder.CreateIndex(
                name: "ix_scorm_learner_preference_scorm_core_id",
                table: "scorm_learner_preference",
                column: "scorm_core_id");
        }
    }
}
