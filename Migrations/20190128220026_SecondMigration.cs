using Microsoft.EntityFrameworkCore.Migrations;

namespace Galleass.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "TradeGoods",
                newName: "GoodWeight");

            migrationBuilder.RenameColumn(
                name: "Volume",
                table: "TradeGoods",
                newName: "GoodVolume");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "TradeGoods",
                newName: "GoodImageURL");

            migrationBuilder.RenameColumn(
                name: "BasePrice",
                table: "TradeGoods",
                newName: "GoodBasePrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GoodWeight",
                table: "TradeGoods",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "GoodVolume",
                table: "TradeGoods",
                newName: "Volume");

            migrationBuilder.RenameColumn(
                name: "GoodImageURL",
                table: "TradeGoods",
                newName: "ImageURL");

            migrationBuilder.RenameColumn(
                name: "GoodBasePrice",
                table: "TradeGoods",
                newName: "BasePrice");
        }
    }
}
