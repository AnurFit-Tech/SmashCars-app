using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarGenerations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarBrandId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearFrom = table.Column<int>(type: "int", nullable: true),
                    YearTo = table.Column<int>(type: "int", nullable: true),
                    EngineSizeCC = table.Column<int>(type: "int", nullable: true),
                    FuelType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarGenerations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarGenerations_CarBrands_CarBrandId",
                        column: x => x.CarBrandId,
                        principalTable: "CarBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCompatibilities",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CarGenerationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCompatibilities", x => new { x.ProductId, x.CarGenerationId });
                    table.ForeignKey(
                        name: "FK_ProductCompatibilities_CarGenerations_CarGenerationId",
                        column: x => x.CarGenerationId,
                        principalTable: "CarGenerations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCompatibilities_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarGenerations_CarBrandId",
                table: "CarGenerations",
                column: "CarBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCompatibilities_CarGenerationId",
                table: "ProductCompatibilities",
                column: "CarGenerationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCompatibilities");

            migrationBuilder.DropTable(
                name: "CarGenerations");

            migrationBuilder.DropTable(
                name: "CarBrands");
        }
    }
}
