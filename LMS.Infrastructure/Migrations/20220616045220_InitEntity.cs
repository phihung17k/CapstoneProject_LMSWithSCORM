using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LMS.Infrastructure.Migrations
{
    public partial class InitEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "permission",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    description = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("pk_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subject",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subject", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sync_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    table_name = table.Column<string>(type: "text", nullable: true),
                    status_code = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(0)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sync_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    is_male = table.Column<bool>(type: "boolean", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permission_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    permission_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission_role", x => x.id);
                    table.ForeignKey(
                        name: "fk_permission_role_permission_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_permission_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    parent_code = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: false),
                    number_of_trainee = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    start_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    end_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course", x => x.id);
                    table.ForeignKey(
                        name: "fk_course_subject_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "access_token",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    expires_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    revoked_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_token", x => x.id);
                    table.ForeignKey(
                        name: "fk_access_token_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_token",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    access_token = table.Column<string>(type: "text", nullable: true),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    expires_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    revoked_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_token", x => x.id);
                    table.ForeignKey(
                        name: "fk_refresh_token_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    delete_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true),
                    delete_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_user_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_user_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "user_subject",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_subject", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_subject_subject_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_subject_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topic",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    course_id = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(0)"),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    create_by = table.Column<Guid>(type: "uuid", nullable: false),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_topic", x => x.id);
                    table.ForeignKey(
                        name: "fk_topic_course_course_id",
                        column: x => x.course_id,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_course",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    course_id = table.Column<int>(type: "integer", nullable: false),
                    action_type = table.Column<string>(type: "text", nullable: false),
                    date_of_join = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_course", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_course_course_course_id",
                        column: x => x.course_id,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_course_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "id", "category", "code", "description", "name" },
                values: new object[,]
                {
                    { 1, "BasePermission", "PerformBaseFeatures", "Perform basic features such as logout, edit avatar", "Base Features" },
                    { 30, "SurveyTemplate", "CreateSurveyTemplate", "Create a new survey template", "Create a new survey template" },
                    { 31, "SurveyTemplate", "UpdateSurveyTemplate", "Update the survey template", "Update the survey template" },
                    { 32, "SurveyTemplate", "DeleteSurveyTemplate", "Delete the survey template", "Delete the survey template" },
                    { 33, "QuestionBank", "CreateQuestionBank", "Create question bank", "Create question bank" },
                    { 34, "QuestionBank", "UpdateQuestionBank", "Update question bank", "Update question bank" },
                    { 35, "QuestionBank", "DeleteQuestionBank", "Delete question bank", "Delete question bank" },
                    { 36, "Question", "ViewQuestionsList", "View list of questions in a question bank", "View list of questions in a question bank" },
                    { 37, "Question", "ViewDetailOfQuestion", "View detail of a question", "View detail of a question" },
                    { 38, "Question", "CreateQuestion", "Create question", "Create question" },
                    { 39, "Question", "UpdateQuestion", "Update question", "Update question" },
                    { 40, "Question", "DeleteQuestion", "Delete question", "Delete question" },
                    { 41, "Subject", "ViewSubjectsList", "View list of subjects", "View list of subjects" },
                    { 42, "Subject", "ViewDetailOfSubject", "View detail of subject", "View detail of subject" },
                    { 43, "Subject", "AddLearningResource", "Add a new learning resource", "Add a new learning resource" },
                    { 44, "Subject", "DeleteLearningResource", "Delete the learning resource", "Delete the learning resource" },
                    { 45, "Role", "ViewRolesList", "View list of roles", "View list of roles" },
                    { 46, "Role", "ViewDetailOfRole", "View detail of role", "View detail of role" },
                    { 47, "Role", "CreateRole", "Create role", "Create role" },
                    { 48, "Role", "UpdateRole", "Update role", "Update role" },
                    { 49, "Role", "DeleteRole", "Delete role", "Delete role" },
                    { 50, "Account", "ViewUsersList", "View list of users", "View list of users" },
                    { 51, "Account", "ViewDetailOfUser", "View detail of user", "View detail of user" },
                    { 52, "Account", "AssignRoleToUser", "Assign role to user", "Assign role to user" },
                    { 29, "SurveyTemplate", "ViewSurveyTemplatesList", "View list of survey templates", "View list of survey templates" },
                    { 28, "Course", "PreviewSurvey", "Preview survey", "Preview survey" },
                    { 27, "Course", "DoAndEditSurvey", "Do and edit survey", "Do and edit survey" },
                    { 26, "Course", "DeleteSurvey", "Delete survey", "Delete survey" },
                    { 2, "Dashboard", "ViewDashboard", "View a summary of information on one display", "View overview of general information" },
                    { 3, "Course", "ViewAssignedCoursesList", "View assigned courses", "View list of assigned courses" },
                    { 4, "Course", "ViewAllCourses", "View all of courses in system", "View all of courses" },
                    { 5, "Course", "ViewDetailOfCourse", "View detail of course include description, outline, topics", "View detail of course" },
                    { 6, "Course", "CreateTopic", "Create topic in course", "Create topic" },
                    { 7, "Course", "UpdateTopic", "Update topic in course", "Update topic" },
                    { 8, "Course", "DeleteTopic", "Delete topic in course", "Delete topic" },
                    { 9, "Course", "AddLearningResource", "Add a new learning resource in topic", "Add a new learning resource" },
                    { 10, "Course", "DeleteLearningResource", "Delete the learning resource in topic", "Delete the learning resource" },
                    { 11, "Course", "ViewContentOfLearningResources", "View content of learning resource", "View content of learning resource" },
                    { 12, "Course", "ViewAttendeesList", "View list attendees in course", "View list attendees in course" },
                    { 13, "Course", "ViewSummaryOfQuizResults", "View summary of quiz results of students", "View summary of quiz results" },
                    { 15, "Course", "ViewSummaryOfSurveyResults", "View summary of survey results of students", "View summary of survey results" },
                    { 16, "Course", "ViewSummaryOfLearningProcessOfStudent", "View summary of student’s learning process", "View summary of student’s learning process" },
                    { 17, "Course", "ViewDetailOfLearningProcessOfStudent", "View detail of student’s learning process", "View detail of student’s learning process" },
                    { 18, "Course", "CreateQuiz", "Create quiz in topic", "Create quiz" },
                    { 19, "Course", "UpdateQuiz", "Update quiz in topic", "Update quiz" },
                    { 20, "Course", "DeleteQuiz", "Delete quiz in topic", "Delete quiz" },
                    { 21, "Course", "AttemptAndReattemptQuiz", "Attempt and Re-attempt quiz", "Attempt and Re-attempt quiz" },
                    { 22, "Course", "ReviewYourOwnAttempts", "Review your own attempts", "Review your own attempts" },
                    { 23, "Course", "PreviewQuiz", "Preview quiz", "Preview quiz" },
                    { 24, "Course", "AddSurveyFromTemplate", "Add survey from template", "Add survey" },
                    { 25, "Course", "UpdateSurvey", "Update survey", "Update survey" },
                    { 14, "Course", "ViewDetailOfQuizResultOfStudent", "View detail of student’s quiz result", "View detail of student’s quiz result" }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "create_by", "create_time", "delete_by", "delete_time", "description", "is_active", "name", "update_by", "update_time" },
                values: new object[,]
                {
                    { 1, new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2022, 6, 16, 11, 52, 20, 39, DateTimeKind.Unspecified).AddTicks(4791), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, true, "Admin", null, null },
                    { 2, new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2022, 6, 16, 11, 52, 20, 41, DateTimeKind.Unspecified).AddTicks(3858), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, true, "Student", null, null }
                });

            migrationBuilder.InsertData(
                table: "permission_role",
                columns: new[] { "id", "permission_id", "role_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 16, 21, 2 },
                    { 15, 11, 2 },
                    { 14, 5, 2 },
                    { 13, 3, 2 },
                    { 12, 2, 2 },
                    { 11, 1, 2 },
                    { 10, 52, 1 },
                    { 9, 51, 1 },
                    { 8, 50, 1 },
                    { 7, 49, 1 },
                    { 6, 48, 1 },
                    { 5, 47, 1 },
                    { 4, 46, 1 },
                    { 3, 45, 1 },
                    { 2, 2, 1 },
                    { 17, 22, 2 },
                    { 18, 27, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_access_token_user_id",
                table: "access_token",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_course_subject_id",
                table: "course",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "ix_permission_role_permission_id",
                table: "permission_role",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_permission_role_role_id",
                table: "permission_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_token_user_id",
                table: "refresh_token",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_user_role_id",
                table: "role_user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_user_user_id",
                table: "role_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_topic_course_id",
                table: "topic",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_course_course_id",
                table: "user_course",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_course_user_id",
                table: "user_course",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_department_department_id",
                table: "user_department",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_department_user_id",
                table: "user_department",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_subject_subject_id",
                table: "user_subject",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_subject_user_id",
                table: "user_subject",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_token");

            migrationBuilder.DropTable(
                name: "permission_role");

            migrationBuilder.DropTable(
                name: "refresh_token");

            migrationBuilder.DropTable(
                name: "role_user");

            migrationBuilder.DropTable(
                name: "sync_log");

            migrationBuilder.DropTable(
                name: "topic");

            migrationBuilder.DropTable(
                name: "user_course");

            migrationBuilder.DropTable(
                name: "user_department");

            migrationBuilder.DropTable(
                name: "user_subject");

            migrationBuilder.DropTable(
                name: "permission");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "subject");
        }
    }
}
