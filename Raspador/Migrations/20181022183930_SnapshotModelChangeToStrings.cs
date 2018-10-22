using Microsoft.EntityFrameworkCore.Migrations;

namespace Raspador.Migrations
{
    public partial class SnapshotModelChangeToStrings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TotalGainPercent",
                table: "Snapshots",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "TotalGain",
                table: "Snapshots",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "NetWorth",
                table: "Snapshots",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "DayGainPercent",
                table: "Snapshots",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "DayGainChange",
                table: "Snapshots",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalGainPercent",
                table: "Snapshots",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TotalGain",
                table: "Snapshots",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "NetWorth",
                table: "Snapshots",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DayGainPercent",
                table: "Snapshots",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DayGainChange",
                table: "Snapshots",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
