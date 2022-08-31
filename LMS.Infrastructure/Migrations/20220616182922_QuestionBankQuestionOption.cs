using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LMS.Infrastructure.Migrations
{
    public partial class QuestionBankQuestionOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "question_bank",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: true),
                    number_of_questions = table.Column<int>(type: "integer", nullable: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    delete_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true),
                    delete_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_question_bank", x => x.id);
                    table.ForeignKey(
                        name: "fk_question_bank_subject_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    question_bank_id = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    delete_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true),
                    delete_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_question", x => x.id);
                    table.ForeignKey(
                        name: "fk_question_question_bank_question_bank_id",
                        column: x => x.question_bank_id,
                        principalTable: "question_bank",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "option",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: true),
                    is_correct = table.Column<bool>(type: "boolean", nullable: false),
                    question_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_option", x => x.id);
                    table.ForeignKey(
                        name: "fk_option_question_question_id",
                        column: x => x.question_id,
                        principalTable: "question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 17, 1, 29, 21, 184, DateTimeKind.Unspecified).AddTicks(4874), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 17, 1, 29, 21, 189, DateTimeKind.Unspecified).AddTicks(8224), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_option_question_id",
                table: "option",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_question_question_bank_id",
                table: "question",
                column: "question_bank_id");

            migrationBuilder.CreateIndex(
                name: "ix_question_bank_subject_id",
                table: "question_bank",
                column: "subject_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "option");

            migrationBuilder.DropTable(
                name: "question");

            migrationBuilder.DropTable(
                name: "question_bank");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 16, 23, 2, 17, 454, DateTimeKind.Unspecified).AddTicks(3862), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 16, 23, 2, 17, 458, DateTimeKind.Unspecified).AddTicks(3113), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
