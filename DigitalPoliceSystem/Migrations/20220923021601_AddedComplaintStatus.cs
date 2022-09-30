using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalPoliceSystem.Migrations
{
    public partial class AddedComplaintStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComplaintStateId",
                table: "Complaints",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ComplaintStates",
                columns: table => new
                {
                    ComplaintStateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompliantStateName = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintStates", x => x.ComplaintStateId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ComplaintStateId",
                table: "Complaints",
                column: "ComplaintStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ComplaintStates_ComplaintStateId",
                table: "Complaints",
                column: "ComplaintStateId",
                principalTable: "ComplaintStates",
                principalColumn: "ComplaintStateId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ComplaintStates_ComplaintStateId",
                table: "Complaints");

            migrationBuilder.DropTable(
                name: "ComplaintStates");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ComplaintStateId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ComplaintStateId",
                table: "Complaints");
        }
    }
}
