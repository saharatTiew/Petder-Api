using Microsoft.EntityFrameworkCore.Migrations;

namespace petder.Migrations
{
    public partial class IsCurrentOnPetImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_current",
                table: "PetImages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_current",
                table: "PetImages");
        }
    }
}
