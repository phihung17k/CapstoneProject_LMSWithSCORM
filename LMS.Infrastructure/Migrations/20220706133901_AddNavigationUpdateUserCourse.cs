using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddNavigationUpdateUserCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_scorm_learner_preference",
                table: "scorm_learner_preference");

            migrationBuilder.DropIndex(
                name: "ix_scorm_learner_preference_scorm_core_id",
                table: "scorm_learner_preference");

            migrationBuilder.DropColumn(
                name: "is_passed",
                table: "user_course");

            migrationBuilder.DropColumn(
                name: "id",
                table: "scorm_learner_preference");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "finish_time",
                table: "user_course",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "learning_status",
                table: "user_course",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_scorm_learner_preference",
                table: "scorm_learner_preference",
                column: "scorm_core_id");

            migrationBuilder.CreateTable(
                name: "scorm_navigation",
                columns: table => new
                {
                    scorm_core_id = table.Column<int>(type: "integer", nullable: false),
                    request = table.Column<string>(type: "text", nullable: true),
                    valid_continue = table.Column<string>(type: "text", nullable: true),
                    valid_previous = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scorm_navigation", x => x.scorm_core_id);
                    table.ForeignKey(
                        name: "fk_scorm_navigation_scorm_core_scorm_core_id",
                        column: x => x.scorm_core_id,
                        principalTable: "scorm_core",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 6, 20, 39, 0, 280, DateTimeKind.Unspecified).AddTicks(1382), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 6, 20, 39, 0, 284, DateTimeKind.Unspecified).AddTicks(1331), new TimeSpan(0, 7, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scorm_navigation");

            migrationBuilder.DropPrimaryKey(
                name: "pk_scorm_learner_preference",
                table: "scorm_learner_preference");

            migrationBuilder.DropColumn(
                name: "finish_time",
                table: "user_course");

            migrationBuilder.DropColumn(
                name: "learning_status",
                table: "user_course");

            migrationBuilder.AddColumn<bool>(
                name: "is_passed",
                table: "user_course",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "scorm_learner_preference",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_scorm_learner_preference",
                table: "scorm_learner_preference",
                column: "id");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 3, 19, 4, 52, 741, DateTimeKind.Unspecified).AddTicks(7358), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 3, 19, 4, 52, 745, DateTimeKind.Unspecified).AddTicks(4115), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_scorm_learner_preference_scorm_core_id",
                table: "scorm_learner_preference",
                column: "scorm_core_id",
                unique: true);
        }
    }
}
