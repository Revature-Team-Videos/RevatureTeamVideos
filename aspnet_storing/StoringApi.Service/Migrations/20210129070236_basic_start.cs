using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoringApi.Service.Migrations
{
    public partial class basic_start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    EntityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.EntityID);
                });

            migrationBuilder.CreateTable(
                name: "ChatBox",
                columns: table => new
                {
                    EntityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomEntityID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBox", x => x.EntityID);
                    table.ForeignKey(
                        name: "FK_ChatBox_Room_RoomEntityID",
                        column: x => x.RoomEntityID,
                        principalTable: "Room",
                        principalColumn: "EntityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    EntityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomEntityID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.EntityID);
                    table.ForeignKey(
                        name: "FK_User_Room_RoomEntityID",
                        column: x => x.RoomEntityID,
                        principalTable: "Room",
                        principalColumn: "EntityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    EntityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sentence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserEntityID = table.Column<long>(type: "bigint", nullable: true),
                    ChatBoxEntityID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.EntityID);
                    table.ForeignKey(
                        name: "FK_Message_ChatBox_ChatBoxEntityID",
                        column: x => x.ChatBoxEntityID,
                        principalTable: "ChatBox",
                        principalColumn: "EntityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_User_UserEntityID",
                        column: x => x.UserEntityID,
                        principalTable: "User",
                        principalColumn: "EntityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "EntityID", "Email", "RoomEntityID", "Username" },
                values: new object[] { 1L, "test@jim.com", null, "TestJim" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "EntityID", "Email", "RoomEntityID", "Username" },
                values: new object[] { 2L, "test@zach.com", null, "TestZach" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "EntityID", "Email", "RoomEntityID", "Username" },
                values: new object[] { 3L, "test@yichen.com", null, "TestYiChen" });

            migrationBuilder.CreateIndex(
                name: "IX_ChatBox_RoomEntityID",
                table: "ChatBox",
                column: "RoomEntityID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatBoxEntityID",
                table: "Message",
                column: "ChatBoxEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserEntityID",
                table: "Message",
                column: "UserEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoomEntityID",
                table: "User",
                column: "RoomEntityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "ChatBox");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}
