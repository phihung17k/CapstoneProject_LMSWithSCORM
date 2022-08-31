using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class RenameOtherLearningResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_other_learning_resources_subject_subject_id",
                table: "other_learning_resources");

            migrationBuilder.DropPrimaryKey(
                name: "pk_other_learning_resources",
                table: "other_learning_resources");

            migrationBuilder.RenameTable(
                name: "other_learning_resources",
                newName: "other_learning_resource");

            migrationBuilder.RenameIndex(
                name: "ix_other_learning_resources_subject_id",
                table: "other_learning_resource",
                newName: "ix_other_learning_resource_subject_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_other_learning_resource",
                table: "other_learning_resource",
                column: "id");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 21, 22, 5, 37, 998, DateTimeKind.Unspecified).AddTicks(4399), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 21, 22, 5, 38, 1, DateTimeKind.Unspecified).AddTicks(7427), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "fk_other_learning_resource_subject_subject_id",
                table: "other_learning_resource",
                column: "subject_id",
                principalTable: "subject",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_other_learning_resource_subject_subject_id",
                table: "other_learning_resource");

            migrationBuilder.DropPrimaryKey(
                name: "pk_other_learning_resource",
                table: "other_learning_resource");

            migrationBuilder.RenameTable(
                name: "other_learning_resource",
                newName: "other_learning_resources");

            migrationBuilder.RenameIndex(
                name: "ix_other_learning_resource_subject_id",
                table: "other_learning_resources",
                newName: "ix_other_learning_resources_subject_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_other_learning_resources",
                table: "other_learning_resources",
                column: "id");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 21, 20, 50, 36, 29, DateTimeKind.Unspecified).AddTicks(971), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 21, 20, 50, 36, 34, DateTimeKind.Unspecified).AddTicks(868), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "fk_other_learning_resources_subject_subject_id",
                table: "other_learning_resources",
                column: "subject_id",
                principalTable: "subject",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
