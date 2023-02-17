using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SimpleBotScenario.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatParticipantId",
                table: "messages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "chat_participant_id",
                table: "messages",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "bot_id",
                table: "chats",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "chat_participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExternalId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_participants_id", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_messages_chat_participant_id",
                table: "messages",
                column: "chat_participant_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_ChatParticipantId",
                table: "messages",
                column: "ChatParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_chat_participants",
                table: "messages",
                column: "chat_participant_id",
                principalTable: "chat_participants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_chat_participants_ChatParticipantId",
                table: "messages",
                column: "ChatParticipantId",
                principalTable: "chat_participants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_chat_participants",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_chat_participants_ChatParticipantId",
                table: "messages");

            migrationBuilder.DropTable(
                name: "chat_participants");

            migrationBuilder.DropIndex(
                name: "IX_messages_chat_participant_id",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_ChatParticipantId",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "ChatParticipantId",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "chat_participant_id",
                table: "messages");

            migrationBuilder.AlterColumn<int>(
                name: "bot_id",
                table: "chats",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
