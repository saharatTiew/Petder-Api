using Microsoft.EntityFrameworkCore.Migrations;

namespace petder.Migrations
{
    public partial class AddressFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "address_id",
                table: "Users",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Addresses",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_address_id",
                table: "Users",
                column: "address_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_address_id",
                table: "Users",
                column: "address_id",
                principalTable: "Addresses",
                principalColumn: "address_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_address_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_address_id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "address_id",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
