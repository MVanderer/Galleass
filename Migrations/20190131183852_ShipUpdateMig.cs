using Microsoft.EntityFrameworkCore.Migrations;

namespace Galleass.Migrations
{
    public partial class ShipUpdateMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VesselImageURL",
                table: "VesselTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VesselImageURL",
                table: "VesselTypes");
        }
    }
}
