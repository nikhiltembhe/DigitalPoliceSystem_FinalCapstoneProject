using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalPoliceSystem.Migrations
{
    public partial class UpdatePoliceStationPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PoliceStationPhNo",
                table: "PoliceStations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PoliceStationPhNo",
                table: "PoliceStations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
