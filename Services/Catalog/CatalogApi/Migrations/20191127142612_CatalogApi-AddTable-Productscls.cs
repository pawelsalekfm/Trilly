using Microsoft.EntityFrameworkCore.Migrations;

namespace CatalogApi.Migrations
{
    public partial class CatalogApiAddTableProductscls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogApi_Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Slug = table.Column<string>(nullable: false),
                    MarketingDescriptionSmall = table.Column<string>(nullable: true, defaultValue: ""),
                    MarketingDescriptionHTML = table.Column<string>(nullable: true, defaultValue: ""),
                    Novelty = table.Column<bool>(nullable: false, defaultValue: false),
                    EanCode = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false, defaultValue: 0.0),
                    PricePromotion = table.Column<double>(nullable: false, defaultValue: 0.0),
                    StockQuantity = table.Column<double>(nullable: false, defaultValue: 0.0),
                    Imported = table.Column<bool>(nullable: false, defaultValue: false),
                    AvailableInShop = table.Column<bool>(nullable: false, defaultValue: true),
                    ImageUrl = table.Column<string>(nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogApi_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogApi_ProductAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogApi_ProductAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogApi_ProductAttribute_CatalogApi_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "CatalogApi_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogApi_ProductCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogApi_ProductCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogApi_ProductCategory_CatalogApi_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "CatalogApi_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogApi_ProductImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogApi_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogApi_ProductImage_CatalogApi_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "CatalogApi_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogApi_ProductAttribute_ProductId",
                table: "CatalogApi_ProductAttribute",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogApi_ProductCategory_ProductId",
                table: "CatalogApi_ProductCategory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogApi_ProductImage_ProductId",
                table: "CatalogApi_ProductImage",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogApi_ProductAttribute");

            migrationBuilder.DropTable(
                name: "CatalogApi_ProductCategory");

            migrationBuilder.DropTable(
                name: "CatalogApi_ProductImage");

            migrationBuilder.DropTable(
                name: "CatalogApi_Product");
        }
    }
}
