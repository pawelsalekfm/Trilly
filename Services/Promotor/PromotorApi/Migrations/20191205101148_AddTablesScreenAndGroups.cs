using Microsoft.EntityFrameworkCore.Migrations;

namespace PromotorApi.Migrations
{
    public partial class AddTablesScreenAndGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromotorApi_ScreenGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false, defaultValue: ""),
                    Description = table.Column<string>(nullable: true, defaultValue: ""),
                    Status = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotorApi_ScreenGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromotorApi_Screen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true, defaultValue: ""),
                    Status = table.Column<int>(nullable: false, defaultValue: 1),
                    GroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotorApi_Screen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotorApi_Screen_PromotorApi_ScreenGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "PromotorApi_ScreenGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotorApi_Screen_GroupId",
                table: "PromotorApi_Screen",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotorApi_Screen");

            migrationBuilder.DropTable(
                name: "PromotorApi_ScreenGroup");
        }
    }
}
