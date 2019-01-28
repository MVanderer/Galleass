using Microsoft.EntityFrameworkCore.Migrations;

namespace Galleass.Migrations
{
    public partial class ThirdMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Ports",
                newName: "PortImageURL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PortImageURL",
                table: "Ports",
                newName: "ImageURL");
        }
    }
}
