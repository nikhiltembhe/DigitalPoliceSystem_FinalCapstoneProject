using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalPoliceSystem.Migrations
{
    public partial class AddedInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplaintCategories",
                columns: table => new
                {
                    ComplaintCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompliantCategoryName = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintCategories", x => x.ComplaintCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    ComplaintId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintDescription = table.Column<string>(maxLength: 200, nullable: false),
                    IncidentDate = table.Column<DateTime>(nullable: false),
                    ComplaintCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.ComplaintId);
                    table.ForeignKey(
                        name: "FK_Complaints_ComplaintCategories_ComplaintCategoryId",
                        column: x => x.ComplaintCategoryId,
                        principalTable: "ComplaintCategories",
                        principalColumn: "ComplaintCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ComplaintCategoryId",
                table: "Complaints",
                column: "ComplaintCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "ComplaintCategories");
        }
    }
}
