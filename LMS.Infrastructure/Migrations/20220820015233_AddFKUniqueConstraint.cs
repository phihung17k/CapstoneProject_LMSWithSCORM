using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddFKUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_survey_detail_survey_question_id",
                table: "user_survey_detail");

            migrationBuilder.DropIndex(
                name: "ix_user_survey_user_id",
                table: "user_survey");

            migrationBuilder.DropIndex(
                name: "ix_user_subject_user_id",
                table: "user_subject");

            migrationBuilder.DropIndex(
                name: "ix_user_quiz_user_id",
                table: "user_quiz");

            migrationBuilder.DropIndex(
                name: "ix_topic_tracking_user_id",
                table: "topic_tracking");

            migrationBuilder.DropIndex(
                name: "ix_topic_scorm_scorm_id",
                table: "topic_scorm");

            migrationBuilder.DropIndex(
                name: "ix_topic_other_learning_resource_other_learning_resource_id",
                table: "topic_other_learning_resource");

            migrationBuilder.DropIndex(
                name: "ix_scorm_core_learner_id",
                table: "scorm_core");

            migrationBuilder.DropIndex(
                name: "ix_role_user_role_id",
                table: "role_user");

            migrationBuilder.DropIndex(
                name: "ix_quiz_question_quiz_id",
                table: "quiz_question");

            migrationBuilder.DropIndex(
                name: "ix_permission_role_role_id",
                table: "permission_role");

            migrationBuilder.DropIndex(
                name: "ix_other_learning_resource_tracking_learner_id",
                table: "other_learning_resource_tracking");

            migrationBuilder.DropIndex(
                name: "ix_notification_recipient_user_id",
                table: "notification_recipient");

            migrationBuilder.AddColumn<Guid>(
                name: "update_by",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "update_time",
                table: "user",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "update_by",
                table: "subject",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "update_time",
                table: "subject",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "create_by",
                table: "section",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "create_time",
                table: "section",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "delete_by",
                table: "section",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "delete_time",
                table: "section",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "update_by",
                table: "section",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "update_time",
                table: "section",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "update_by",
                table: "course",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "update_time",
                table: "course",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "id", "category", "code", "description", "name" },
                values: new object[] { 76, "Subject", "UpdateSubject", "Update subject description", "Update subject" });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 20, 8, 52, 31, 940, DateTimeKind.Unspecified).AddTicks(9368), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 20, 8, 52, 31, 943, DateTimeKind.Unspecified).AddTicks(7557), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 3,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 20, 8, 52, 31, 943, DateTimeKind.Unspecified).AddTicks(7597), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 4,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 20, 8, 52, 31, 943, DateTimeKind.Unspecified).AddTicks(7602), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 5,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 20, 8, 52, 31, 943, DateTimeKind.Unspecified).AddTicks(7605), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "permission_role",
                columns: new[] { "id", "permission_id", "role_id" },
                values: new object[] { 101, 76, 5 });

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_detail_survey_question_id_user_survey_id",
                table: "user_survey_detail",
                columns: new[] { "survey_question_id", "user_survey_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_user_id_survey_id",
                table: "user_survey",
                columns: new[] { "user_id", "survey_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_subject_user_id_subject_id",
                table: "user_subject",
                columns: new[] { "user_id", "subject_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_quiz_user_id_quiz_id",
                table: "user_quiz",
                columns: new[] { "user_id", "quiz_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_topic_tracking_user_id_topic_id",
                table: "topic_tracking",
                columns: new[] { "user_id", "topic_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_topic_scorm_scorm_id_topic_id",
                table: "topic_scorm",
                columns: new[] { "scorm_id", "topic_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_topic_other_learning_resource_other_learning_resource_id_to",
                table: "topic_other_learning_resource",
                columns: new[] { "other_learning_resource_id", "topic_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scorm_core_learner_id_topic_scorm_id",
                table: "scorm_core",
                columns: new[] { "learner_id", "topic_scorm_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_role_user_role_id_user_id",
                table: "role_user",
                columns: new[] { "role_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_quiz_question_quiz_id_question_id",
                table: "quiz_question",
                columns: new[] { "quiz_id", "question_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permission_role_role_id_permission_id",
                table: "permission_role",
                columns: new[] { "role_id", "permission_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_other_learning_resource_tracking_learner_id_topic_other_lea",
                table: "other_learning_resource_tracking",
                columns: new[] { "learner_id", "topic_other_learning_resource_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_notification_recipient_user_id_notification_id",
                table: "notification_recipient",
                columns: new[] { "user_id", "notification_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_survey_detail_survey_question_id_user_survey_id",
                table: "user_survey_detail");

            migrationBuilder.DropIndex(
                name: "ix_user_survey_user_id_survey_id",
                table: "user_survey");

            migrationBuilder.DropIndex(
                name: "ix_user_subject_user_id_subject_id",
                table: "user_subject");

            migrationBuilder.DropIndex(
                name: "ix_user_quiz_user_id_quiz_id",
                table: "user_quiz");

            migrationBuilder.DropIndex(
                name: "ix_topic_tracking_user_id_topic_id",
                table: "topic_tracking");

            migrationBuilder.DropIndex(
                name: "ix_topic_scorm_scorm_id_topic_id",
                table: "topic_scorm");

            migrationBuilder.DropIndex(
                name: "ix_topic_other_learning_resource_other_learning_resource_id_to",
                table: "topic_other_learning_resource");

            migrationBuilder.DropIndex(
                name: "ix_scorm_core_learner_id_topic_scorm_id",
                table: "scorm_core");

            migrationBuilder.DropIndex(
                name: "ix_role_user_role_id_user_id",
                table: "role_user");

            migrationBuilder.DropIndex(
                name: "ix_quiz_question_quiz_id_question_id",
                table: "quiz_question");

            migrationBuilder.DropIndex(
                name: "ix_permission_role_role_id_permission_id",
                table: "permission_role");

            migrationBuilder.DropIndex(
                name: "ix_other_learning_resource_tracking_learner_id_topic_other_lea",
                table: "other_learning_resource_tracking");

            migrationBuilder.DropIndex(
                name: "ix_notification_recipient_user_id_notification_id",
                table: "notification_recipient");

            migrationBuilder.DeleteData(
                table: "permission_role",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "permission",
                keyColumn: "id",
                keyValue: 76);

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "user");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "user");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "subject");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "subject");

            migrationBuilder.DropColumn(
                name: "create_by",
                table: "section");

            migrationBuilder.DropColumn(
                name: "create_time",
                table: "section");

            migrationBuilder.DropColumn(
                name: "delete_by",
                table: "section");

            migrationBuilder.DropColumn(
                name: "delete_time",
                table: "section");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "section");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "section");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "course");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "course");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 19, 20, 45, 12, 728, DateTimeKind.Unspecified).AddTicks(6537), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 19, 20, 45, 12, 730, DateTimeKind.Unspecified).AddTicks(5466), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 3,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 19, 20, 45, 12, 730, DateTimeKind.Unspecified).AddTicks(5492), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 4,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 19, 20, 45, 12, 730, DateTimeKind.Unspecified).AddTicks(5495), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 5,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 19, 20, 45, 12, 730, DateTimeKind.Unspecified).AddTicks(5497), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_detail_survey_question_id",
                table: "user_survey_detail",
                column: "survey_question_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_user_id",
                table: "user_survey",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_subject_user_id",
                table: "user_subject",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_quiz_user_id",
                table: "user_quiz",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_topic_tracking_user_id",
                table: "topic_tracking",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_topic_scorm_scorm_id",
                table: "topic_scorm",
                column: "scorm_id");

            migrationBuilder.CreateIndex(
                name: "ix_topic_other_learning_resource_other_learning_resource_id",
                table: "topic_other_learning_resource",
                column: "other_learning_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_scorm_core_learner_id",
                table: "scorm_core",
                column: "learner_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_user_role_id",
                table: "role_user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_quiz_question_quiz_id",
                table: "quiz_question",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "ix_permission_role_role_id",
                table: "permission_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_other_learning_resource_tracking_learner_id",
                table: "other_learning_resource_tracking",
                column: "learner_id");

            migrationBuilder.CreateIndex(
                name: "ix_notification_recipient_user_id",
                table: "notification_recipient",
                column: "user_id");
        }
    }
}
