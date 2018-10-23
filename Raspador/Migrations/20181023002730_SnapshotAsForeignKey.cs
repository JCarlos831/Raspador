using Microsoft.EntityFrameworkCore.Migrations;

namespace Raspador.Migrations
{
    public partial class SnapshotAsForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Snapshots_SnapshotInfoId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Snapshots",
                newName: "SnapshotId");

            migrationBuilder.AlterColumn<int>(
                name: "SnapshotInfoId",
                table: "Stocks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Snapshots_SnapshotInfoId",
                table: "Stocks",
                column: "SnapshotInfoId",
                principalTable: "Snapshots",
                principalColumn: "SnapshotId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Snapshots_SnapshotInfoId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "SnapshotId",
                table: "Snapshots",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "SnapshotInfoId",
                table: "Stocks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Snapshots_SnapshotInfoId",
                table: "Stocks",
                column: "SnapshotInfoId",
                principalTable: "Snapshots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
