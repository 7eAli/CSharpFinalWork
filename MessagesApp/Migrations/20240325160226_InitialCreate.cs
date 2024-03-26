using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagesApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    read = table.Column<bool>(type: "boolean", nullable: false),
                    sender = table.Column<Guid>(type: "uuid", nullable: false),
                    receiver = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("messages_pkey", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_messages_id",
                table: "messages",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages");
        }
    }
}
