using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalPoliceSystem.Migrations
{
    public partial class UpdateUserPropertyEmailIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyUserEmailId",
                table: "UserProperties");

            migrationBuilder.AddColumn<string>(
                name: "UserPropertyEmailId",
                table: "UserProperties",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPropertyEmailId",
                table: "UserProperties");

            migrationBuilder.AddColumn<string>(
                name: "MyUserEmailId",
                table: "UserProperties",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
