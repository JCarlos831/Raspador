using Microsoft.EntityFrameworkCore.Migrations;

namespace Raspador.Migrations
{
    public partial class SubtractSnapshotId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Snapshots_SnapshotId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_SnapshotId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "SnapshotId",
                table: "Stocks");

            migrationBuilder.AddColumn<int>(
                name: "SnapshotInfoSnapshotId",
                table: "Stocks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_SnapshotInfoSnapshotId",
                table: "Stocks",
                column: "SnapshotInfoSnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Snapshots_SnapshotInfoSnapshotId",
                table: "Stocks",
                column: "SnapshotInfoSnapshotId",
                principalTable: "Snapshots",
                principalColumn: "SnapshotId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Snapshots_SnapshotInfoSnapshotId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_SnapshotInfoSnapshotId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "SnapshotInfoSnapshotId",
                table: "Stocks");

            migrationBuilder.AddColumn<int>(
                name: "SnapshotId",
                table: "Stocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_SnapshotId",
                table: "Stocks",
                column: "SnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Snapshots_SnapshotId",
                table: "Stocks",
                column: "SnapshotId",
                principalTable: "Snapshots",
                principalColumn: "SnapshotId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
