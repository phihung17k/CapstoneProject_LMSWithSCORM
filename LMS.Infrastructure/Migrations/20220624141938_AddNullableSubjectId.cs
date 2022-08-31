using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddNullableSubjectId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_other_learning_resource_subject_subject_id",
                table: "other_learning_resource");

            migrationBuilder.DropForeignKey(
                name: "fk_scorm_subject_subject_id",
                table: "scorm");

            migrationBuilder.AlterColumn<int>(
                name: "subject_id",
                table: "scorm",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "subject_id",
                table: "other_learning_resource",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 24, 21, 19, 37, 767, DateTimeKind.Unspecified).AddTicks(3658), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 24, 21, 19, 37, 772, DateTimeKind.Unspecified).AddTicks(6541), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "fk_other_learning_resource_subject_subject_id",
                table: "other_learning_resource",
                column: "subject_id",
                principalTable: "subject",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_scorm_subject_subject_id",
                table: "scorm",
                column: "subject_id",
                principalTable: "subject",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_other_learning_resource_subject_subject_id",
                table: "other_learning_resource");

            migrationBuilder.DropForeignKey(
                name: "fk_scorm_subject_subject_id",
                table: "scorm");

            migrationBuilder.AlterColumn<int>(
                name: "subject_id",
                table: "scorm",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "subject_id",
                table: "other_learning_resource",
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
                value: new DateTimeOffset(new DateTime(2022, 6, 24, 0, 22, 59, 809, DateTimeKind.Unspecified).AddTicks(4778), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 24, 0, 22, 59, 814, DateTimeKind.Unspecified).AddTicks(322), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "fk_other_learning_resource_subject_subject_id",
                table: "other_learning_resource",
                column: "subject_id",
                principalTable: "subject",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_scorm_subject_subject_id",
                table: "scorm",
                column: "subject_id",
                principalTable: "subject",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
