using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace petder.Migrations
{
    public partial class BlockList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockLists",
                columns: table => new
                {
                    block_list_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_active = table.Column<bool>(nullable: false),
                    pet_id = table.Column<long>(nullable: false),
                    blocked_pet_id = table.Column<long>(nullable: false),
                    block_datetime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockLists", x => x.block_list_id);
                    table.ForeignKey(
                        name: "FK_BlockLists_Pets_blocked_pet_id",
                        column: x => x.blocked_pet_id,
                        principalTable: "Pets",
                        principalColumn: "pet_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlockLists_Pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "Pets",
                        principalColumn: "pet_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockLists_blocked_pet_id",
                table: "BlockLists",
                column: "blocked_pet_id");

            migrationBuilder.CreateIndex(
                name: "IX_BlockLists_pet_id",
                table: "BlockLists",
                column: "pet_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockLists");
        }
    }
}
