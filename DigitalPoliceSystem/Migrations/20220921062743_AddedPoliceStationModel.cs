using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalPoliceSystem.Migrations
{
    public partial class AddedPoliceStationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PoliceStationId",
                table: "Complaints",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PoliceStations",
                columns: table => new
                {
                    PoliceStationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoliceStationName = table.Column<string>(maxLength: 60, nullable: false),
                    PoliceStationPhNo = table.Column<int>(nullable: false),
                    PoliceStationAddress = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceStations", x => x.PoliceStationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_PoliceStationId",
                table: "Complaints",
                column: "PoliceStationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_PoliceStations_PoliceStationId",
                table: "Complaints",
                column: "PoliceStationId",
                principalTable: "PoliceStations",
                principalColumn: "PoliceStationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_PoliceStations_PoliceStationId",
                table: "Complaints");

            migrationBuilder.DropTable(
                name: "PoliceStations");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_PoliceStationId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "PoliceStationId",
                table: "Complaints");
        }
    }
}
