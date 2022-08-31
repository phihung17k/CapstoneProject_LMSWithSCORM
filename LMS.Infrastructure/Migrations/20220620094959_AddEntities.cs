using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "other_learning_resources",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: true),
                    path_to_file = table.Column<string>(type: "text", nullable: true),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_other_learning_resources", x => x.id);
                    table.ForeignKey(
                        name: "fk_other_learning_resources_subject_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quiz",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    number_of_allowed_attempts = table.Column<int>(type: "integer", nullable: false),
                    number_of_questions = table.Column<int>(type: "integer", nullable: false),
                    time_limit = table.Column<TimeSpan>(type: "interval", nullable: false),
                    start_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    end_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    has_start_time = table.Column<bool>(type: "boolean", nullable: false),
                    grading_method = table.Column<string>(type: "text", nullable: false),
                    passed_score = table.Column<float>(type: "real", nullable: false),
                    shuffled_question = table.Column<bool>(type: "boolean", nullable: false),
                    shuffled_option = table.Column<bool>(type: "boolean", nullable: false),
                    credit = table.Column<int>(type: "integer", nullable: false),
                    has_score = table.Column<bool>(type: "boolean", nullable: false),
                    topic_id = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    delete_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true),
                    delete_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quiz", x => x.id);
                    table.ForeignKey(
                        name: "fk_quiz_topic_topic_id",
                        column: x => x.topic_id,
                        principalTable: "topic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scorm",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    title_from_manifest = table.Column<string>(type: "text", nullable: true),
                    title_from_upload = table.Column<string>(type: "text", nullable: true),
                    path_to_index = table.Column<string>(type: "text", nullable: true),
                    path_to_folder = table.Column<string>(type: "text", nullable: true),
                    scorm_version = table.Column<string>(type: "text", nullable: true),
                    subject_id = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scorm", x => x.id);
                    table.ForeignKey(
                        name: "fk_scorm_subject_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "template",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    delete_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true),
                    delete_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_template", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_quiz",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quiz_id = table.Column<int>(type: "integer", nullable: false),
                    final_score = table.Column<float>(type: "real", nullable: false),
                    is_passed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_quiz", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_quiz_quiz_quiz_id",
                        column: x => x.quiz_id,
                        principalTable: "quiz",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_quiz_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "survey",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    point = table.Column<float>(type: "real", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    template_id = table.Column<int>(type: "integer", nullable: false),
                    topic_id = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    delete_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true),
                    delete_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_survey", x => x.id);
                    table.ForeignKey(
                        name: "fk_survey_template_template_id",
                        column: x => x.template_id,
                        principalTable: "template",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_survey_topic_topic_id",
                        column: x => x.topic_id,
                        principalTable: "topic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "template_question",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: true),
                    belong_topic = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    template_id = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    delete_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true),
                    delete_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_template_question", x => x.id);
                    table.ForeignKey(
                        name: "fk_template_question_template_template_id",
                        column: x => x.template_id,
                        principalTable: "template",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quiz_attempt",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    finish_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    estimated_finish_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    number_of_correct_answers = table.Column<int>(type: "integer", nullable: true),
                    score = table.Column<float>(type: "real", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false),
                    answer_history = table.Column<string>(type: "json", nullable: true),
                    user_quiz_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quiz_attempt", x => x.id);
                    table.ForeignKey(
                        name: "fk_quiz_attempt_user_quiz_user_quiz_id",
                        column: x => x.user_quiz_id,
                        principalTable: "user_quiz",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "survey_question",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: true),
                    belong_topic = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    survey_id = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    delete_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true),
                    delete_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_survey_question", x => x.id);
                    table.ForeignKey(
                        name: "fk_survey_question_survey_survey_id",
                        column: x => x.survey_id,
                        principalTable: "survey",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_survey",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    action_type = table.Column<string>(type: "text", nullable: false),
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
                name: "template_option",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: true),
                    point = table.Column<int>(type: "integer", nullable: false),
                    template_question_id = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_template_option", x => x.id);
                    table.ForeignKey(
                        name: "fk_template_option_template_question_template_question_id",
                        column: x => x.template_question_id,
                        principalTable: "template_question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "survey_option",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: true),
                    point = table.Column<int>(type: "integer", nullable: false),
                    survey_question_id = table.Column<int>(type: "integer", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_survey_option", x => x.id);
                    table.ForeignKey(
                        name: "fk_survey_option_survey_question_survey_question_id",
                        column: x => x.survey_question_id,
                        principalTable: "survey_question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 20, 16, 49, 58, 273, DateTimeKind.Unspecified).AddTicks(9512), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 20, 16, 49, 58, 279, DateTimeKind.Unspecified).AddTicks(7412), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_other_learning_resources_subject_id",
                table: "other_learning_resources",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "ix_quiz_topic_id",
                table: "quiz",
                column: "topic_id");

            migrationBuilder.CreateIndex(
                name: "ix_quiz_attempt_user_quiz_id",
                table: "quiz_attempt",
                column: "user_quiz_id");

            migrationBuilder.CreateIndex(
                name: "ix_scorm_subject_id",
                table: "scorm",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "ix_survey_template_id",
                table: "survey",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "ix_survey_topic_id",
                table: "survey",
                column: "topic_id");

            migrationBuilder.CreateIndex(
                name: "ix_survey_option_survey_question_id",
                table: "survey_option",
                column: "survey_question_id");

            migrationBuilder.CreateIndex(
                name: "ix_survey_question_survey_id",
                table: "survey_question",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "ix_template_option_template_question_id",
                table: "template_option",
                column: "template_question_id");

            migrationBuilder.CreateIndex(
                name: "ix_template_question_template_id",
                table: "template_question",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_quiz_quiz_id",
                table: "user_quiz",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_quiz_user_id",
                table: "user_quiz",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_survey_id",
                table: "user_survey",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_user_id",
                table: "user_survey",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "other_learning_resources");

            migrationBuilder.DropTable(
                name: "quiz_attempt");

            migrationBuilder.DropTable(
                name: "scorm");

            migrationBuilder.DropTable(
                name: "survey_option");

            migrationBuilder.DropTable(
                name: "template_option");

            migrationBuilder.DropTable(
                name: "user_survey");

            migrationBuilder.DropTable(
                name: "user_quiz");

            migrationBuilder.DropTable(
                name: "survey_question");

            migrationBuilder.DropTable(
                name: "template_question");

            migrationBuilder.DropTable(
                name: "quiz");

            migrationBuilder.DropTable(
                name: "survey");

            migrationBuilder.DropTable(
                name: "template");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 17, 12, 2, 11, 844, DateTimeKind.Unspecified).AddTicks(5739), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 6, 17, 12, 2, 11, 846, DateTimeKind.Unspecified).AddTicks(9629), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
