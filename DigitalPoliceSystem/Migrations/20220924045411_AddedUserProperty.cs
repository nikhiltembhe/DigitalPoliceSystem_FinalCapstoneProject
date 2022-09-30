using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalPoliceSystem.Migrations
{
    public partial class AddedUserProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPropertyId",
                table: "Complaints",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserProperties",
                columns: table => new
                {
                    UserPropertyId = table.Column<string>(nullable: false),
                    UserPropertyName = table.Column<string>(maxLength: 60, nullable: false),
                    UserPropertyPhoneNumer = table.Column<string>(nullable: false),
                    UserPropertyAddress = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProperties", x => x.UserPropertyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UserPropertyId",
                table: "Complaints",
                column: "UserPropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_UserProperties_UserPropertyId",
                table: "Complaints",
                column: "UserPropertyId",
                principalTable: "UserProperties",
                principalColumn: "UserPropertyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_UserProperties_UserPropertyId",
                table: "Complaints");

            migrationBuilder.DropTable(
                name: "UserProperties");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_UserPropertyId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "UserPropertyId",
                table: "Complaints");
        }
    }
}
