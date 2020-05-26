using Microsoft.EntityFrameworkCore.Migrations;

namespace petder.Migrations
{
    public partial class PetImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetImages",
                columns: table => new
                {
                    image_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_active = table.Column<bool>(nullable: false),
                    pet_id = table.Column<long>(nullable: false),
                    image_url = table.Column<string>(type: "varchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetImages", x => x.image_id);
                    table.ForeignKey(
                        name: "FK_PetImages_Pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "Pets",
                        principalColumn: "pet_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetImages_pet_id",
                table: "PetImages",
                column: "pet_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetImages");
        }
    }
}
