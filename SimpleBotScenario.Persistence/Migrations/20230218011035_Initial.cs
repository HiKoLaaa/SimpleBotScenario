using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SimpleBotScenario.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bots",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bots_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    external_id = table.Column<long>(type: "bigint", nullable: false),
                    bot_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_chats_messages",
                        column: x => x.bot_id,
                        principalTable: "bots",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    external_id = table.Column<int>(type: "integer", nullable: false),
                    text = table.Column<string>(type: "text", nullable: true),
                    chat_id = table.Column<long>(type: "bigint", nullable: false),
                    date_time_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_chats_messages",
                        column: x => x.chat_id,
                        principalTable: "chats",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "UX_bots_token",
                table: "bots",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_chats_bot_id",
                table: "chats",
                column: "bot_id");

            migrationBuilder.CreateIndex(
                name: "IX_chats_external_id",
                table: "chats",
                column: "external_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_chat_id",
                table: "messages",
                column: "chat_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_external_id",
                table: "messages",
                column: "external_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.DropTable(
                name: "bots");
        }
    }
}
