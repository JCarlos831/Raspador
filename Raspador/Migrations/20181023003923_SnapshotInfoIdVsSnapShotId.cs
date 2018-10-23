using Microsoft.EntityFrameworkCore.Migrations;

namespace Raspador.Migrations
{
    public partial class SnapshotInfoIdVsSnapShotId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Snapshots_SnapshotInfoId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "SnapshotInfoId",
                table: "Stocks",
                newName: "SnapshotId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_SnapshotInfoId",
                table: "Stocks",
                newName: "IX_Stocks_SnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Snapshots_SnapshotId",
                table: "Stocks",
                column: "SnapshotId",
                principalTable: "Snapshots",
                principalColumn: "SnapshotId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Snapshots_SnapshotId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "SnapshotId",
                table: "Stocks",
                newName: "SnapshotInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_SnapshotId",
                table: "Stocks",
                newName: "IX_Stocks_SnapshotInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Snapshots_SnapshotInfoId",
                table: "Stocks",
                column: "SnapshotInfoId",
                principalTable: "Snapshots",
                principalColumn: "SnapshotId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
