using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Infrastructure.Migrations
{
    public partial class AddMailEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    create_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mail", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mail_recipient",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mail_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mail_recipient", x => x.id);
                    table.ForeignKey(
                        name: "fk_mail_recipient_mail_mail_id",
                        column: x => x.mail_id,
                        principalTable: "mail",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mail_recipient_user_user_id",
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
                value: new DateTimeOffset(new DateTime(2022, 8, 21, 13, 46, 29, 385, DateTimeKind.Unspecified).AddTicks(9253), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 21, 13, 46, 29, 388, DateTimeKind.Unspecified).AddTicks(2427), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 3,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 21, 13, 46, 29, 388, DateTimeKind.Unspecified).AddTicks(2494), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 4,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 21, 13, 46, 29, 388, DateTimeKind.Unspecified).AddTicks(2497), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 5,
                column: "create_time",
                value: new DateTimeOffset(new DateTime(2022, 8, 21, 13, 46, 29, 388, DateTimeKind.Unspecified).AddTicks(2499), new TimeSpan(0, 7, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "ix_mail_id",
                table: "mail",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_mail_recipient_id",
                table: "mail_recipient",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_mail_recipient_mail_id",
                table: "mail_recipient",
                column: "mail_id");

            migrationBuilder.CreateIndex(
                name: "ix_mail_recipient_user_id_mail_id",
                table: "mail_recipient",
                columns: new[] { "user_id", "mail_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mail_recipient");

            migrationBuilder.DropTable(
                name: "mail");

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
        }
    }
}
