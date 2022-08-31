using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_other_learning_resource_subject_subject_id",
                table: "other_learning_resource");

            migrationBuilder.DropForeignKey(
                name: "fk_scorm_subject_subject_id",
                table: "scorm");

            migrationBuilder.RenameColumn(
                name: "subject_id",
                table: "scorm",
                newName: "section_id");

            migrationBuilder.RenameIndex(
                name: "ix_scorm_subject_id",
                table: "scorm",
                newName: "ix_scorm_section_id");

            migrationBuilder.RenameColumn(
                name: "subject_id",
                table: "other_learning_resource",
                newName: "section_id");

            migrationBuilder.RenameIndex(
                name: "ix_other_learning_resource_subject_id",
                table: "other_learning_resource",
                newName: "ix_other_learning_resource_section_id");

            migrationBuilder.CreateTable(
                name: "section",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_section", x => x.id);
                    table.ForeignKey(
                        name: "fk_section_subject_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_section_id",
                table: "section",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_section_subject_id",
                table: "section",
                column: "subject_id");

            migrationBuilder.AddForeignKey(
                name: "fk_other_learning_resource_section_section_id",
                table: "other_learning_resource",
                column: "section_id",
                principalTable: "section",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_scorm_section_section_id",
                table: "scorm",
                column: "section_id",
                principalTable: "section",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_other_learning_resource_section_section_id",
                table: "other_learning_resource");

            migrationBuilder.DropForeignKey(
                name: "fk_scorm_section_section_id",
                table: "scorm");

            migrationBuilder.DropTable(
                name: "section");

            migrationBuilder.RenameColumn(
                name: "section_id",
                table: "scorm",
                newName: "subject_id");

            migrationBuilder.RenameIndex(
                name: "ix_scorm_section_id",
                table: "scorm",
                newName: "ix_scorm_subject_id");

            migrationBuilder.RenameColumn(
                name: "section_id",
                table: "other_learning_resource",
                newName: "subject_id");

            migrationBuilder.RenameIndex(
                name: "ix_other_learning_resource_section_id",
                table: "other_learning_resource",
                newName: "ix_other_learning_resource_subject_id");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 7, 13, 53, 47, 845, DateTimeKind.Unspecified).AddTicks(444), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 7, 13, 53, 47, 847, DateTimeKind.Unspecified).AddTicks(5054), new TimeSpan(0, 7, 0, 0, 0)));

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
    }
}
