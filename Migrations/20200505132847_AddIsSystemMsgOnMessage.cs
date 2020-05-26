using Microsoft.EntityFrameworkCore.Migrations;

namespace petder.Migrations
{
    public partial class AddIsSystemMsgOnMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_system_message",
                table: "Messages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_system_message",
                table: "Messages");
        }
    }
}
