using Microsoft.EntityFrameworkCore.Migrations;

namespace petder.Migrations
{
    public partial class StatusOnRequestList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_accepted",
                table: "RequestLists");

            migrationBuilder.AddColumn<long>(
                name: "status_id",
                table: "RequestLists",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    status_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.status_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestLists_status_id",
                table: "RequestLists",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLists_Statuses_status_id",
                table: "RequestLists",
                column: "status_id",
                principalTable: "Statuses",
                principalColumn: "status_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLists_Statuses_status_id",
                table: "RequestLists");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_RequestLists_status_id",
                table: "RequestLists");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "RequestLists");

            migrationBuilder.AddColumn<bool>(
                name: "is_accepted",
                table: "RequestLists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
