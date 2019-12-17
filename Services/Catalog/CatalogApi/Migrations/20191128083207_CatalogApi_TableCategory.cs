using Microsoft.EntityFrameworkCore.Migrations;

namespace CatalogApi.Migrations
{
    public partial class CatalogApi_TableCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogApi_Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentCategoryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Slug = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true, defaultValue: ""),
                    ProductCounter = table.Column<int>(nullable: false, defaultValue: 0),
                    IsVisible = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogApi_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogApi_Category_CatalogApi_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "CatalogApi_Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogApi_ProductCategory_CategoryId",
                table: "CatalogApi_ProductCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogApi_Category_ParentCategoryId",
                table: "CatalogApi_Category",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogApi_ProductCategory_CatalogApi_Category_CategoryId",
                table: "CatalogApi_ProductCategory",
                column: "CategoryId",
                principalTable: "CatalogApi_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogApi_ProductCategory_CatalogApi_Category_CategoryId",
                table: "CatalogApi_ProductCategory");

            migrationBuilder.DropTable(
                name: "CatalogApi_Category");

            migrationBuilder.DropIndex(
                name: "IX_CatalogApi_ProductCategory_CategoryId",
                table: "CatalogApi_ProductCategory");
        }
    }
}
