using Microsoft.EntityFrameworkCore.Migrations;

namespace Raspador.Migrations
{
    public partial class AddUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Stocks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Snapshots",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Snapshots");
        }
    }
}
