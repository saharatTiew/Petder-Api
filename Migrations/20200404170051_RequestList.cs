using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace petder.Migrations
{
    public partial class RequestList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestLists",
                columns: table => new
                {
                    request_list_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_active = table.Column<bool>(nullable: false),
                    pet_id = table.Column<long>(nullable: false),
                    requested_pet_id = table.Column<long>(nullable: false),
                    request_datetime = table.Column<DateTime>(nullable: false),
                    is_accepted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLists", x => x.request_list_id);
                    table.ForeignKey(
                        name: "FK_RequestLists_Pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "Pets",
                        principalColumn: "pet_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestLists_Pets_requested_pet_id",
                        column: x => x.requested_pet_id,
                        principalTable: "Pets",
                        principalColumn: "pet_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestLists_pet_id",
                table: "RequestLists",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLists_requested_pet_id",
                table: "RequestLists",
                column: "requested_pet_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestLists");
        }
    }
}
