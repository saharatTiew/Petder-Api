using Microsoft.EntityFrameworkCore.Migrations;

namespace petder.Migrations
{
    public partial class isCurrentOnPet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_current",
                table: "Pets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_current",
                table: "Pets");
        }
    }
}
