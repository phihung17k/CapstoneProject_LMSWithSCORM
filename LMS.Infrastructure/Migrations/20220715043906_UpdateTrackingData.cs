using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LMS.Infrastructure.Migrations
{
    public partial class UpdateTrackingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "delete_by",
                table: "question_bank");

            migrationBuilder.DropColumn(
                name: "delete_time",
                table: "question_bank");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "question_bank");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "question_bank");

            migrationBuilder.DropColumn(
                name: "duration",
                table: "other_learning_resource_tracking");

            migrationBuilder.AddColumn<int>(
                name: "number_of_learning_resources",
                table: "topic",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "number_of_quizzes",
                table: "topic",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "number_of_surveys",
                table: "topic",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "quiz_question",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "number_of_active_questions",
                table: "quiz",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "questions_per_page",
                table: "quiz",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "topic_tracking",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    completed_learning_resourses = table.Column<int>(type: "integer", nullable: false),
                    completed_quizzes = table.Column<int>(type: "integer", nullable: false),
                    completed_surveys = table.Column<int>(type: "integer", nullable: false),
                    topic_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_topic_tracking", x => x.id);
                    table.ForeignKey(
                        name: "fk_topic_tracking_topic_topic_id",
                        column: x => x.topic_id,
                        principalTable: "topic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_topic_tracking_user_user_id",
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
                value: new DateTimeOffset(new DateTime(2022, 7, 15, 11, 39, 5, 809, DateTimeKind.Unspecified).AddTicks(546), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 15, 11, 39, 5, 810, DateTimeKind.Unspecified).AddTicks(9310), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_topic_tracking_topic_id",
                table: "topic_tracking",
                column: "topic_id");

            migrationBuilder.CreateIndex(
                name: "ix_topic_tracking_user_id",
                table: "topic_tracking",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "topic_tracking");

            migrationBuilder.DropColumn(
                name: "number_of_learning_resources",
                table: "topic");

            migrationBuilder.DropColumn(
                name: "number_of_quizzes",
                table: "topic");

            migrationBuilder.DropColumn(
                name: "number_of_surveys",
                table: "topic");

            migrationBuilder.DropColumn(
                name: "order",
                table: "quiz_question");

            migrationBuilder.DropColumn(
                name: "number_of_active_questions",
                table: "quiz");

            migrationBuilder.DropColumn(
                name: "questions_per_page",
                table: "quiz");

            migrationBuilder.AddColumn<Guid>(
                name: "delete_by",
                table: "question_bank",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "delete_time",
                table: "question_bank",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "question_bank",
                type: "boolean",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "question_bank",
                type: "boolean",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "duration",
                table: "other_learning_resource_tracking",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 13, 0, 13, 22, 287, DateTimeKind.Unspecified).AddTicks(5373), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 7, 13, 0, 13, 22, 292, DateTimeKind.Unspecified).AddTicks(740), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
