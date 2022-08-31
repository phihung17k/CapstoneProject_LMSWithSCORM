using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class UpdateConstraintEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "user_name",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "eid",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "scorm_name",
                table: "topic_scorm",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "other_learning_resource_name",
                table: "topic_other_learning_resource",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "topic",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "template_question",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "template_option",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "template",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "table_name",
                table: "sync_log",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "survey_question",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "survey_option",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "survey",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "subject",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "subject",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "learner_name",
                table: "scorm_core",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title_from_upload",
                table: "scorm",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title_from_manifest",
                table: "scorm",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "scorm_version",
                table: "scorm",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "path_to_index",
                table: "scorm",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "path_to_folder",
                table: "scorm",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "role",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "role",
                type: "boolean",
                nullable: false,
                defaultValueSql: "false",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValueSql: "false");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "role",
                type: "boolean",
                nullable: false,
                defaultValueSql: "true",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValueSql: "true");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "refresh_token",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "access_token",
                table: "refresh_token",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "quiz",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "question_bank",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "question",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "question",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "question",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "permission",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "permission",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "other_learning_resource",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "path_to_file",
                table: "other_learning_resource",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "option",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "notification",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "message",
                table: "notification",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "start_time",
                table: "course",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "parent_name",
                table: "course",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "parent_code",
                table: "course",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "number_of_trainee",
                table: "course",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "course",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "course",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "course",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "end_time",
                table: "course",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "course",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 5, 20, 31, 11, 9, DateTimeKind.Unspecified).AddTicks(9173), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 5, 20, 31, 11, 12, DateTimeKind.Unspecified).AddTicks(4998), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_detail_id",
                table: "user_survey_detail",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_survey_id",
                table: "user_survey",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_subject_id",
                table: "user_subject",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_quiz_id",
                table: "user_quiz",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_course_id",
                table: "user_course",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_id",
                table: "user",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_topic_tracking_id",
                table: "topic_tracking",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_topic_scorm_id",
                table: "topic_scorm",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_topic_other_learning_resource_id",
                table: "topic_other_learning_resource",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_topic_id",
                table: "topic",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_template_question_id",
                table: "template_question",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_template_option_id",
                table: "template_option",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_template_id",
                table: "template",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_sync_log_id",
                table: "sync_log",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_survey_question_id",
                table: "survey_question",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_survey_option_id",
                table: "survey_option",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_survey_id",
                table: "survey",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_subject_id",
                table: "subject",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scorm_objective_id",
                table: "scorm_objective",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scorm_navigation_scorm_core_id",
                table: "scorm_navigation",
                column: "scorm_core_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scorm_learner_preference_scorm_core_id",
                table: "scorm_learner_preference",
                column: "scorm_core_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scorm_interaction_objective_id",
                table: "scorm_interaction_objective",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scorm_interaction_correct_response_id",
                table: "scorm_interaction_correct_response",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scorm_interaction_id",
                table: "scorm_interaction",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scorm_core_id",
                table: "scorm_core",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scorm_comment_from_lms_id",
                table: "scorm_comment_from_lms",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scorm_comment_from_learner_id",
                table: "scorm_comment_from_learner",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scorm_id",
                table: "scorm",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_role_user_id",
                table: "role_user",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_role_id",
                table: "role",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_refresh_token_id",
                table: "refresh_token",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_quiz_question_id",
                table: "quiz_question",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_quiz_attempt_id",
                table: "quiz_attempt",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_quiz_id",
                table: "quiz",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_question_bank_id",
                table: "question_bank",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_question_id",
                table: "question",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permission_role_id",
                table: "permission_role",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permission_id",
                table: "permission",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_other_learning_resource_tracking_id",
                table: "other_learning_resource_tracking",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_other_learning_resource_id",
                table: "other_learning_resource",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_option_id",
                table: "option",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_notification_recipient_id",
                table: "notification_recipient",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_notification_id",
                table: "notification",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_course_id",
                table: "course",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_survey_detail_id",
                table: "user_survey_detail");

            migrationBuilder.DropIndex(
                name: "ix_user_survey_id",
                table: "user_survey");

            migrationBuilder.DropIndex(
                name: "ix_user_subject_id",
                table: "user_subject");

            migrationBuilder.DropIndex(
                name: "ix_user_quiz_id",
                table: "user_quiz");

            migrationBuilder.DropIndex(
                name: "ix_user_course_id",
                table: "user_course");

            migrationBuilder.DropIndex(
                name: "ix_user_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_topic_tracking_id",
                table: "topic_tracking");

            migrationBuilder.DropIndex(
                name: "ix_topic_scorm_id",
                table: "topic_scorm");

            migrationBuilder.DropIndex(
                name: "ix_topic_other_learning_resource_id",
                table: "topic_other_learning_resource");

            migrationBuilder.DropIndex(
                name: "ix_topic_id",
                table: "topic");

            migrationBuilder.DropIndex(
                name: "ix_template_question_id",
                table: "template_question");

            migrationBuilder.DropIndex(
                name: "ix_template_option_id",
                table: "template_option");

            migrationBuilder.DropIndex(
                name: "ix_template_id",
                table: "template");

            migrationBuilder.DropIndex(
                name: "ix_sync_log_id",
                table: "sync_log");

            migrationBuilder.DropIndex(
                name: "ix_survey_question_id",
                table: "survey_question");

            migrationBuilder.DropIndex(
                name: "ix_survey_option_id",
                table: "survey_option");

            migrationBuilder.DropIndex(
                name: "ix_survey_id",
                table: "survey");

            migrationBuilder.DropIndex(
                name: "ix_subject_id",
                table: "subject");

            migrationBuilder.DropIndex(
                name: "ix_scorm_objective_id",
                table: "scorm_objective");

            migrationBuilder.DropIndex(
                name: "ix_scorm_navigation_scorm_core_id",
                table: "scorm_navigation");

            migrationBuilder.DropIndex(
                name: "ix_scorm_learner_preference_scorm_core_id",
                table: "scorm_learner_preference");

            migrationBuilder.DropIndex(
                name: "ix_scorm_interaction_objective_id",
                table: "scorm_interaction_objective");

            migrationBuilder.DropIndex(
                name: "ix_scorm_interaction_correct_response_id",
                table: "scorm_interaction_correct_response");

            migrationBuilder.DropIndex(
                name: "ix_scorm_interaction_id",
                table: "scorm_interaction");

            migrationBuilder.DropIndex(
                name: "ix_scorm_core_id",
                table: "scorm_core");

            migrationBuilder.DropIndex(
                name: "ix_scorm_comment_from_lms_id",
                table: "scorm_comment_from_lms");

            migrationBuilder.DropIndex(
                name: "ix_scorm_comment_from_learner_id",
                table: "scorm_comment_from_learner");

            migrationBuilder.DropIndex(
                name: "ix_scorm_id",
                table: "scorm");

            migrationBuilder.DropIndex(
                name: "ix_role_user_id",
                table: "role_user");

            migrationBuilder.DropIndex(
                name: "ix_role_id",
                table: "role");

            migrationBuilder.DropIndex(
                name: "ix_refresh_token_id",
                table: "refresh_token");

            migrationBuilder.DropIndex(
                name: "ix_quiz_question_id",
                table: "quiz_question");

            migrationBuilder.DropIndex(
                name: "ix_quiz_attempt_id",
                table: "quiz_attempt");

            migrationBuilder.DropIndex(
                name: "ix_quiz_id",
                table: "quiz");

            migrationBuilder.DropIndex(
                name: "ix_question_bank_id",
                table: "question_bank");

            migrationBuilder.DropIndex(
                name: "ix_question_id",
                table: "question");

            migrationBuilder.DropIndex(
                name: "ix_permission_role_id",
                table: "permission_role");

            migrationBuilder.DropIndex(
                name: "ix_permission_id",
                table: "permission");

            migrationBuilder.DropIndex(
                name: "ix_other_learning_resource_tracking_id",
                table: "other_learning_resource_tracking");

            migrationBuilder.DropIndex(
                name: "ix_other_learning_resource_id",
                table: "other_learning_resource");

            migrationBuilder.DropIndex(
                name: "ix_option_id",
                table: "option");

            migrationBuilder.DropIndex(
                name: "ix_notification_recipient_id",
                table: "notification_recipient");

            migrationBuilder.DropIndex(
                name: "ix_notification_id",
                table: "notification");

            migrationBuilder.DropIndex(
                name: "ix_course_id",
                table: "course");

            migrationBuilder.AlterColumn<string>(
                name: "user_name",
                table: "user",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "eid",
                table: "user",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "scorm_name",
                table: "topic_scorm",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "other_learning_resource_name",
                table: "topic_other_learning_resource",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "topic",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "template_question",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "template_option",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "template",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "table_name",
                table: "sync_log",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "survey_question",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "survey_option",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "survey",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "subject",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "subject",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "learner_name",
                table: "scorm_core",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "title_from_upload",
                table: "scorm",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "title_from_manifest",
                table: "scorm",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "scorm_version",
                table: "scorm",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "path_to_index",
                table: "scorm",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "path_to_folder",
                table: "scorm",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "role",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "role",
                type: "boolean",
                nullable: true,
                defaultValueSql: "false",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValueSql: "false");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "role",
                type: "boolean",
                nullable: true,
                defaultValueSql: "true",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValueSql: "true");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "refresh_token",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "access_token",
                table: "refresh_token",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "quiz",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "question_bank",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "question",
                type: "boolean",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "question",
                type: "boolean",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "question",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "permission",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "permission",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "other_learning_resource",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "path_to_file",
                table: "other_learning_resource",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "option",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "notification",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "message",
                table: "notification",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "start_time",
                table: "course",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "parent_name",
                table: "course",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "parent_code",
                table: "course",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "number_of_trainee",
                table: "course",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "course",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "is_deleted",
                table: "course",
                type: "boolean",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "course",
                type: "boolean",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "end_time",
                table: "course",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "course",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 3, 19, 1, 45, 513, DateTimeKind.Unspecified).AddTicks(698), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 3, 19, 1, 45, 515, DateTimeKind.Unspecified).AddTicks(4793), new TimeSpan(0, 7, 0, 0, 0)));
        }
    }
}
