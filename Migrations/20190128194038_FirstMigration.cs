using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Galleass.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ports",
                columns: table => new
                {
                    PortId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PortName = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GridSquareId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ports", x => x.PortId);
                });

            migrationBuilder.CreateTable(
                name: "TradeGoods",
                columns: table => new
                {
                    TradeGoodId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GoodName = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    BasePrice = table.Column<float>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Volume = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeGoods", x => x.TradeGoodId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Admin = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "VesselTypes",
                columns: table => new
                {
                    VesselTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeName = table.Column<string>(nullable: true),
                    CargoSpace = table.Column<int>(nullable: false),
                    TopSpeed = table.Column<int>(nullable: false),
                    Oars = table.Column<bool>(nullable: false),
                    MinCrew = table.Column<int>(nullable: false),
                    MaxCrew = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VesselTypes", x => x.VesselTypeId);
                });

            migrationBuilder.CreateTable(
                name: "GridSquares",
                columns: table => new
                {
                    GridSquareId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    xCoord = table.Column<int>(nullable: false),
                    yCoord = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    PortId = table.Column<int>(nullable: false),
                    PortId1 = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GridSquares", x => x.GridSquareId);
                    table.ForeignKey(
                        name: "FK_GridSquares_Ports_PortId1",
                        column: x => x.PortId1,
                        principalTable: "Ports",
                        principalColumn: "PortId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortPrices",
                columns: table => new
                {
                    PortPriceId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuyModifier = table.Column<float>(nullable: false),
                    SellModifier = table.Column<float>(nullable: false),
                    QuantityAvailable = table.Column<int>(nullable: false),
                    PortId = table.Column<int>(nullable: false),
                    TradeGoodId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortPrices", x => x.PortPriceId);
                    table.ForeignKey(
                        name: "FK_PortPrices_Ports_PortId",
                        column: x => x.PortId,
                        principalTable: "Ports",
                        principalColumn: "PortId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortPrices_TradeGoods_TradeGoodId",
                        column: x => x.TradeGoodId,
                        principalTable: "TradeGoods",
                        principalColumn: "TradeGoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Wealth = table.Column<int>(nullable: false),
                    PlayerName = table.Column<string>(nullable: true),
                    ShipName = table.Column<string>(nullable: true),
                    HullCondition = table.Column<int>(nullable: false),
                    SailsCondition = table.Column<int>(nullable: false),
                    RiggingCondition = table.Column<int>(nullable: false),
                    CrewCondition = table.Column<int>(nullable: false),
                    Crew = table.Column<int>(nullable: false),
                    VesselTypeId = table.Column<int>(nullable: false),
                    GridSquareId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_GridSquares_GridSquareId",
                        column: x => x.GridSquareId,
                        principalTable: "GridSquares",
                        principalColumn: "GridSquareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_VesselTypes_VesselTypeId",
                        column: x => x.VesselTypeId,
                        principalTable: "VesselTypes",
                        principalColumn: "VesselTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    CargoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false),
                    TradeGoodId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.CargoId);
                    table.ForeignKey(
                        name: "FK_Cargos_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cargos_TradeGoods_TradeGoodId",
                        column: x => x.TradeGoodId,
                        principalTable: "TradeGoods",
                        principalColumn: "TradeGoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discovereds",
                columns: table => new
                {
                    DiscoverdId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(nullable: false),
                    GridSquareId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discovereds", x => x.DiscoverdId);
                    table.ForeignKey(
                        name: "FK_Discovereds_GridSquares_GridSquareId",
                        column: x => x.GridSquareId,
                        principalTable: "GridSquares",
                        principalColumn: "GridSquareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Discovereds_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_PlayerId",
                table: "Cargos",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_TradeGoodId",
                table: "Cargos",
                column: "TradeGoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Discovereds_GridSquareId",
                table: "Discovereds",
                column: "GridSquareId");

            migrationBuilder.CreateIndex(
                name: "IX_Discovereds_PlayerId",
                table: "Discovereds",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GridSquares_PortId1",
                table: "GridSquares",
                column: "PortId1");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GridSquareId",
                table: "Players",
                column: "GridSquareId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_VesselTypeId",
                table: "Players",
                column: "VesselTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PortPrices_PortId",
                table: "PortPrices",
                column: "PortId");

            migrationBuilder.CreateIndex(
                name: "IX_PortPrices_TradeGoodId",
                table: "PortPrices",
                column: "TradeGoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Discovereds");

            migrationBuilder.DropTable(
                name: "PortPrices");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "TradeGoods");

            migrationBuilder.DropTable(
                name: "GridSquares");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VesselTypes");

            migrationBuilder.DropTable(
                name: "Ports");
        }
    }
}
