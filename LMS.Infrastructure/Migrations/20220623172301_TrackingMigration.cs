using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LMS.Infrastructure.Migrations
{
    public partial class TrackingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_by",
                table: "template_question");

            migrationBuilder.DropColumn(
                name: "create_time",
                table: "template_question");

            migrationBuilder.DropColumn(
                name: "delete_by",
                table: "template_question");

            migrationBuilder.DropColumn(
                name: "delete_time",
                table: "template_question");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "template_question");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "template_question");

            migrationBuilder.DropColumn(
                name: "create_by",
                table: "template_option");

            migrationBuilder.DropColumn(
                name: "create_time",
                table: "template_option");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "template_option");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "template_option");

            migrationBuilder.DropColumn(
                name: "create_by",
                table: "survey_question");

            migrationBuilder.DropColumn(
                name: "create_time",
                table: "survey_question");

            migrationBuilder.DropColumn(
                name: "delete_by",
                table: "survey_question");

            migrationBuilder.DropColumn(
                name: "delete_time",
                table: "survey_question");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "survey_question");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "survey_question");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "survey_option");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "survey_option");

            migrationBuilder.DropColumn(
                name: "delete_by",
                table: "role_user");

            migrationBuilder.DropColumn(
                name: "delete_time",
                table: "role_user");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "role_user");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "role_user");

            migrationBuilder.DropColumn(
                name: "delete_by",
                table: "quiz");

            migrationBuilder.DropColumn(
                name: "delete_time",
                table: "quiz");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "submit_time",
                table: "user_survey",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "user_quiz",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "is_passed",
                table: "user_course",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "delete_by",
                table: "scorm",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "delete_time",
                table: "scorm",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "user_quiz_id",
                table: "quiz_attempt",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "delete_by",
                table: "other_learning_resource",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "delete_time",
                table: "other_learning_resource",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "topic_other_learning_resource",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    topic_id = table.Column<int>(type: "integer", nullable: false),
                    other_learning_resource_id = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_topic_other_learning_resource", x => x.id);
                    table.ForeignKey(
                        name: "fk_topic_other_learning_resource_other_learning_resource_other",
                        column: x => x.other_learning_resource_id,
                        principalTable: "other_learning_resource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_topic_other_learning_resource_topic_topic_id",
                        column: x => x.topic_id,
                        principalTable: "topic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topic_scorm",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    topic_id = table.Column<int>(type: "integer", nullable: false),
                    scorm_id = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_topic_scorm", x => x.id);
                    table.ForeignKey(
                        name: "fk_topic_scorm_scorm_scorm_id",
                        column: x => x.scorm_id,
                        principalTable: "scorm",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_topic_scorm_topic_topic_id",
                        column: x => x.topic_id,
                        principalTable: "topic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "other_learning_resource_tracking",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    learner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    topic_other_learning_resource_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_other_learning_resource_tracking", x => x.id);
                    table.ForeignKey(
                        name: "fk_other_learning_resource_tracking_topic_other_learning_resou",
                        column: x => x.topic_other_learning_resource_id,
                        principalTable: "topic_other_learning_resource",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_other_learning_resource_tracking_user_learner_id",
                        column: x => x.learner_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scorm_core",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    learner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    topic_scorm_id = table.Column<int>(type: "integer", nullable: false),
                    learner_name = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    mode = table.Column<string>(type: "text", nullable: true),
                    credit = table.Column<string>(type: "text", nullable: true),
                    completion_status = table.Column<string>(type: "text", nullable: true),
                    entry = table.Column<string>(type: "text", nullable: true),
                    exit = table.Column<string>(type: "text", nullable: true),
                    score_scaled = table.Column<string>(type: "text", nullable: true),
                    progress_measure = table.Column<string>(type: "text", nullable: true),
                    scaled_passing_score = table.Column<string>(type: "text", nullable: true),
                    score_raw = table.Column<string>(type: "text", nullable: true),
                    score_min = table.Column<string>(type: "text", nullable: true),
                    score_max = table.Column<string>(type: "text", nullable: true),
                    total_time = table.Column<string>(type: "text", nullable: true),
                    session_time = table.Column<string>(type: "text", nullable: true),
                    max_time_allowed = table.Column<string>(type: "text", nullable: true),
                    time_limit_action = table.Column<string>(type: "text", nullable: true),
                    success_status = table.Column<string>(type: "text", nullable: true),
                    completion_threshold = table.Column<string>(type: "text", nullable: true),
                    suspend_data = table.Column<string>(type: "text", nullable: true),
                    launch_data = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scorm_core", x => x.id);
                    table.ForeignKey(
                        name: "fk_scorm_core_topic_scorm_topic_scorm_id",
                        column: x => x.topic_scorm_id,
                        principalTable: "topic_scorm",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_scorm_core_user_learner_id",
                        column: x => x.learner_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scorm_comment_from_learner",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    n = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    timestamp = table.Column<string>(type: "text", nullable: true),
                    scorm_core_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scorm_comment_from_learner", x => x.id);
                    table.ForeignKey(
                        name: "fk_scorm_comment_from_learner_scorm_core_scorm_core_id",
                        column: x => x.scorm_core_id,
                        principalTable: "scorm_core",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scorm_comment_from_lms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    n = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    timestamp = table.Column<string>(type: "text", nullable: true),
                    scorm_core_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scorm_comment_from_lms", x => x.id);
                    table.ForeignKey(
                        name: "fk_scorm_comment_from_lms_scorm_core_scorm_core_id",
                        column: x => x.scorm_core_id,
                        principalTable: "scorm_core",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scorm_interaction",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    n = table.Column<int>(type: "integer", nullable: false),
                    n_id = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    timestamp = table.Column<string>(type: "text", nullable: true),
                    weighting = table.Column<string>(type: "text", nullable: true),
                    learner_response = table.Column<string>(type: "text", nullable: true),
                    result = table.Column<string>(type: "text", nullable: true),
                    latency = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    scorm_core_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scorm_interaction", x => x.id);
                    table.ForeignKey(
                        name: "fk_scorm_interaction_scorm_core_scorm_core_id",
                        column: x => x.scorm_core_id,
                        principalTable: "scorm_core",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scorm_learner_preference",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    audio_level = table.Column<string>(type: "text", nullable: true),
                    language = table.Column<string>(type: "text", nullable: true),
                    delivery_speed = table.Column<string>(type: "text", nullable: true),
                    audio_captioning = table.Column<string>(type: "text", nullable: true),
                    scorm_core_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scorm_learner_preference", x => x.id);
                    table.ForeignKey(
                        name: "fk_scorm_learner_preference_scorm_core_scorm_core_id",
                        column: x => x.scorm_core_id,
                        principalTable: "scorm_core",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scorm_objective",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    n = table.Column<int>(type: "integer", nullable: false),
                    nid = table.Column<string>(type: "text", nullable: true),
                    score_scaled = table.Column<string>(type: "text", nullable: true),
                    score_raw = table.Column<string>(type: "text", nullable: true),
                    score_min = table.Column<string>(type: "text", nullable: true),
                    score_max = table.Column<string>(type: "text", nullable: true),
                    success_status = table.Column<string>(type: "text", nullable: true),
                    completion_status = table.Column<string>(type: "text", nullable: true),
                    progress_measure = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    scorm_core_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scorm_objective", x => x.id);
                    table.ForeignKey(
                        name: "fk_scorm_objective_scorm_core_scorm_core_id",
                        column: x => x.scorm_core_id,
                        principalTable: "scorm_core",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scorm_interaction_correct_response",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    n = table.Column<int>(type: "integer", nullable: false),
                    interaction_n = table.Column<int>(type: "integer", nullable: false),
                    pattern = table.Column<string>(type: "text", nullable: true),
                    interaction_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scorm_interaction_correct_response", x => x.id);
                    table.ForeignKey(
                        name: "fk_scorm_interaction_correct_response_scorm_interaction_intera",
                        column: x => x.interaction_id,
                        principalTable: "scorm_interaction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scorm_interaction_objective",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    n = table.Column<int>(type: "integer", nullable: false),
                    interaction_n = table.Column<int>(type: "integer", nullable: false),
                    n_id = table.Column<string>(type: "text", nullable: true),
                    interaction_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scorm_interaction_objective", x => x.id);
                    table.ForeignKey(
                        name: "fk_scorm_interaction_objective_scorm_interaction_interaction_id",
                        column: x => x.interaction_id,
                        principalTable: "scorm_interaction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_other_learning_resource_tracking_learner_id",
                table: "other_learning_resource_tracking",
                column: "learner_id");

            migrationBuilder.CreateIndex(
                name: "ix_other_learning_resource_tracking_topic_other_learning_resou",
                table: "other_learning_resource_tracking",
                column: "topic_other_learning_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_scorm_comment_from_learner_scorm_core_id",
                table: "scorm_comment_from_learner",
                column: "scorm_core_id");

            migrationBuilder.CreateIndex(
                name: "ix_scorm_comment_from_lms_scorm_core_id",
                table: "scorm_comment_from_lms",
                column: "scorm_core_id");

            migrationBuilder.CreateIndex(
                name: "ix_scorm_core_learner_id",
                table: "scorm_core",
                column: "learner_id");

            migrationBuilder.CreateIndex(
                name: "ix_scorm_core_topic_scorm_id",
                table: "scorm_core",
                column: "topic_scorm_id");

            migrationBuilder.CreateIndex(
                name: "ix_scorm_interaction_scorm_core_id",
                table: "scorm_interaction",
                column: "scorm_core_id");

            migrationBuilder.CreateIndex(
                name: "ix_scorm_interaction_correct_response_interaction_id",
                table: "scorm_interaction_correct_response",
                column: "interaction_id");

            migrationBuilder.CreateIndex(
                name: "ix_scorm_interaction_objective_interaction_id",
                table: "scorm_interaction_objective",
                column: "interaction_id");

            migrationBuilder.CreateIndex(
                name: "ix_scorm_learner_preference_scorm_core_id",
                table: "scorm_learner_preference",
                column: "scorm_core_id");

            migrationBuilder.CreateIndex(
                name: "ix_scorm_objective_scorm_core_id",
                table: "scorm_objective",
                column: "scorm_core_id");

            migrationBuilder.CreateIndex(
                name: "ix_topic_other_learning_resource_other_learning_resource_id",
                table: "topic_other_learning_resource",
                column: "other_learning_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_topic_other_learning_resource_topic_id",
                table: "topic_other_learning_resource",
                column: "topic_id");

            migrationBuilder.CreateIndex(
                name: "ix_topic_scorm_scorm_id",
                table: "topic_scorm",
                column: "scorm_id");

            migrationBuilder.CreateIndex(
                name: "ix_topic_scorm_topic_id",
                table: "topic_scorm",
                column: "topic_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "other_learning_resource_tracking");

            migrationBuilder.DropTable(
                name: "scorm_comment_from_learner");

            migrationBuilder.DropTable(
                name: "scorm_comment_from_lms");

            migrationBuilder.DropTable(
                name: "scorm_interaction_correct_response");

            migrationBuilder.DropTable(
                name: "scorm_interaction_objective");

            migrationBuilder.DropTable(
                name: "scorm_learner_preference");

            migrationBuilder.DropTable(
                name: "scorm_objective");

            migrationBuilder.DropTable(
                name: "topic_other_learning_resource");

            migrationBuilder.DropTable(
                name: "scorm_interaction");

            migrationBuilder.DropTable(
                name: "scorm_core");

            migrationBuilder.DropTable(
                name: "topic_scorm");

            migrationBuilder.DropColumn(
                name: "submit_time",
                table: "user_survey");

            migrationBuilder.DropColumn(
                name: "is_passed",
                table: "user_course");

            migrationBuilder.DropColumn(
                name: "delete_by",
                table: "scorm");

            migrationBuilder.DropColumn(
                name: "delete_time",
                table: "scorm");

            migrationBuilder.DropColumn(
                name: "delete_by",
                table: "other_learning_resource");

            migrationBuilder.DropColumn(
                name: "delete_time",
                table: "other_learning_resource");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "user_quiz",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "create_by",
                table: "template_question",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "create_time",
                table: "template_question",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "delete_by",
                table: "template_question",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "delete_time",
                table: "template_question",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "update_by",
                table: "template_question",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "update_time",
                table: "template_question",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "create_by",
                table: "template_option",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "create_time",
                table: "template_option",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "update_by",
                table: "template_option",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "update_time",
                table: "template_option",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "create_by",
                table: "survey_question",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "create_time",
                table: "survey_question",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "delete_by",
                table: "survey_question",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "delete_time",
                table: "survey_question",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "update_by",
                table: "survey_question",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "update_time",
                table: "survey_question",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "update_by",
                table: "survey_option",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "update_time",
                table: "survey_option",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "delete_by",
                table: "role_user",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "delete_time",
                table: "role_user",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "update_by",
                table: "role_user",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "update_time",
                table: "role_user",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "user_quiz_id",
                table: "quiz_attempt",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<Guid>(
                name: "delete_by",
                table: "quiz",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "delete_time",
                table: "quiz",
                type: "timestamp with time zone",
                nullable: true);

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
        }
    }
}
