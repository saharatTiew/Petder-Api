using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace petder.Migrations
{
    public partial class Pet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    pet_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_active = table.Column<bool>(nullable: false),
                    user_id = table.Column<long>(nullable: false),
                    breed_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: true),
                    gender = table.Column<string>(maxLength: 10, nullable: true),
                    birth_datetime = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    number_of_like = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.pet_id);
                    table.ForeignKey(
                        name: "FK_Pets_Breeds_breed_id",
                        column: x => x.breed_id,
                        principalTable: "Breeds",
                        principalColumn: "breed_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_breed_id",
                table: "Pets",
                column: "breed_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_user_id",
                table: "Pets",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");
        }
    }
}
