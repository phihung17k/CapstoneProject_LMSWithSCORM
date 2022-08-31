using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LMS.Infrastructure.Migrations
{
    public partial class DeleteDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_department");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.AddColumn<string>(
                name: "department",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "duration",
                table: "other_learning_resources",
                type: "interval",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.CreateTable(
                name: "quiz_question",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quiz_id = table.Column<int>(type: "integer", nullable: false),
                    question_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quiz_question", x => x.id);
                    table.ForeignKey(
                        name: "fk_quiz_question_question_question_id",
                        column: x => x.question_id,
                        principalTable: "question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quiz_question_quiz_quiz_id",
                        column: x => x.quiz_id,
                        principalTable: "quiz",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_survey",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    survey_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_survey", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_survey_survey_survey_id",
                        column: x => x.survey_id,
                        principalTable: "survey",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_survey_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_survey_detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_survey_id = table.Column<int>(type: "integer", nullable: false),
                    survey_question_id = table.Column<int>(type: "integer", nullable: false),
                    selected_survey_option_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_survey_detail", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_survey_detail_survey_option_selected_survey_option_id",
                        column: x => x.selected_survey_option_id,
                        principalTable: "survey_option",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_survey_detail_survey_question_survey_question_id",
                        column: x => x.survey_question_id,
                        principalTable: "survey_question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_survey_detail_user_survey_user_survey_id",
                        column: x => x.user_survey_id,
                        principalTable: "user_survey",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_quiz_question_question_id",
                table: "quiz_question",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_quiz_question_quiz_id",
                table: "quiz_question",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_survey_id",
                table: "user_survey",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_user_id",
                table: "user_survey",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_detail_selected_survey_option_id",
                table: "user_survey_detail",
                column: "selected_survey_option_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_detail_survey_question_id",
                table: "user_survey_detail",
                column: "survey_question_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_detail_user_survey_id",
                table: "user_survey_detail",
                column: "user_survey_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quiz_question");

            migrationBuilder.DropTable(
                name: "user_survey_detail");

            migrationBuilder.DropTable(
                name: "user_survey");

            migrationBuilder.DropColumn(
                name: "department",
                table: "user");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "duration",
                table: "other_learning_resources",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    department_code = table.Column<string>(type: "text", nullable: true),
                    department_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_department",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    department_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_department", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_department_department_department_id",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_department_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 21, 9, 48, 0, 95, DateTimeKind.Unspecified).AddTicks(2013), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 21, 9, 48, 0, 97, DateTimeKind.Unspecified).AddTicks(6478), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_user_department_department_id",
                table: "user_department",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_department_user_id",
                table: "user_department",
                column: "user_id");
        }
    }
}
