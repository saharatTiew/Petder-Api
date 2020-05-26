using Microsoft.EntityFrameworkCore.Migrations;

namespace petder.Migrations
{
    public partial class IsProfileOnPetImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_current",
                table: "PetImages");

            migrationBuilder.AddColumn<bool>(
                name: "is_profile",
                table: "PetImages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_profile",
                table: "PetImages");

            migrationBuilder.AddColumn<bool>(
                name: "is_current",
                table: "PetImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
